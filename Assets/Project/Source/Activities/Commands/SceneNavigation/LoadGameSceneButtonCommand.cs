using System.Collections;
using UnityEngine.SceneManagement;

public class LoadGameSceneButtonCommand : ButtonCommand
{
    public override void Execute()
    {
        StartCoroutine(LoadScenes());
    }

    private IEnumerator LoadScenes()
    {
        var sceneOp = SceneManager.LoadSceneAsync(SceneNames.LoadScene, LoadSceneMode.Additive);

        while (!sceneOp.isDone)
        {
            yield return null;
        }
        
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(SceneNames.LoadScene));
        SceneManager.UnloadSceneAsync(gameObject.scene.name);
    }
}
