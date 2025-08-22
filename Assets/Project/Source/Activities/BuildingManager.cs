using System.Collections.Generic;
using UnityEngine;

public class BuildingManager
{
    public static bool BuyBuilding(BuildingView buildingView)
    {
        var buildingDef = DefsFacade.I.BuildingRepository.GetById(buildingView.Id);
        if (buildingDef == null)
        {
            Debug.LogError($"Building with id {buildingView.Id} not found.");
            return false;
        }
        
        var priceList = buildingDef.Price;
        
        if(!IsEnoughResources(priceList))
        {
            Debug.LogError($"Not enough resources to buy building {buildingView.Id}.");
            return false;
        }

        var playerResources = GameSession.I.PlayerInventory.Resources;
        
        foreach (var item in priceList)
        {
            foreach (var resource in playerResources)
            {
                if(resource.Id == item.Id)
                {
                    GameSession.I.PlayerInventory.TryRemoveResource(item.Id, item.Amount);
                }
            }
        }
        
        return true;
    }
    
    public static bool IsEnoughResources(List<PriceDef> priceList)
    {
        var joined = new Dictionary<string, int>();

        foreach (var item in priceList)
        {
            if (joined.ContainsKey(item.Id))
                joined[item.Id] += item.Amount;
            else
                joined.Add(item.Id, item.Amount);
        }
        
        foreach (var kvp in joined)
        {
            var count = Count(kvp.Key);
            if (count < kvp.Value)
                return false;
        }

        return true;

    }
    
    private static int Count(string id)
    {
        var playerInventory = GameSession.I.PlayerInventory;

        var ob = playerInventory.FindResourceInInventory(id);
       
        return ob?.Amount ?? 0;
    }
    
}
