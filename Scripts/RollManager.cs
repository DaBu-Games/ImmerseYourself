using Godot;
using System.Collections.Generic;

public partial class RollManager : Node
{
    private List<Roll> rollList;

    public class Roll
    {
        public string Name;
        public string PlayerName;

        public int score;

        public Roll(string name, string PlayerName)
        {
            this.Name = name;
            this.PlayerName = PlayerName;
        }
    }

    public override void _Ready()
    {
        rollList = new List<Roll>()
        {
            new Roll("Judge","Player1"),
            new Roll("Devil","Player2"),
            new Roll("Angel","Player3")
        };
    }

    public void SwitchRolls()
    {
        var tempList = rollList;
        rollList[0] = tempList[1]; // Judge --> Devil
        rollList[1] = tempList[2]; // Devil --> Angel
        rollList[2] = tempList[0]; // Angel --> Judge
    }

    public void FillRoll(string name, string playerName)
    {
        foreach (Roll roll in rollList)
        {
            if (roll.Name != name) continue;

            roll.PlayerName = playerName;
            GD.Print(roll.Name, " = ", roll.PlayerName);
        }
    }
}