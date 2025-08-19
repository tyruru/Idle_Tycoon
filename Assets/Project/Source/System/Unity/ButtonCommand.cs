using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public abstract class ButtonCommand : MonoCommand
{
    [SerializeField] protected Button _button;

    protected virtual void Awake()
    {
        Assert.IsNotNull(_button, "[ButtonCommand] Button is null");
        
        _button.onClick.AddListener(Execute);
    }
    
    private void OnDestroy()
    {
        _button.onClick.RemoveListener(Execute);
    }
}
