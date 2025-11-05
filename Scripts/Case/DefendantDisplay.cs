using Godot;
using System;

public partial class DefendantDisplay : TextureRect
{
	[Export] private float fadeDuration = 1.0f;
	private Defendant defendant;
	private float alpha = 0.0f;

	public override void _Ready()
	{
		Modulate = new Color(1, 1, 1, 0);
	}


	public void SetUp(Defendant defendant)
	{
		this.defendant = defendant;
		this.Texture = defendant.Sprite;
		ShowDefendant(true);
	}

	public async void ShowDefendant(bool show)
	{
		var tween = CreateTween();
		tween.TweenProperty(this, "modulate:a", show ? 1.0f : 0.0f, fadeDuration);
		await ToSignal(tween, Tween.SignalName.Finished);
	}

	public void ChangeZIndex(int zIndex)
	{
		ZIndex = zIndex; 
	}
}
