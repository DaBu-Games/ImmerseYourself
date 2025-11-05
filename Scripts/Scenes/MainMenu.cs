using Godot;

public partial class MainMenu : Node
{
	private void _on_button_pressed()
	{
		UIManager.Instance.SwitchToRollMenu();
	}
}
