using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BuildingPresenter : IBuilding
{
    private BuildingsMediator _mediator;
    private BuildingView _view;
    [SerializeField] private BuildingModel _buildingModel;

    public string Id => _buildingModel.Id;
    public int CurrentLevel => _buildingModel.CurerntLevel;

    public void OnReadyToCollect()
    {
    }

    public void SetStats(BuildingStats nextStats, int currentLevel)
    {
        _buildingModel.Stats = nextStats;
        _buildingModel.CurerntLevel = currentLevel;
        _view.StartTimer(_buildingModel.Stats.SecondsForProduce);
    }

    public void Initialize(BuildingView view)
    {
        _view = view;
        _view.OnTimerEnd += TimerEnd;
        GameSession.I.BuildingsMediator.Register(this);
    }
    
    public void SetMediator(BuildingsMediator mediator)
    {
        _mediator = mediator;
    }
    
    public void Execute()
    {
        Debug.Log($"executed action!");
    }

    public void Produce()
    {
        _mediator.Notify(this, "Produce");
    }

    public void Upgrade()
    {
        _mediator.Notify(this, "Upgrade");
    }

    public void SetModel(BuildingModel model)
    {
        _buildingModel = model;
        _view.StartTimer(_buildingModel.Stats.SecondsForProduce);
    }

    public void Dispose()
    {
        _view.OnTimerEnd -= TimerEnd;
        GameSession.I.BuildingsMediator.Unregister(this);
    }
    
    private void TimerEnd()
    {
        var resources = new List<ResourceForDisplaying>();

        foreach (var produce in _buildingModel.Stats.ProducePerTime)
        {
            var def = DefsFacade.I.ResourcesRepository.GetById(produce.Id);
            
            resources.Add(new ResourceForDisplaying()
            {
                Name = def.Name,
                Amount = produce.Amount,
                Icon = def.Icon
            });
        }
        
        _view.ShowCollectWidget(resources);
    }

}