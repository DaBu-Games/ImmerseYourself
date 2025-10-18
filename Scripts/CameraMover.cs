using Godot;

public partial class CameraMover : Camera2D
{
    [Export] Vector2[] movePosition;
    [Export] float speed = 5f;

    Vector2 cameraPosition;

    int currentTarget = 0;
    int test;

    bool isMoving = false;

    float timeElapsed;
    float lerpDuration = 3;

    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("TestSpace"))
        {
            cameraPosition = this.Position;
            isMoving = true;
        }
    }

    private void _on_button_pressed()
    {
        cameraPosition = this.Position;
        isMoving = true;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (!isMoving) return;

        if (timeElapsed < lerpDuration)
        {
            this.Position = cameraPosition.Lerp(movePosition[currentTarget], timeElapsed / lerpDuration);
            timeElapsed += (float)(delta * speed);
        }
        else
        {
            isMoving = false;
            timeElapsed = 0;

            currentTarget++;
            if (currentTarget >= movePosition.Length) currentTarget = 0;
            GD.Print(currentTarget);
        }
    }

}
