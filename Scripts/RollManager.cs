using Godot;
using System.Collections.Generic;

public partial class RollManager : Node
{
    private List<Roll> rollList;

    public class Roll
    {
        public string Name;
        public string PlayerName;

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

    public override void _Process(double delta)
    {
        foreach (Roll roll in rollList)
        {
            GD.Print(roll.Name, " = ", roll.PlayerName);
        }
    }

    public void FillRoll(string name, string playerName)
    {
        foreach (Roll roll in rollList)
        {
            if (roll.Name == name)
            {
                roll.PlayerName = playerName;
            }
        }
    }
}
