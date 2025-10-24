using Godot;

public partial class EvidenceDisplay : Button
{
    [Export] private Label labelNR;
    private int count;

    private void _on_button_pressed()
    {
       
    }
    
    public void Setup(Texture2D texture, int nr)
    {
        this.Icon = texture;
        labelNR.Text = nr.ToString();
        
        count = nr;
    }
}
