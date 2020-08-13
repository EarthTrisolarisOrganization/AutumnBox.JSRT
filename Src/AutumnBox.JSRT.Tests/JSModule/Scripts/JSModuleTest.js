// @name         Hello World
// @version      0.0.5
// @description  Great Test
// @author       zsh2401@163.com
function atmbMain() {
    return testMethod();
}
function atmbEvent(eventName) {
    return eventName + "-" + arguments.length;
}
const testMethod = () => {
    Console.WriteLine("Good");
    return 2401;
}