using Godot;
using System;
using System.Collections.Generic;

public partial class CaseManager : Node
{
    [Export] private Godot.Collections.Array<Case> cases { get; set; } = new();
    [Export] private PackedScene evidenceScene;
    [Export] private PackedScene caseScene;
    [Export] private RoundsManager roundsManager;
    
    private List<int> choosedCase = new();
    private int caseIndex = 0;

    public override void _Ready()
    {
        GetTree().NodeAdded += SetEvidence;
        GetTree().NodeAdded += SetCases;
    }

    public void SetNewCase(int index)
    {
        caseIndex = index;
        choosedCase.Add(index);
        roundsManager.StartTiking();
        UIManager.Instance.StartCase(cases[index].Defendant);
    }

    public void ShowEvidence(int index)
    {
        UIManager.Instance.SlideInEvidence(cases[caseIndex].Evidence[index].Image, index);
    }

    private void SetEvidence(Node evidenceScreen)
    {
        if (evidenceScreen.IsInGroup("EvidenceScreen"))
        {
            
            var currentEvidence = cases[caseIndex].Evidence;
            
            for(int i = 0; i < currentEvidence.Count; i++ )
            {
                var evidenceDisplay = evidenceScene.Instantiate<EvidenceDisplay>();
                
                evidenceDisplay.SetUp(currentEvidence[i].Image, i);
                int count = i;
                evidenceDisplay.Pressed += () => ShowEvidence(count);
                
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
               
               caseDisplay.Setup(defendant.Icon, defendant.Name);
               int count = i;
               if (choosedCase.Contains(i))
               {
                   caseDisplay.Disabled = true;
               }
               else
               {
                   caseDisplay.Pressed += () => SetNewCase(count);
               }
               
               caseMenu.AddChild(caseDisplay);
            }
        }
    }
}
