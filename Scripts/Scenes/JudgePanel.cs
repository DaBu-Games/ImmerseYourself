using Godot;

public partial class JudgePanel : Node
{
    private void _on_button_pressed()
    {
        UIManager.Instance.SwitchToJudgePanel();
    }
}