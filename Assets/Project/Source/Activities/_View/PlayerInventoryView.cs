using UnityEngine;

public class PlayerInventoryView : MonoBehaviour
{
    [SerializeField] private PlayerInventoryPresenter _playerInventory;
    
    private void Start()
    {
        _playerInventory = GameSession.I.PlayerInventory;
        
    }
    
    public void AddResource(string id)
    {
        if (_playerInventory == null)
        {
            Debug.LogError("PlayerInventory is not initialized.");
            return;
        }
        
        _playerInventory.AddResource(id);
        Debug.Log($"Adding {id} to player inventory.");
    }
    
    public void RemoveResource(string id)
    {
        if (_playerInventory == null)
        {
            Debug.LogError("PlayerInventory is not initialized.");
            return;
        }
        
        if (!_playerInventory.TryRemoveResource(id))
            Debug.LogError($"Failed to remove resource: {id}");
        else
            Debug.Log($"Removing {id} from player inventory.");
    }
}
