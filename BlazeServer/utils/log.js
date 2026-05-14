const log = {
    info: (...args) => {
        const now = new Date();
        const timeString = `${now.getHours().toString().padStart(2, '0')}:` +
            `${now.getMinutes().toString().padStart(2, '0')}:` +
            `${now.getSeconds().toString().padStart(2, '0')}.` +
            `${now.getMilliseconds().toString().padStart(3, '0')}`;

        console.log(
            `[${timeString}] [\x1b[32minfo\x1b[0m]`,
            ...args
        );
    },
    error: (...args) => {
        const now = new Date();
        const timeString = `${now.getHours().toString().padStart(2, '0')}:` +
            `${now.getMinutes().toString().padStart(2, '0')}:` +
            `${now.getSeconds().toString().padStart(2, '0')}.` +
            `${now.getMilliseconds().toString().padStart(3, '0')}`;

        console.log(
            `[${timeString}] [\x1b[31merror\x1b[0m]`,
            ...args
        );
    },
    debug: (...args) => {
        const now = new Date();
        const timeString = `${now.getHours().toString().padStart(2, '0')}:` +
            `${now.getMinutes().toString().padStart(2, '0')}:` +
            `${now.getSeconds().toString().padStart(2, '0')}.` +
            `${now.getMilliseconds().toString().padStart(3, '0')}`;

        console.log(
            `[${timeString}] [\x1b[90mdebug\x1b[0m]`,
            ...args
        );
    }
}

export { log }