using UnityEngine;

public class InputService : MonoBehaviour
{
    public static InputService I { get; private set; }

    private PlayerInput _actions;
    public PlayerInput Actions => _actions;

    private void Awake()
    {
        if (I != null && I != this)
        {
            Destroy(gameObject);
            return;
        }

        I = this;
        DontDestroyOnLoad(gameObject);

        _actions = new PlayerInput();
        _actions.Enable();
    }
}