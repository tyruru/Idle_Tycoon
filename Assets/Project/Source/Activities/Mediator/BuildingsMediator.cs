using System.Collections.Generic;
using UnityEngine;

public class BuildingsMediator
{
    private readonly List<IBuilding> _buildings = new List<IBuilding>();
    private PlaySoundsComponent _playSoundsComponent;
    private UpgradeWindow _upgradeWindow;
    
    public void Initialize(UpgradeWindow upgradeWindow)
    {
        _upgradeWindow = upgradeWindow;
    }
    
    public void Register(IBuilding building)
    {
        if (!_buildings.Contains(building))
            _buildings.Add(building);

        building.SetMediator(this);
    }
    
    public void Unregister(IBuilding building)
    {
        if (_buildings.Contains(building))
            _buildings.Remove(building);
    }
    
    public void ShowUpgradeWindow(IBuilding building)
    {
        _upgradeWindow.Show(building, this);
    }

    public void TryUpgrade(IBuilding building)
    {
        Debug.Log($"Mediator upgrading {building.Id}");
        var currentLevel = building.CurrentLevel;
        var def = DefsFacade.I.BuildingRepository.GetById(building.Id);
        if (currentLevel >= def.Stats.Count)
        {
            Debug.LogWarning($"Building {building.Id} is already at max level.");
            return;
        }

        var nextStats = def.Stats[currentLevel];
        
        _playSoundsComponent?.PlayOneShot("Upgrade");
        building.SetStats(nextStats, currentLevel+1);
    }

    public void CollectResources(IBuilding building)
    {
        Debug.Log($"Mediator collecting resources from {building}");
        
        var resources = building.ProducePerTime;
        
        foreach (var resource in resources)
        {
            var inventory = GameSession.I.PlayerInventory;
            inventory.AddResource(resource.Id, resource.Amount);
        }
        
        _playSoundsComponent?.PlayOneShot("Collect");
        building.OnResourcesCollected();
    }

    public void SetSounds(PlaySoundsComponent playSoundsComponent)
    {
        _playSoundsComponent = playSoundsComponent;
    }
}
