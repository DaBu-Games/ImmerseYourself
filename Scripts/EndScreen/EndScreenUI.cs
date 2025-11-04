using Godot;
using System;
using System.Collections.Generic;

public partial class EndScreenUI : CanvasLayer
{
    [Export] HBoxContainer hContainer;
    [Export] private PackedScene scoreCard;

    public override void _Ready()
    {
        this.Visible = false;
    }

    // list of players
    public void ShowEndScreen(List<RollManager.Player> players)
    {
        this.Visible = true;
        foreach (var player in players)
        {
            var scoreDisplay = this.scoreCard.Instantiate<ScoreDisplay>(); 
            scoreDisplay.SetUp(player.PlayerName, player.score);
            
            hContainer.AddChild(scoreDisplay);
        }
    }
}
