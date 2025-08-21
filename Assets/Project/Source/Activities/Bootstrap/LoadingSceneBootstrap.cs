using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LoadingSceneBootstrap : MonoBehaviour
{
    [SerializeField] private Slider _progressBar;

    private AsyncOperation _hudOperation;
    private AsyncOperation _levelOperation;

    private void Start()
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
        LoadScenes();

        float duration = Random.Range(2f, 3f);
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            _progressBar.value = Mathf.Lerp(0f, 0.8f, elapsed / duration);
            yield return null;
        }

        _levelOperation!.allowSceneActivation = true;
        while (!_levelOperation.isDone)
        {
            yield return null;
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(SceneNames.GameScene));
        _hudOperation!.allowSceneActivation = true;
        SceneManager.UnloadSceneAsync(gameObject.scene.name);
    }

    private void LoadScenes()
    {
        _levelOperation = SceneManager.LoadSceneAsync(SceneNames.GameScene, LoadSceneMode.Additive);
        _levelOperation!.allowSceneActivation = false;
        
        _hudOperation = SceneManager.LoadSceneAsync(SceneNames.HUD, LoadSceneMode.Additive);
        _hudOperation!.allowSceneActivation = false;
    }

      
        
}
