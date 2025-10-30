using Godot;
using System;

public partial class ScaleRotate : TextureRect
{
    [Export] private ScaleInput input;
    [Export] private float maxRotation;
    [Export] private float minWeight = 0.3f;
    [Export] private float stiffness = 10f;       // spring strength (higher = snappier)
    [Export] private float damping = 0.1f;
    [Export] private Label good;
    [Export] private Label bad;
    
    private float targetRotation = 0f;
    private float angularVelocity = 0f;

    public override async void _Ready()
    {
        await ToSignal(input, ScaleInput.SignalName.Initialized);
        input.UpdateInput(true);
    }
    
    public override void _Process(double delta)
    {
        float weight = input.GetTotalWeight();
        Vector2 percentage = input.GetPercentage();

        if (weight > minWeight)
        {
            float balance = percentage.Y - percentage.X;
            good.Text = percentage.X.ToString("0") + "%";
            bad.Text = percentage.Y.ToString("0") + "%";

            // scale into degrees
            targetRotation = Mathf.DegToRad(maxRotation * balance);
        }
        else
        {
            good.Text = "50%";
            bad.Text = "50%";
            targetRotation = 0f;
        }
        
        float angleDiff = targetRotation - Rotation;
        angularVelocity += angleDiff * stiffness * (float)delta;
        angularVelocity -= angularVelocity * damping * (float)delta;
        Rotation += angularVelocity * (float)delta;
    }

    public override void _ExitTree()
    {
        input.UpdateInput(false);
    }
}
