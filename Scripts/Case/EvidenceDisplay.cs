using Godot;

public partial class EvidenceDisplay : Button
{
    [Export] TextureRect textureRect;
    [Export] private Label labelNR;
    private int count;

    private void _on_button_pressed()
    {
       
    }
    
    public void Setup(Texture2D texture, int nr)
    {
        textureRect.Texture = texture;
        labelNR.Text = (nr + 1).ToString();
        
        count = nr;
    }
}
