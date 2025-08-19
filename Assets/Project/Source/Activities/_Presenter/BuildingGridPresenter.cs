
using System.Collections.Generic;
using UnityEngine;

public class BuildingGridPresenter
{
    private readonly BuildingGridView _view;
    private readonly List<Building> _placedBuildings = new List<Building>();
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

    private void OnBuildingPlaced(Building building)
    {
        _placedBuildings.Add(building);
        SaveBuildings();
    }
    
    private void SaveBuildings()
    {
        var data = new BuildingsSaveData();

        foreach (var building in _placedBuildings)
        {
            var bData = new BuildingData
            {
                Id = building.Id,
                Position = building.transform.position,
                Size = building.Size
            };
            data.Buildings.Add(bData);
        }

        JsonBuildingGridSaver.Save(data);
    }

    private void LoadBuildings()
    {
        var data = JsonBuildingGridSaver.Load();

        foreach (var bData in data.Buildings)
        {
            // ищем def по id
            var def = _buildingRepository.GetById(bData.Id);
            if (def == null)
            {
                Debug.LogWarning($"BuildingDef not found: {bData.Id}");
                continue;
            }

            var prefab = DefsFacade.I.BuildingRepository.GetById(bData.Id).Prefab;
            if (prefab == null)
            {
                Debug.LogWarning($"Prefab not found at path: {def.Prefab}");
                continue;
            }

            var building = _view.Create(prefab, bData.Position, Quaternion.identity);
            
            _placedBuildings.Add(building);
        }
    }
}
