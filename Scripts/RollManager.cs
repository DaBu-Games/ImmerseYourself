using Godot;
using Godot.Collections;
using System.Collections.Generic;

public partial class RollManager : Node
{
    public List<Player> rollList;
    public int roundIndex = 1;
    private float minWeight = 0.3f;

    [Export] private Node2D rollVisualizer;
    [Export] private Array<Label> visualNames;
    [Export] private ScaleInput input;

    public class Player
    {
        public string rol;
        public string PlayerName;

        public int score = 0;

        public Player(string rol, string PlayerName)
        {
            this.rol = rol;
            this.PlayerName = PlayerName;
        }
    }

    public bool FinalRound()
    {
        return roundIndex >= 3;
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

    public void ShowNames()
    {
        foreach (Player p in rollList)
        {
            if (p.rol == "Devil")
            {
                visualNames[1].Text = p.PlayerName;
            }
            else if (p.rol == "Angel")
            {
                visualNames[0].Text = p.PlayerName;
            }
        }
        
        visualNames[0].Show();
        visualNames[1].Show();
    }

    public List<string> GetAllRols()
    {
        var tempList = new List<string>();
        foreach (Player p in rollList)
        {
            tempList.Add(p.rol);
        }
        
        return tempList;
    }

    public void SwitchRolls()
    {
        var tempList = GetAllRols();

        rollList[0].rol = tempList[1]; // Judge --> Devil
        rollList[1].rol = tempList[2]; // Devil --> Angel
        rollList[2].rol = tempList[0]; // Angel --> Judge
        
        GD.Print("switch");
                
        visualNames[0].Hide();
        visualNames[1].Hide();

        roundIndex++;
    }

    public void AddScore()
    {
        float weight = input.GetTotalWeight();
        
        Vector2 percentage = input.GetPercentage();
        
        if (weight <= minWeight)
        {
            percentage.X = 0.5f;
            percentage.Y = 0.5f;
        }
        
        foreach (Player p in rollList)
        {
            if (p.rol == "Devil")
            {
                p.score += Mathf.RoundToInt(percentage.Y * 100f);
                GD.Print(p.rol + ": " + p.PlayerName + " -> " + p.score);
            }
            else if (p.rol == "Angel")
            {
                p.score += Mathf.RoundToInt(percentage.X * 100f);
                GD.Print(p.rol + ": " + p.PlayerName + " -> " + p.score);
            }
        }

        if (!FinalRound())
        {
            SwitchRolls();
            UIManager.Instance.ChooseCase();
        }
        else
        {
            UIManager.Instance.ShowEndScreen(rollList);
        }
    }

    public void FillRoll(string name, string playerName)
    {
        foreach (Player roll in rollList)
        {
            if (roll.rol != name) continue;

            roll.PlayerName = playerName;
        }
    }
}