using Godot;

public partial class JudgePanel : Node
{
    private UIManager uiManager;

    public override void _Ready()
    {
        var nodeGroup = GetTree().GetNodesInGroup("MainCamera");
        uiManager = (UIManager)nodeGroup[0];
    }
}