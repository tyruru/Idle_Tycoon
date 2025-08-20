using System.Collections.Generic;
using UnityEngine;

public class BuildingsMediator
{
    private readonly List<IBuilding> _buildings = new List<IBuilding>();

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

    public void Notify(IBuilding sender, string action)
    {
        Debug.Log($"Mediator: received '{action}' from {sender}");

        switch (action)
        {
            case "Produce":
                CollectResources(sender);
                break;
            case "Upgrade":
                TryUpgrade(sender);
                break;
        }
    }

    private void TryUpgrade(IBuilding building)
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
        
        building.SetStats(nextStats, currentLevel+1);
    }
    
    private void CollectResources(IBuilding building)
    {
        Debug.Log($"Mediator collecting resources from {building}");
        
        var resources = building.ProducePerTime;
        
        foreach (var resource in resources)
        {
            var inventory = GameSession.I.PlayerInventory;
            inventory.AddResource(resource.Id, resource.Amount);
        }
        
        building.OnResourcesCollected();
    }
}
