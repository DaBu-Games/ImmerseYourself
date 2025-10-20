using Godot;

public partial class UIManager : Node
{
    [Export] private EvidenceDisplay[] evidenceDisplayItems;
    public EvidenceDisplay[] EvidenceItems { get { return evidenceDisplayItems; } }

    [Export] private SceneChanger sceneChanger;
    public SceneChanger SceneChanger { get { return sceneChanger; } }


}
