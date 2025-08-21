using UnityEngine.SceneManagement;

public class LoadGameSceneButtonCommand : ButtonCommand
{
    public override void Execute()
    {
        SceneManager.LoadScene(SceneNames.LoadScene);
    }
}
