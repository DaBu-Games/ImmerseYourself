using Godot;
using System;

public partial class ScaleRotate : TextureRect
{
    [Export] private ScaleInput input;
    [Export] private float maxRotation;
    [Export] private float minWeight = 0.3f;
    [Export] private float stiffness = 5;
    [Export] private float damping = 0.5f;
    [Export] private Label good;
    [Export] private Label bad;
    
    private float textUpdateTimer = 0f;
    private float targetRotation = 0f;
    private float angularVelocity = 0f;
    private const float textUpdateInterval = 0.2f;

    public override async void _Ready()
    {
        await ToSignal(input, ScaleInput.SignalName.Initialized);
        input.UpdateInput(true);
        
        PivotOffset = Size / 2f;
    }
    
    public override void _Process(double delta)
    {
        float weight = input.GetTotalWeight();
        Vector2 percentage = input.GetPercentage();

        if (weight > minWeight)
        {
            float balance = percentage.Y - percentage.X;

            // scale into degrees
            targetRotation = Mathf.DegToRad(maxRotation * balance);
        }
        else
        {
            targetRotation = 0f;
        }
        
        float angleDiff = targetRotation - Rotation;
        angularVelocity += angleDiff * stiffness * (float)delta;
        angularVelocity -= angularVelocity * damping * (float)delta;
        Rotation += angularVelocity * (float)delta;
        
        textUpdateTimer += (float)delta;
        if (textUpdateTimer >= textUpdateInterval)
        {
            textUpdateTimer = 0f;
            if (weight > minWeight)
            {
                good.Text = $"{percentage.X * 100:0}%";
                bad.Text = $"{percentage.Y * 100:0}%";
            }
            else
            {
                good.Text = "50%";
                bad.Text = "50%";
            }
        }
    }

    public override void _ExitTree()
    {
        input.UpdateInput(false);
    }
}
