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

        rollManager.SwitchRolls();
        SetDisplay();
    }

    private void SetDisplay()
    {
        for (int i = 0; i < DisplayItems.Count; i++)
        {
            DisplayItems[i].Text = rollManager.rollList[i].PlayerName
                + " --> " + rollManager.rollList[i].Name;
        }
    }

}
