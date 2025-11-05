using Godot;
using System.Collections.Generic;

public enum RoundType
{
    preparing,
    good,
    evil,
    allOut,
}

public partial class RoundsManager : Node
{
    [Export] public Label timer;
    [Export] public Label roundTimer;

    [Export] public Node2D WarningDisplay;

    public class Round
    {
        public RoundType Type { get; private set; }
        public double Duration { get; private set; }

        public bool Skipable { get; private set; }
        public string DisplayInfo { get; private set; }

        public Round(RoundType type, double duration, bool skipable, string displayInfo = "")
        {
            Type = type;
            Duration = duration * 60;
            DisplayInfo = displayInfo;
            Skipable = skipable;
        }
    }

    [ExportCategory("Rounds Data")]
    private List<Round> rounds = new List<Round>()
    {
        new Round(RoundType.preparing, 1, true, "Prep phase"),
        new Round(RoundType.evil,      1, true, "Opening statement [SINS]"),
        new Round(RoundType.good,      1, true, "Opening statement [VIRTUES]"),
        new Round(RoundType.allOut,    3, false, "OPEN DEBATE")

    };

    private int currentRound = 0;
    private double time = 0;
    private double targetTime = 0;

    private bool isRunning = false;

    private double fullTimer = 0;

    public override void _Process(double delta)
    {
        if (time != 0 && Input.IsActionJustPressed("TestSpace")) TryStartNextRound();

        if (!isRunning)
            return;

        time += delta;

        DisplayTimers(delta);

        if (time > targetTime)
        {
            OnRoundEnd();
            currentRound++;

            isRunning = false;

            //Display based on round state.
            if (currentRound >= rounds.Count)
            {
                UIManager.Instance.RoundsCompleted();
            }
            else
            {
                WarningDisplay.Visible = true;
            }
        }
    }

    public void StartTiking()
    {
        foreach (var round in rounds)
        {
            fullTimer += round.Duration;
        }

        currentRound = 0;
        StartRound();
    }

    private void StartRound()
    {
        if (currentRound >= rounds.Count)
        {
            isRunning = false;
            return;
        }

        var round = rounds[currentRound];
        targetTime = round.Duration;
        time = 0;

        isRunning = true;

        WarningDisplay.Visible = false;

        OnRoundStart();
    }

    private void TryStartNextRound()
    {
        if (currentRound > rounds.Count) return;

        if (!rounds[currentRound].Skipable) return;

        //Check for remaining time after the current round.
        double newTime = 0;
        for (int i = 0; i < rounds.Count; i++)
        {
            if (i > currentRound)
            {
                newTime += rounds[i].Duration;
            }
        }
        fullTimer = newTime == 0 ? fullTimer : newTime;

        currentRound++;
        StartRound();
    }

    private void DisplayTimers(double delta)
    {
        fullTimer -= delta;
        int minutes = (int)fullTimer / 60;
        int seconds = (int)fullTimer % 60;
        timer.Text = $"{minutes}:{seconds}";

        var infoText = rounds[currentRound].DisplayInfo;
        minutes = (int)(rounds[currentRound].Duration - time) / 60;
        seconds = (int)(rounds[currentRound].Duration - time) % 60;
        roundTimer.Text = $"{infoText}, {minutes}:{seconds}";
    }

    private void OnRoundStart()
    {
        GD.Print($"[Event] {rounds[currentRound].Type} started!");
    }

    private void OnRoundEnd()
    {
        GD.Print($"[Event] {rounds[currentRound].Type} ended!");
    }

}
