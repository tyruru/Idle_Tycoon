
using UnityEngine.SceneManagement;

public class LoadSettingsButtonCommand : ButtonCommand
{
    public override void Execute()
    {
        SceneManager.LoadScene(SceneNames.Settings, LoadSceneMode.Additive);    
        
    }
}
