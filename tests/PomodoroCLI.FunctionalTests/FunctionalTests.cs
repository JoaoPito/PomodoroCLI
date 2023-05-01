using PomodoroCLI.UI;

namespace PomodoroCLI.FunctionalTests;

public class FunctionalTests
{
    [Fact]
    public void Test1()
    {
        //Pedro wants to start a standard study session
        // He opens up the PomodoroCLI client with default parameters
        var console = new ConsoleMock();
        var args = PomodoroCLI.UI.ArgsParser.LoadDefaults();
        var pomodoro = new UI.CLI(args, console);
        pomodoro.Start();

        // He sees that the app opened as expected
        // Pedro sees: 00:45:00 [Press Spacebar]
        AssertConsoleText("00:45:00\n[Press Spacebar]\n", console);

        // Then, he presses the spacebar to start
        console.SendKey(new ConsoleKeyInfo(' ', ConsoleKey.Spacebar, false, false, false));

        // A second goes by
        // clock mock -> skip 1s

        // He sees that the time is ticking and that he is on his first session of the day
        AssertConsoleText("00:44:59\n[Press Spacebar]\n", console);

        // He works until the timer finishes and a notification pops up
        // He wraps up his work and presses the spacebar again to start a break session
        // After the break ends a notification shows up, he presses the spacebar again for another session
        // He sees the session counter show that he is on his second session
    }

    void AssertConsoleText(string expectedText, ConsoleMock console){
        var result = console.GetPrintBuffer();
        Assert.True(expectedText == result, $"App text not expected. Got: \"{result}\", expected: \"{expectedText}\"");
    }
}