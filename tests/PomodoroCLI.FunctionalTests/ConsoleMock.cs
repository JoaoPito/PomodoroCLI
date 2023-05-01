namespace PomodoroCLI.FunctionalTests;

public class ConsoleMock : PomodoroCLI.Console.IConsoleBase {

    string buffer = "";
    ConsoleKeyInfo lastInput = new ConsoleKeyInfo();

    public void WriteLine(string text){
        buffer += (text + '\n');
    }
    public void Write(string text){
        buffer += text;
    }

    public int Read(){
        return ((short)lastInput.KeyChar);
    }
    public ConsoleKeyInfo ReadKey(){
        return ReadKey(false);
    }
    public ConsoleKeyInfo ReadKey(bool showsKey){
        var sentInput = lastInput;
        FlushInput();
        return lastInput;
    }

    void FlushInput(){
        lastInput = new ConsoleKeyInfo();
    }

    public void SendKey(ConsoleKeyInfo key){
        lastInput = key;
    }

    public string GetPrintBuffer() {
        return buffer;
    }

}