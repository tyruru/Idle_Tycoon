using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainBootstrap : MonoBehaviour
{
    [SerializeField] private Slider _progressBar;

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
        AsyncOperation menuOp = SceneManager.LoadSceneAsync(SceneNames.MainMenu, LoadSceneMode.Additive);
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

        while (!menuOp.isDone)
        {
            yield return null;
        }
        
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(SceneNames.MainMenu));
        
        SceneManager.UnloadSceneAsync(gameObject.scene.name);
    }
}
