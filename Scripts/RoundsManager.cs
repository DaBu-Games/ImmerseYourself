using Godot;
using System;
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
    public class Round
    {
        public RoundType Type { get; private set; }
        public double Duration { get; private set; }

        public Round(RoundType type, double duration)
        {
            Type = type;
            Duration = duration;
        }
    }
    
    [ExportCategory("Rounds Data")]
    private List<Round> rounds = new List<Round>()
    {
        new Round(RoundType.preparing, 2),
        new Round(RoundType.evil, 2),
        new Round(RoundType.good, 2),
        new Round(RoundType.allOut, 2)
    };

    private int currentRound = 0;
    private double time = 0;
    private double targetTime = 0;
    private bool isRunning = false;

    public override void _Process(double delta)
    {
        if(!isRunning)
            return;
        
        time += delta;
        
        if (time >= targetTime)
        {
            OnRoundEnd();
            currentRound++;
            StartRound();
        }
    }

    public void StartTiking()
    {
        currentRound = 0; 
        StartRound();
    }

    private void StartRound()
    {
        if (currentRound >= rounds.Count)
        {
            GD.Print("All rounds finished!");
            isRunning = false;
            return;
        }

        var round = rounds[currentRound];
        targetTime = round.Duration;
        time = 0;
        isRunning = true;
        
        OnRoundStart();
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
