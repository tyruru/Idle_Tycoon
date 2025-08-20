using System;
using UnityEngine;

[Serializable]
public class BuildingPresenter : IBuilding
{
    private BuildingsMediator _mediator;
    private BuildingView _view;
    [SerializeField] private BuildingModel _buildingModel;

    public void Initialize(BuildingView view)
    {
        _view = view;
        
        GameSession.I.BuildingsMediator.Register(this);
    }

    public void SetMediator(BuildingsMediator mediator)
    {
        _mediator = mediator;
    }

    public string Id { get; }

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
    }
}
