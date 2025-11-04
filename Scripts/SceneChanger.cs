using Godot;

public partial class SceneChanger : Node
{
    public void SwitchScene(PackedScene scene, Node parentNode)
    {
        foreach (var child in parentNode.GetChildren())
        {
            parentNode.RemoveChild(child);
        }

        var instance = scene.Instantiate();
        //instance.GetOwner<Control>().Size = new Vector2(1000000, 1000000); // THIS CODE HURTS ME, BUT IS A TEMP FIX.

        parentNode.AddChild(instance);
    }

    public void TryOpenScene(PackedScene scene, string rootName, Node parentNode)
    {
        var instance = scene.Instantiate();
        foreach (var child in parentNode.GetChildren())
        {
            if (child.IsInGroup(rootName))
            {
                GD.Print("Instance already exists");
                return;
            }
        }

        parentNode.AddChild(instance);
        //instance.GetOwner<Control>().Size = new Vector2(1000000, 1000000); // THIS CODE HURTS ME, BUT IS A TEMP FIX.
    }

    public void CloseScene(PackedScene scene, string rootName, Node parentNode)
    {
        foreach (var child in parentNode.GetChildren())
        {
            if (child.IsInGroup(rootName))
            {
                parentNode.RemoveChild(child);
            }
        }
    }
}
