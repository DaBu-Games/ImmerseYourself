using Godot;
using System;

public partial class ScoreDisplay : Node
{
    [Export] private Label nameLabel;
    [Export] private Label scoreLabel;
    [Export] private float duration = 5f;

    private int targetScore = 0;
    private int displayedScore = 0;
    private Tween scoreTween;

    public void SetUp(string name, int newScore)
    {
        nameLabel.Text = name;
        SetScore(newScore);
    }
    
    public void SetScore(int newScore)
    {
        targetScore = newScore;
        
        scoreTween?.Kill();
        
        scoreTween = CreateTween();
        
        scoreTween.TweenMethod(
            Callable.From<int>(value =>
            {
                displayedScore = value;
                scoreLabel.Text = displayedScore.ToString();
            }),
            displayedScore,
            targetScore,
            duration
        ).SetTrans(Tween.TransitionType.Sine).SetEase(Tween.EaseType.Out);
    }
}
