using Godot;

public partial class UIManager : Node
{
    public static UIManager Instance { get; private set; }
    
    [Export] private PackedScene judgePanel;
    public PackedScene JudgePanel { get { return judgePanel; } }

    [Export] private PackedScene mainMenu;
    public PackedScene MainMenu { get { return mainMenu; } }

    private SceneChanger sceneChanger = new SceneChanger();

    public override void _Ready()
    {
        Instance = this;
        sceneChanger.SwitchScene(mainMenu, this);
    }

    public void SwitchToJudgePanel() => sceneChanger.SwitchScene(judgePanel, this);
    public void SwitchToMainMenu()   => sceneChanger.SwitchScene(mainMenu, this);
}
