using Godot;

public partial class CaseMenu : Node
{
    private void _on_button_pressed()
    {
        UIManager.Instance.SwitchToJudgePanel();
    }
}
