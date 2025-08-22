using System.Collections.Generic;
using UnityEngine;

public class BuildingGridPresenter
{
    private readonly BuildingGridView _view;
    private readonly BuildingRepository _buildingRepository;
    
    public readonly List<BuildingModel> PlacedBuildings = new List<BuildingModel>();

    private readonly JsonBuildingGridSaver _jsonBuildingGridSaver = new();
    public BuildingGridPresenter(BuildingGridView gridView)
    {
        _view = gridView;
        _view.OnBuildingPlaced += OnBuildingPlaced;
        
        var repository = DefsFacade.I.BuildingRepository;
        _buildingRepository = repository;
        
        
    }

    public void Initialize()
    {
        LoadBuildings();
    }

    public void StartPlacingBuilding(string id)
    {
        var buildingPrefab = DefsFacade.I.BuildingRepository.GetById(id).Prefab;
        buildingPrefab.Id = id;

        _view.StartPlacingBuilding(buildingPrefab);
    }

    private void OnBuildingPlaced(BuildingView buildingView)
    {
        var def = _buildingRepository.GetById(buildingView.Id);
        var model = new BuildingModel(
            id: def.Id,
            position: buildingView.transform.position,
            size: buildingView.Size,
            price: def.Price,
            stats: def.Stats[def.CurrentLevel-1],
            currentLevel: def.CurrentLevel
        );
        
        PlacedBuildings.Add(model);
        buildingView.SetModel(model);
        SaveBuildings();
    }
    
    private void SaveBuildings()
    {
        var data = new BuildingsSaveData
        {
            Buildings = PlacedBuildings
        };

        _jsonBuildingGridSaver.Save(data);
    }

    private void LoadBuildings()
    {
        var data = _jsonBuildingGridSaver.Load();

        foreach (var model in data.Buildings)
        {
            if (model == null || string.IsNullOrEmpty(model.Id))
            {
                Debug.LogWarning("Invalid building model found in save data.");
                continue;
            }
            var view = DefsFacade.I.BuildingRepository.GetById(model.Id).Prefab;

            var instance = _view.Create(view, model.Position, Quaternion.Euler(0, 180, 0));
            instance.SetModel(model);
            
            PlacedBuildings.Add(model);
        }
    }
}
