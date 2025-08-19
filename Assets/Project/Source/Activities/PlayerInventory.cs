using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerInventory
{
    [SerializeField] private List<ResourceData> _resources;

    public PlayerInventory()
    {
        _resources = new List<ResourceData>();
    }
    
    public void AddResource(string resourceId, int amount = 1)
    {
        if(!IsResourceExists(resourceId))
            return;
        
        var resourceData = FindResourceInInventory(resourceId);

        if (resourceData != null)
        {
            resourceData.AddAmount(amount);
            GameSession.I.NotifyInventoryChanged();
        }
        else
        {
            var resourceDef = DefsFacade.I.ResourcesRepository.GetById(resourceId);
            var newResourceData = new ResourceData(resourceDef.Id, resourceDef.IconPath, amount);
            _resources.Add(newResourceData);
            GameSession.I.NotifyInventoryChanged();
        }
    }
    
    public bool TryRemoveResource(string resourceId, int amount = 1)
    {
        if(!IsResourceExists(resourceId))
            return false;
        
        var resourceData = FindResourceInInventory(resourceId);
        
        if(resourceData == null)
        {
            Debug.LogError($"Cannot remove resource {resourceId} because it doesn't exist in inventory");
            return false;
        }

        var isRemoving = resourceData.TryRemoveAmount(amount);
        
        if(isRemoving)
            GameSession.I.NotifyInventoryChanged();
        
        return isRemoving;
    }

    private bool IsResourceExists(string resourceId)
    {
        var resourceDef = DefsFacade.I.ResourcesRepository.GetById(resourceId);

        if (resourceDef == null)
        {
            Debug.LogError("Resource not found in repository: " + resourceId);
            return false;
        }
        
        return true;
    }
    
    private ResourceData FindResourceInInventory(string resourceId)
    {
        foreach (var resourceData in _resources)
        {
            if (resourceData.Id == resourceId)
                return resourceData;
        }

        return null;
    }
    
    
}
