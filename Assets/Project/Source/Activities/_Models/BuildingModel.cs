using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BuildingModel
{
    public BuildingModel(string id, Vector3 position, Vector2Int size, List<PriceDef> price, BuildingSettings settings)
    {
        Id = id;
        Position = position;
        Size = size;
        Price = price;
        Settings = settings;
    }

    public string Id;
    public Vector3 Position;
    public Vector2Int Size;
    public List<PriceDef> Price;
    public BuildingSettings Settings;
}

[Serializable]
public class BuildingsSaveData
{
    public List<BuildingModel> Buildings = new List<BuildingModel>();
}