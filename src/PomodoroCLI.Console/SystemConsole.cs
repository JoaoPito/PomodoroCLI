namespace PomodoroCLI.Console;

public class SystemConsole : IConsoleBase {

    public void WriteLine(string text){
        System.Console.WriteLine(text);
    }
    public void Write(string text){
        System.Console.Write(text);
    }

    public int Read(){
        return System.Console.Read();
    }
    public ConsoleKeyInfo ReadKey(){
        return System.Console.ReadKey();
    }
    public ConsoleKeyInfo ReadKey(bool showsKey){
        return System.Console.ReadKey(showsKey);
    }
    
}