using Godot;

public partial class UIManager : Node
{
    public static UIManager Instance { get; private set; }
    
    [Export] private PackedScene judgePanel;
    public PackedScene JudgePanel { get { return judgePanel; } }

    [Export] private PackedScene caseMenu;
    public PackedScene CaseMenu { get { return caseMenu; } }

    private SceneChanger sceneChanger = new SceneChanger();

    public override void _Ready()
    {
        Instance = this;
        SwitchToCaseMenu();
    }

    public void SwitchToJudgePanel() => sceneChanger.SwitchScene(judgePanel, this);
    public void SwitchToCaseMenu()   => sceneChanger.SwitchScene(caseMenu, this);
}
