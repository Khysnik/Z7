#[cfg(windows)]
use std::os::windows::process::CommandExt;
#[cfg(windows)]
const CREATE_NO_WINDOW: u32 = 0x0800_0000;

#[tauri::command]
fn base_dir() -> String {
    if cfg!(debug_assertions) {
        return r"C:\Users\user\Downloads\Z7-0.9.9.1".to_string();
    }
    std::env::current_exe()
        .ok()
        .and_then(|p| p.parent().map(|d| d.to_string_lossy().into_owned()))
        .unwrap_or_default()
}

fn is_running(name: &str) -> bool {
    let filter = format!("IMAGENAME eq {}", name);
    let mut c = std::process::Command::new("tasklist");
    c.args(["/FI", filter.as_str(), "/NH"]);
    #[cfg(windows)]
    c.creation_flags(CREATE_NO_WINDOW);
    match c.output() {
        Ok(o) => String::from_utf8_lossy(&o.stdout)
            .to_lowercase()
            .contains(&name.to_lowercase()),
        Err(_) => false,
    }
}

fn kill_name(name: &str) {
    let mut c = std::process::Command::new("taskkill");
    c.args(["/IM", name, "/F"]);
    #[cfg(windows)]
    c.creation_flags(CREATE_NO_WINDOW);
    let _ = c.output();
}

#[tauri::command]
fn proc_running(name: String) -> bool {
    is_running(&name)
}

#[tauri::command]
fn kill_by_name(name: String) {
    kill_name(&name);
}

// Filesystem helpers that work on any drive (the fs plugin's scope is limited).
#[tauri::command]
fn path_exists(path: String) -> bool {
    std::path::Path::new(&path).exists()
}

#[tauri::command]
fn remove_path(path: String) {
    let _ = std::fs::remove_file(&path);
}

// Run a program to completion and return its exit code + output. Uses
// std::process (CreateProcess) directly so argument paths with spaces are
// handled correctly — no cmd.exe re-tokenizing/quote-stripping.
#[derive(serde::Serialize)]
struct ProcOutput {
    code: i32,
    stdout: String,
    stderr: String,
}

#[tauri::command]
fn run_process(program: String, args: Vec<String>, cwd: String) -> Result<ProcOutput, String> {
    let mut c = std::process::Command::new(&program);
    c.args(&args);
    if !cwd.is_empty() {
        c.current_dir(&cwd);
    }
    #[cfg(windows)]
    c.creation_flags(CREATE_NO_WINDOW);
    match c.output() {
        Ok(o) => Ok(ProcOutput {
            code: o.status.code().unwrap_or(-1),
            stdout: String::from_utf8_lossy(&o.stdout).into_owned(),
            stderr: String::from_utf8_lossy(&o.stderr).into_owned(),
        }),
        Err(e) => Err(e.to_string()),
    }
}

// Start a program detached in the background with no console window.
#[tauri::command]
fn spawn_hidden(program: String, args: Vec<String>, cwd: String) -> Result<(), String> {
    let mut c = std::process::Command::new(&program);
    c.args(&args).current_dir(&cwd);
    #[cfg(windows)]
    c.creation_flags(CREATE_NO_WINDOW);
    c.spawn().map(|_| ()).map_err(|e| e.to_string())
}

// Watch a process by name; once it has started and then exited, kill the given
// processes. Used to shut the servers down when the game window is closed.
#[tauri::command]
fn watch_and_cleanup(watch: String, kill: Vec<String>) {
    std::thread::spawn(move || {
        // wait for the watched process to appear (game takes a few seconds)
        let mut appeared = false;
        for _ in 0..120 {
            if is_running(&watch) {
                appeared = true;
                break;
            }
            std::thread::sleep(std::time::Duration::from_millis(500));
        }
        if !appeared {
            return; // game never launched — leave the servers alone
        }
        // wait until it's gone
        while is_running(&watch) {
            std::thread::sleep(std::time::Duration::from_secs(1));
        }
        for k in &kill {
            kill_name(k);
        }
    });
}

#[cfg_attr(mobile, tauri::mobile_entry_point)]
pub fn run() {
    tauri::Builder::default()
        .plugin(tauri_plugin_fs::init())
        .plugin(tauri_plugin_shell::init())
        .plugin(tauri_plugin_dialog::init())
        .invoke_handler(tauri::generate_handler![
            base_dir,
            proc_running,
            kill_by_name,
            path_exists,
            remove_path,
            run_process,
            spawn_hidden,
            watch_and_cleanup
        ])
        .setup(|app| {
            if cfg!(debug_assertions) {
                app.handle().plugin(
                    tauri_plugin_log::Builder::default()
                        .level(log::LevelFilter::Info)
                        .build(),
                )?;
            }
            Ok(())
        })
        .run(tauri::generate_context!())
        .expect("error while running tauri application");
}
