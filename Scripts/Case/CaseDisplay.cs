using Godot;
using System;

public partial class CaseDisplay : Button
{
    [Export] private Label name;

    private void _on_button_pressed()
    {
       
    }
    
    public void Setup(Texture2D texture, string name)
    {
        this.Icon = texture;
        this.name.Text = name;
    }
}
