using PomodoroCLI.UI;
using PomodoroCLI.Console;

namespace PomodoroCLI.Client{
    public class Program{
        public static void Main(string[] args) {
            var console = new SystemConsole();
            var parsedArgs = ArgsParser.Parse(args);

            var cli = new CLI(parsedArgs, console);
            cli.Start();
        }


    }
}