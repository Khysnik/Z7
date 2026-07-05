import adapter from '@sveltejs/adapter-static';
import { vitePreprocess } from '@sveltejs/vite-plugin-svelte';

/** @type {import('@sveltejs/kit').Config} */
const config = {
	preprocess: vitePreprocess(),

	kit: {
		// adapter-static + ssr=false gives a pure SPA, which is what Tauri loads.
		adapter: adapter(),
		alias: {
			'@/*': './path/to/lib/*'
		}
	},

	vitePlugin: {
		// Per-file compile options. Skip node_modules so third-party libraries
		// keep their own runes/legacy settings.
		dynamicCompileOptions({ filename }) {
			const isLibrary = filename.split(/[/\\]/).includes('node_modules');
			if (isLibrary) return {};

			return {
				// Force runes mode for our own components. Can be removed in svelte 6.
				runes: true,
				// Enable top-level `await` in components — needed for awaiting
				// Tauri fs ops (readTextFile, etc.) directly in <script>.
				experimental: {
					async: true
				}
			};
		}
	}
};

export default config;
