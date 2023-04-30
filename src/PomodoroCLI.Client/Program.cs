namespace PomodoroCLI.Client{
    public class Program{
        public static void Main(string[] args) {
            var cli = new PomodoroCLI.CLI.CLI(args);
            cli.Start();
        }


    }
}