using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BuildingModel
{
    public BuildingModel(string id, Vector3 position, Vector2Int size,
        List<PriceDef> price, BuildingStats stats, int currentLevel)
    {
        Id = id;
        Position = position;
        Size = size;
        Price = price;
        Stats = stats;
        CurerntLevel = currentLevel;
    }

    public string Id;
    public Vector3 Position;
    public Vector2Int Size;
    public int CurerntLevel;
    public List<PriceDef> Price;
    public BuildingStats Stats;
}

[Serializable]
public class BuildingsSaveData
{
    public List<BuildingModel> Buildings = new List<BuildingModel>();
}