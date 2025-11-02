using Godot;
using Godot.Collections;
using System.Collections.Generic;

public partial class RollManager : Node
{
    public List<Player> rollList;

    [Export] private Node2D rollVisualizer;
    [Export] private Array<Label> visualScores;

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

        rollVisualizer.Visible = false;
    }

    public override void _Process(double delta)
    {
        for (int i = 0; i < visualScores.Count; i++)
        {
            var currentPlayer = rollList[i];
            visualScores[i].Text = currentPlayer.PlayerName + ": " + currentPlayer.score;
        }
    }

    public void SwitchRolls()
    {
        var tempList = new List<string>();
        foreach (var player in rollList)
        {
            tempList.Add(player.PlayerName);
        }

        rollList[0].PlayerName = tempList[1]; // Judge --> Devil
        rollList[1].PlayerName = tempList[2]; // Devil --> Angel
        rollList[2].PlayerName = tempList[0]; // Angel --> Judge
    }

    public void FillRoll(string name, string playerName)
    {
        foreach (Player roll in rollList)
        {
            if (roll.Name != name) continue;

            roll.PlayerName = playerName;
        }
    }

    public void ToggleVisualizeRolls()
    {

    }
}