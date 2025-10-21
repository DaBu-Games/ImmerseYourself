using Godot;

public partial class MainMenu : Node
{
    [Export] PackedScene JudgePanel;
    private UIManager uiManager;

    public override void _Ready()
    {
        var nodeGroup = GetTree().GetNodesInGroup("UIManager");
        uiManager = (UIManager)nodeGroup[0];
    }

    private void _on_button_pressed()
    {
        uiManager.sceneChanger.SwitchScene(JudgePanel, uiManager);
    }
}
