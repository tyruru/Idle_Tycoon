using System.Collections;
using UnityEngine.SceneManagement;

public class QuitGameSceneButtonCommand : ButtonCommand
{
    public override void Execute()
    {
        StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        var operation = SceneManager.LoadSceneAsync(SceneNames.MainMenu, LoadSceneMode.Additive);

        while (!operation.isDone)
        {
            yield return null;
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(SceneNames.MainMenu));
        
        SceneManager.UnloadSceneAsync(SceneNames.GameScene);
        SceneManager.UnloadSceneAsync(gameObject.scene.name);
    }
}
