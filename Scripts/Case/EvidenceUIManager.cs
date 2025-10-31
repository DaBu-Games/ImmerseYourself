using Godot;
using System;
using System.Threading.Tasks;

public partial class EvidenceUIManager : TextureRect
{
    [Export] private float slideDuration = 0.8f;
    
    private int currentIndex = -1;

    private Vector2 offScreenLeft;
    private Vector2 offScreenRight;
    private Vector2 screenCenter;
    
    private bool isCentered = false;
    private bool isAnimating = false;
    private bool isSlidingOut = false;

    public override void _Ready()
    {
        screenCenter = GetViewportRect().Size / 2 - Size / 2;
        offScreenLeft = new Vector2(-Size.X, screenCenter.Y);
        offScreenRight = new Vector2(GetViewportRect().Size.X + Size.X, screenCenter.Y);
        
        Position = offScreenRight;
        Visible = false;
    }

    public async void SetUp(Texture2D texture, int index)
    {
        if (index == currentIndex || isAnimating)
            return;

        if (isCentered)
        {
            await SlideOut();
        }

        await SlideIn(texture, index);
    }

    private async Task SlideIn(Texture2D texture, int index)
    {
        isAnimating = true;

        Texture = texture;
        currentIndex = index;
        Visible = true;
        Position = offScreenLeft;

        var tween = CreateTween()
            .SetEase(Tween.EaseType.Out)
            .SetTrans(Tween.TransitionType.Cubic);

        tween.TweenProperty(this, "position", screenCenter, slideDuration);
        await ToSignal(tween, Tween.SignalName.Finished);

        isCentered = true;
        isAnimating = false;
    }

    public async Task SlideOut()
    {
        if(Texture == null)
            return;
        
        isAnimating = true;

        var tween = CreateTween()
            .SetEase(Tween.EaseType.In)
            .SetTrans(Tween.TransitionType.Cubic);

        tween.TweenProperty(this, "position", offScreenRight, slideDuration);
        await ToSignal(tween, Tween.SignalName.Finished);

        isCentered = false;
        Visible = false;
        isAnimating = false;
    }
}
