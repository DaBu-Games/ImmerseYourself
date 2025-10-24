using Godot;
using System;

[GlobalClass]
public partial class Evidence : Resource
{
    [ExportCategory("Data")]
    //[Export] private string Title { get; set; } = "";
    
    //[Export(PropertyHint.MultilineText)]
    //private string Description { get; set; } = "";
    
    [Export] private Texture2D image { get; set; }
    public Texture2D Image { get { return image; } }
}
