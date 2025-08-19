using System;
using UnityEngine;

public class PlaceBuildingButtonCommand : ButtonCommand
{
    [SerializeField, BuildingId] string _buildingId;

    private void Start()
    {
        CheckResources(null);
        GameSession.I.OnInventoryChanged += CheckResources;
    }

    public override void Execute()
    {
        if (!BuyBuildingManager.IsEnoughResources(DefsFacade.I.BuildingRepository.GetById(_buildingId).Price))
        {
            Debug.LogError($"Buy building {_buildingId} is not enough resources.");
            return;
        }
        GameSession.I.BuildingGridPresenter.StartPlacingBuilding(_buildingId);
    }

    private void CheckResources(PlayerInventoryPresenter obj)
    {
        var isEnough = BuyBuildingManager.IsEnoughResources(DefsFacade.I.BuildingRepository.GetById(_buildingId).Price);
        
        _button.interactable = isEnough;
    }
    
    private void OnDestroy()
    {
        GameSession.I.OnInventoryChanged -= CheckResources;
    }
}
