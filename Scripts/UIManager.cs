using Godot;

public partial class UIManager : Node
{
    public static UIManager Instance { get; private set; }
    [ExportCategory("Small screen")]

    [Export] private PackedScene judgePanel;
    public PackedScene JudgePanel { get { return judgePanel; } }

    [Export] private PackedScene caseMenu;
    public PackedScene CaseMenu { get { return caseMenu; } }

    [ExportCategory("Big screen")]

    [Export] private DefendantDisplay defendantDisplay;
    [Export] private EvidenceSlideIn evidenceSlideIn;

    [Export] private TextureRect scale;
    [Export] private TextureRect scaleFocus;
    
    [Export] private CanvasLayer timerDisplay;

    private SceneChanger sceneChanger = new SceneChanger();

    public override void _Ready()
    {
        Instance = this;
        ChooseCase();
    }

    public void SwitchToJudgePanel() => sceneChanger.SwitchScene(judgePanel, this);
    public void SwitchToCaseMenu() => sceneChanger.SwitchScene(caseMenu, this);

    public void ChooseCase()
    {
        GD.Print("HEY");
        SwitchToCaseMenu();
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
        evidenceSlideIn.SetUp(texture, index);
    }

    public void ShowResult()
    {
        timerDisplay.Hide();
        evidenceSlideIn.SlideOut();
        scale.ZIndex = 1;
        defendantDisplay.ChangeZIndex(2);
        scaleFocus.Show();
    }
}
