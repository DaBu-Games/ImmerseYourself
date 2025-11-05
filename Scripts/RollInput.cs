using Godot;

public partial class RollInput : Node
{
	[Export] private TextEdit inputItem;

	private RollManager rollManager;

	private string name;
	private string input;

	public override void _Ready()
	{
		if (inputItem == null)
		{
			inputItem = this.GetNode<TextEdit>("TextEdit");
		}

		// Gets the RollManager from a different loaded scene.
		var nodeGroup = GetTree().GetNodesInGroup("RollManager");
		rollManager = (RollManager)nodeGroup[0];
	}

	public override void _Process(double delta)
	{
		if (inputItem == null) return;

		input = inputItem.Text;
		name = this.Name;

		rollManager.FillRoll(name, input);
	}
}
