async function atmbEvent(eventId) {
    return Promise.resolve(eventId + "-" + (arguments.length - 1));
}