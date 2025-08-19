using System;
using UnityEngine;

[Serializable]
public class ResourceData
{
    [SerializeField] private string _id;  
    [SerializeField] private Sprite _icon;
    [SerializeField] private int _amount;

    public string Id
    {
        get => _id;
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                Debug.LogError("Resource ID cannot be null or empty.");
                return;
            }
            _id = value;
        }
    }
    public Sprite Icon
    {
        get => _icon;
        set
        {
            if (value == null)
            {
                Debug.LogError("Resource icon cannot be null.");
                return;
            }
            _icon = value;
        }
    }

    public int Amount => _amount;
    
    public ResourceData(string id, Sprite icon, int amount)
    {
        Id = id;
        Icon = icon;
        _amount = amount > 0 ? amount : throw new ArgumentException("Amount must be greater than zero.");
    }
    
    public void AddAmount(int amount)
    {
        if (amount <= 0)
        {
            Debug.LogError("Amount to add must be greater than zero.");
            return;
        }
        _amount += amount;
    }
    
    public bool TryRemoveAmount(int amount)
    {
        if (amount <= 0)
        {
            Debug.LogError("Amount to remove must be greater than zero.");
            return false;
        }
        if (_amount < amount)
        {
            Debug.LogError("Cannot remove more than current amount.");
            return false;
        }
        _amount -= amount;
        return true;
    }
}
