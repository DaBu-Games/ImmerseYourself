using Godot;

public partial class EvidenceDisplay : Button
{
    [Export] Vector2 endPosition;
    Vector2 startPosition;


    [Export] float sizeMultiplier;
    Vector2 endSize;
    Vector2 startSize;

    bool isMoving = false;

    float lerpDuration = 3;
    float timeElapsed;
    float speed = 5;


    public override void _Ready()
    {
        startPosition = this.Position;

        startSize = this.Scale;
        endSize = startSize * sizeMultiplier;
    }

    private void _on_button_pressed()
    {
        isMoving = true;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (!isMoving) return;

        if (timeElapsed < lerpDuration)
        {
            this.Position = startPosition.Lerp(endPosition, timeElapsed / lerpDuration);
            this.Scale = startSize.Lerp(endSize, timeElapsed / lerpDuration);
            timeElapsed += (float)(delta * speed);
        }
        else
        {
            isMoving = false;
            timeElapsed = 0;

            //Swap start and end values.
            (startPosition, endPosition) = (endPosition, startPosition);
            (startSize, endSize) = (endSize, startSize);
        }
    }


}
