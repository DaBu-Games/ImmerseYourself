using Godot;
using System;
using System.Collections.Generic;

[GlobalClass]
public partial class Case : Resource
{
    [ExportCategory("Data")]
    [Export] private Godot.Collections.Array<Evidence> evidence { get; set; } = new();
    public Godot.Collections.Array<Evidence> Evidence { get { return evidence; } }
    
    [Export] private Defendant defendant { get; set; } = new();
    public Defendant Defendant { get { return defendant; } }
}
