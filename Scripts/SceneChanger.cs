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
