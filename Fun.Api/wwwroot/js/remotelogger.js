window.onerror = async function (message, source, line, column) {
    await fetch("api/log/error", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(`message: ${message} | source: ${source} | line: ${line} | column: ${column}`)
    });
};