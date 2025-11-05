using Godot;
using Godot.Collections;

public partial class RollDisplayer : Node2D
{
    [Export] private Array<Label> DisplayItems;

    private RollManager rollManager;

    public override void _Ready()
    {
        // Gets the RollManager from a different loaded scene.
        var nodeGroup = GetTree().GetNodesInGroup("RollManager");
        rollManager = (RollManager)nodeGroup[0];

        rollManager.ShowNames();
        SetDisplay();
    }

    public void AddScore()
    {
        if (rollManager.roundIndex < 4 )
        {
            rollManager.AddScore();
        }
    }

    private void SetDisplay()
    {
        if (!rollManager.FinalRound())
        {
            for (int i = 0; i < DisplayItems.Count; i++)
            {
                int index = i + 1 == 3 ? 0 : i + 1;
            
                DisplayItems[i].Text = rollManager.rollList[i].PlayerName
                                       + " --> " + rollManager.rollList[index].rol;
            }
        }
        else
        {
            DisplayItems[0].Text = "No more rounds";
        }
        
    }

}
