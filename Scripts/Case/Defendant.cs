using Godot;
using System;

[GlobalClass]
public partial class Defendant : Resource
{
    [ExportCategory("Data")]
    [Export] private string name { get; set; } = "";
    public string Name { get { return name; } }
    
    [Export(PropertyHint.MultilineText)]
    private string DescriptionGood { get; set; } = "";
    
    [Export(PropertyHint.MultilineText)]
    private string DescriptionEvil { get; set; } = "";
    
    [Export] private Texture2D image { get; set; }
    public Texture2D Image { get { return image; } }
}
