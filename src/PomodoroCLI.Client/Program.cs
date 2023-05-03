using PomodoroCLI.UI;
using PomodoroCLI.Console;
using PomodoroCLI.Timer;

namespace PomodoroCLI.Client{
    public class Program{
        public static void Main(string[] args) {
            var console = new SystemConsole();
            var parsedArgs = ArgsParser.Parse(args);

            var clock = new SystemTimer();
            var cliTimer = new SessionTimer(clock);

            var cli = new CLI(parsedArgs, console, cliTimer);
            cli.Start();
        }


    }
}