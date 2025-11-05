using Godot;

public partial class RollMenu : Node
{
	private void _on_button_pressed()
	{
		UIManager.Instance.SwitchToCaseMenu();
	}
}
