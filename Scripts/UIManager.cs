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

    [Export] private PackedScene mainMenu;
    public PackedScene Mainmenu { get { return mainMenu; } }

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

        sceneChanger.SwitchScene(mainMenu, this);
        ChooseCase();
    }

    public void SwitchToJudgePanel()
    {
        sceneChanger.SwitchScene(judgePanel, this);
        ChooseCase();
    }
    public void SwitchToCaseMenu() => sceneChanger.SwitchScene(caseMenu, this);
    public void SwitchToRollSwitch() => sceneChanger.SwitchScene(rollSwitchMenu, this);
    public void SwitchToRollMenu() => sceneChanger.SwitchScene(rollMenu, this);


    public void ChooseCase()
    {
        GD.Print("HEY");

        //SwitchToCaseMenu();
        scale.ZIndex = 0;
        defendantDisplay.ChangeZIndex(0);
        scaleFocus.Hide();
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
        ResetCameraPosition();
    }

    public void ShowResult()
    {
        timerDisplay.Hide();
        scale.ZIndex = 1;
        defendantDisplay.ChangeZIndex(2);

        SwitchToRollSwitch();
        scaleFocus.Show();
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
