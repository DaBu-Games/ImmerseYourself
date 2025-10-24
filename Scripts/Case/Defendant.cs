using Godot;
using System;

[GlobalClass]
public partial class Defendant : Resource
{
    [ExportCategory("Data")]
    [Export] private string Name { get; set; } = "";
    
    [Export(PropertyHint.MultilineText)]
    private string DescriptionGood { get; set; } = "";
    
    [Export(PropertyHint.MultilineText)]
    private string DescriptionEvil { get; set; } = "";
    
    [Export] private Texture2D Image { get; set; }
}
