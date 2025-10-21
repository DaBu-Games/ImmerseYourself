using Godot;

public partial class UIManager : Node
{
    [Export] private PackedScene judgePanel;
    public PackedScene JudgePanel { get { return judgePanel; } }

    [Export] private PackedScene mainMenu;
    public PackedScene MainMenu { get { return mainMenu; } }

    public SceneChanger sceneChanger = new SceneChanger();

    public override void _Ready()
    {
        sceneChanger.SwitchScene(mainMenu, this);
    }
}
