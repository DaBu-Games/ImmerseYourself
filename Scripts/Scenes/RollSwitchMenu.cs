using Godot;

public partial class RollSwitchMenu : Node
{
    private void _on_button_pressed()
    {
        UIManager.Instance.ChooseCase();
    }
}
