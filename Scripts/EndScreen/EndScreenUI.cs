using Godot;
using System;

public partial class EndScreenUI : CanvasLayer
{
    [Export] HBoxContainer hContainer;
    [Export] private PackedScene scoreCard;

    public override void _Ready()
    {
        this.Visible = false;
    }

    // list of players
    public void ShowEndScreen()
    {
        this.Visible = true;
        for (int i = 0; i < 3; i++)
        {
            var scoreDisplay = this.scoreCard.Instantiate<ScoreDisplay>(); 
            scoreDisplay.SetUp("Daan", 60 * i);
            
            hContainer.AddChild(scoreDisplay);
        }
    }
}
