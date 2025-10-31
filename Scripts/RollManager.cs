using Godot;
using System.Collections.Generic;

public partial class RollManager : Node
{
    private List<Player> rollList;

    public class Player
    {
        public string Name;
        public string PlayerName;

        public int score = 0;

        public Player(string name, string PlayerName)
        {
            this.Name = name;
            this.PlayerName = PlayerName;
        }
    }

    public override void _Ready()
    {
        rollList = new List<Player>()
        {
            new Player("Judge","Player1"),
            new Player("Devil","Player2"),
            new Player("Angel","Player3")
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
        foreach (Player roll in rollList)
        {
            if (roll.Name != name) continue;

            roll.PlayerName = playerName;
        }
    }
}