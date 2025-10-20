using Godot;

public partial class CameraMover : Node
{
    Camera2D camera;

    Vector2 movePosition;
    Vector2 zoom;

    Vector2 cameraPosition;
    Vector2 cameraZoom;

    bool isMoving = false;

    float lerpDuration = 3;
    float timeElapsed;
    float speed = 5f;

    public override void _Ready()
    {
        var nodeGroup = GetTree().GetNodesInGroup("MainCamera");
        camera = (Camera2D)nodeGroup[0];
    }

    private void _on_button_pressed(Vector2 position, float speed = 5.0f, float zoom = 0.6f)
    {
        this.zoom = new Vector2(zoom, zoom);
        this.movePosition = position;
        this.speed = speed;

        cameraPosition = camera.Position;
        cameraZoom = camera.Zoom;

        isMoving = true;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (!isMoving) return;

        if (timeElapsed < lerpDuration)
        {
            camera.Position = cameraPosition.Lerp(movePosition, timeElapsed / lerpDuration);
            camera.Zoom = cameraZoom.Lerp(zoom, timeElapsed / lerpDuration);
            timeElapsed += (float)(delta * speed);
        }
        else
        {
            isMoving = false;
            timeElapsed = 0;
        }
    }

}
