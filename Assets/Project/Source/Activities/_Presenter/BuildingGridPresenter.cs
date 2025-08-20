using System.Collections.Generic;
using UnityEngine;

public class BuildingGridPresenter
{
    private readonly BuildingGridView _view;
    private readonly List<BuildingModel> _placedBuildings = new List<BuildingModel>();
    private readonly BuildingRepository _buildingRepository;
    
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
            settings: def.Settings
        );
        
        _placedBuildings.Add(model);
        buildingView.SetModel(model);
        SaveBuildings();
    }
    
    private void SaveBuildings()
    {
        var data = new BuildingsSaveData();

        foreach (var building in _placedBuildings)
        {
            var bData = new BuildingModel(
                id: building.Id,
                position: building.Position,
                size: building.Size,
                price: building.Price,
                settings: building.Settings
            );
            
            data.Buildings.Add(bData);
        }

        JsonBuildingGridSaver.Save(data);
    }

    private void LoadBuildings()
    {
        var data = JsonBuildingGridSaver.Load();

        foreach (var model in data.Buildings)
        {
            if (model == null || string.IsNullOrEmpty(model.Id))
            {
                Debug.LogWarning("Invalid building model found in save data.");
                continue;
            }
            var view = DefsFacade.I.BuildingRepository.GetById(model.Id).Prefab;

            var instance = _view.Create(view, model.Position, Quaternion.identity);
            instance.SetModel(model);
            
            _placedBuildings.Add(model);
        }
    }
}
