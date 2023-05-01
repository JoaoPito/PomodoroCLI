namespace PomodoroCLI.Console;

public interface IConsoleBase {
    public void WriteLine(string text);
    public void Write(string text);

    public int Read();
    public ConsoleKeyInfo ReadKey();
    public ConsoleKeyInfo ReadKey(bool showsKey);
}