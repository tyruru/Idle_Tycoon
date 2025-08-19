using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BuildingData
{
    public string Id;
    public Vector3 Position;
    public Vector2Int Size;
   
}

[Serializable]
public class BuildingsSaveData
{
    public List<BuildingData> Buildings = new List<BuildingData>();
}