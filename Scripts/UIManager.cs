using Godot;

public partial class UIManager : Node
{
    public static UIManager Instance { get; private set; }
    [ExportCategory("Small screen")]

    [Export] private PackedScene judgePanel;
    public PackedScene JudgePanel { get { return judgePanel; } }

    [Export] private PackedScene caseMenu;
    public PackedScene CaseMenu { get { return caseMenu; } }

    [Export] private PackedScene rollMenu;
    public PackedScene Rollmenu { get { return rollMenu; } }

    [Export] private PackedScene rollSwitchMenu;
    public PackedScene RollSwitchMenu { get { return rollSwitchMenu; } }



    [ExportCategory("Big screen")]

    [Export] private DefendantDisplay defendantDisplay;
    [Export] private EvidenceUIManager evidenceUiManager;

    [Export] private TextureRect scale;
    [Export] private Node2D scaleFocus;

    [Export] private CanvasLayer timerDisplay;
    [Export] private Camera2D camera2D;
    
    [Export] private EndScreenUI endScreenUI;

    private SceneChanger sceneChanger = new SceneChanger();

    public override void _Ready()
    {
        Instance = this;

        sceneChanger.SwitchScene(rollMenu, this);
        ResetCase();
    }

    public void SwitchToJudgePanel()
    {
        sceneChanger.SwitchScene(judgePanel, this);
        ResetCase();
    }

    private void ResetCase()
    {
        scale.ZIndex = 0;
        defendantDisplay.ChangeZIndex(0);
        scaleFocus.Hide();
    }
    
    public void SwitchToCaseMenu() => sceneChanger.SwitchScene(caseMenu, this);
    public void SwitchToRollSwitch() => sceneChanger.SwitchScene(rollSwitchMenu, this);

    public void ChooseCase()
    {
        ResetCase();
        defendantDisplay.Texture = null;
        SwitchToCaseMenu();
    }

    public void StartCase(Defendant defendant)
    {
        SwitchToJudgePanel();
        defendantDisplay.SetUp(defendant);
        timerDisplay.Show();
    }

    public void SlideInEvidence(Texture2D texture, int index)
    {
        evidenceUiManager.SetUp(texture, index);
    }

    public void RoundsCompleted()
    {
        evidenceUiManager.SlideOut();
        (GetTree().GetFirstNodeInGroup("evidenceButton") as TextureButton)?.SetDisabled(true);
        timerDisplay.Hide();
        scale.ZIndex = 1;
        defendantDisplay.ChangeZIndex(2);
        scaleFocus.Show();
        
        ResetCameraPosition();
        SwitchToRollSwitch();
    }


    private void ResetCameraPosition()
    {
        camera2D.Position = GetViewport().GetVisibleRect().Size / 2;
    }

    public void ShowEndScreen()
    {
        endScreenUI.ShowEndScreen();
    }
}
