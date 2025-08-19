using System;
using UnityEngine;

[Serializable]
public class ResourceData
{
    [SerializeField] private string _id; 
    [SerializeField] private string _iconPath;
    [SerializeField] private int _amount;

    private Sprite _icon;
    
    public string Id
    {
        get => _id;
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                Debug.LogError("Resource Id cannot be null or empty.");
                return;
            }
            _id = value;
        }
    }
    public Sprite Icon
    {
        get
        {
            if (_icon == null && !string.IsNullOrEmpty(_iconPath))
            {
                _icon = Resources.Load<Sprite>(_iconPath);
                if (_icon == null)
                    Debug.LogError($"Sprite not found at path: {_iconPath}");
            }
            return _icon;
        }
    }

    public string IconPath
    {
        get => _iconPath;
        set => _iconPath = value;
    }
    
    public int Amount => _amount;
    
    public ResourceData(string id, string iconPath, int amount)
    {
        Id = id;
        _iconPath = iconPath;
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
