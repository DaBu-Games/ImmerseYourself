using Godot;
using System;

public partial class CaseManager : Node
{
    [Export] private Godot.Collections.Array<Case> Cases { get; set; } = new();
    [Export] private PackedScene evidenceScene;
    private int caseIndex = 0;
    private Node evidenceScreen;

    public override void _Ready()
    {
        GetTree().NodeAdded += SetEvidence;
    }

    public void SetNewCase()
    {
        
    }

    private void SetEvidence(Node node)
    {
        if (node.IsInGroup("EvidenceScreen"))
        {
            evidenceScreen = node;
            
            var currentEvidence = Cases[caseIndex].Evidence;
        
            // Example loop through your case’s evidence
            for(int i = 0; i < currentEvidence.Count; i++ )
            {
                var evidenceDisplay = evidenceScene.Instantiate<EvidenceDisplay>();
                int count = i + 1;
                
                evidenceDisplay.Setup(currentEvidence[i].Image, count);
                
                evidenceScreen.AddChild(evidenceDisplay);
            }
        }
    }
}
