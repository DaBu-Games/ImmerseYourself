using Godot;
using System;

public partial class ScaleRotate : Node2D
{
    [Export] private ScaleInput input;
    [Export] private float maxRotation;
    [Export] private float minWeight = 0.3f;
    [Export] private float stiffness = 10f;       // spring strength (higher = snappier)
    [Export] private float damping = 0.1f; 
    
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
    }

    public override void _ExitTree()
    {
        input.UpdateInput(false);
    }
}
