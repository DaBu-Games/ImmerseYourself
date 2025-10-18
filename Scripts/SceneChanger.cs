using Godot;

public partial class SceneChanger : Node
{
    [Export] string baseScene;

    private void _on_button_pressed(string sceneLocation) => ChangeScene(sceneLocation);
    private void _on_button_pressed() => ChangeScene();


    private void ChangeScene(string scene) => GetTree().ChangeSceneToFile(scene);
    private void ChangeScene() => GetTree().ChangeSceneToFile(baseScene);
}
