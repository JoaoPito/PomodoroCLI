﻿namespace PomodoroCLI.Timer;
public interface Timer
{
    public void SetDuration(TimeSpan duration);
    public TimeSpan GetRemainingTime();

    public void Start();
}
