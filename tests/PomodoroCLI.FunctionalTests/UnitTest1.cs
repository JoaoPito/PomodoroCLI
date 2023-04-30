namespace PomodoroCLI.FunctionalTests;

public class FunctionalTests
{
    [Fact]
    public void Test1()
    {
        //Pedro wants to start a study session
        // He opens up the PomodoroCLI client with args 45 15 for a 45min session/15min break
        // Then, he presses the spacebar to start
        // He sees that the time is ticking and that he is on his first session of the day
        // He works until the timer finishes and a notification pops up
        // He wraps up his work and presses the spacebar again to start a break session
        // After the break ends a notification shows up, he presses the spacebar again for another session
        // He sees the session counter show that he is on his second session
    }
}