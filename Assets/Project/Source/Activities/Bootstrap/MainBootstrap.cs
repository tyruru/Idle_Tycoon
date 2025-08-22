using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainBootstrap : MonoBehaviour
{
    [SerializeField] private Slider _progressBar;

    private AsyncOperation menuOp;
    private void Awake()
    {
        Load();
    }

    private void Load()
    {
        Time.timeScale = 1f;
        StartCoroutine(LoadScenesAsync());
    }

    private IEnumerator LoadScenesAsync()
    {
        AsyncOperation audioOp = SceneManager.LoadSceneAsync(SceneNames.Audio, LoadSceneMode.Additive);
        menuOp = SceneManager.LoadSceneAsync(SceneNames.MainMenu, LoadSceneMode.Additive);
        menuOp.allowSceneActivation = false;
        
        float duration = Random.Range(2f, 3f);
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            _progressBar.value = Mathf.Lerp(0f, 0.8f, elapsed / duration);
            yield return null;
        }

        menuOp.allowSceneActivation = true;
        menuOp.completed += ActiveScene;
        // while (!menuOp.isDone)
        // {
        //     yield return null;
        // }
        
       
    }

    private void ActiveScene(AsyncOperation obj)
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(SceneNames.MainMenu));
        
        SceneManager.UnloadSceneAsync(gameObject.scene.name);
        
        menuOp.completed -= ActiveScene;
    }
}
