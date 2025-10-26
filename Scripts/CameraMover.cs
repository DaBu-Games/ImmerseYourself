using Godot;

public partial class CameraMover : Node
{
    Camera2D camera;

    Vector2 movePosition;

    Vector2 cameraPosition;

    bool isMoving = false;

    float lerpDuration = 3;
    float timeElapsed;
    float speed = 5f;

    public override void _Ready()
    {
        var nodeGroup = GetTree().GetNodesInGroup("MainCamera");
        camera = (Camera2D)nodeGroup[0];
    }

    private void _on_button_pressed(Vector2 position)
    {
        this.movePosition = position;
        cameraPosition = camera.Position;

        isMoving = true;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (!isMoving) return;

        if (timeElapsed < lerpDuration)
        {
            camera.Position = cameraPosition.Lerp(movePosition, timeElapsed / lerpDuration);
            timeElapsed += (float)(delta * speed);
        }
        else
        {
            isMoving = false;
            timeElapsed = 0;
        }
    }

}
