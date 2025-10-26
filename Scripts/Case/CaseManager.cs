using Godot;
using System;

public partial class CaseManager : Node
{
    [Export] private Godot.Collections.Array<Case> cases { get; set; } = new();
    [Export] private PackedScene evidenceScene;
    [Export] private PackedScene caseScene;
    [Export] private RoundsManager roundsManager;
    
    private int caseIndex = 0;

    public override void _Ready()
    {
        GetTree().NodeAdded += SetEvidence;
        GetTree().NodeAdded += SetCases;
    }

    public void SetNewCase(int index)
    {
        caseIndex = index;
        roundsManager.StartTiking();
        
        UIManager.Instance.SwitchToJudgePanel();
    }

    private void SetEvidence(Node evidenceScreen)
    {
        if (evidenceScreen.IsInGroup("EvidenceScreen"))
        {
            
            var currentEvidence = cases[caseIndex].Evidence;
            
            for(int i = 0; i < currentEvidence.Count; i++ )
            {
                var evidenceDisplay = evidenceScene.Instantiate<EvidenceDisplay>();
                
                evidenceDisplay.Setup(currentEvidence[i].Image, i);
                
                evidenceScreen.AddChild(evidenceDisplay);
            }
        }
    }

    private void SetCases(Node caseMenu)
    {
        if (caseMenu.IsInGroup("CaseMenu"))
        {
            for (int i = 0; i < cases.Count; i++)
            { 
                var defendant = cases[i].Defendant;
               var caseDisplay = caseScene.Instantiate<CaseDisplay>();
               
               caseDisplay.Setup(defendant.Image, defendant.Name);
               int count = i;
               caseDisplay.Pressed += () => SetNewCase(count);
               
               caseMenu.AddChild(caseDisplay);
            }
        }
    }
}
