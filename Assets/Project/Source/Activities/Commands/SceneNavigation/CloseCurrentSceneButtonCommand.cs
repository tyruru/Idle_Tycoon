using UnityEngine.SceneManagement;

public class CloseCurrentSceneButtonCommand : ButtonCommand
{
    public override void Execute()
    {
        SceneManager.UnloadSceneAsync(gameObject.scene.name);
    }
}
