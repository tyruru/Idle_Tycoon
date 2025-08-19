using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Defs/Buildings", fileName = "Buildings")]

public class BuildingRepository : BaseRepository<BuildingDef>
{
    
}

[Serializable]
public class BuildingDef : IStringId
{
    [field: SerializeField] public string Id { get; private set; }
    [field: SerializeField] public Building Prefab { get; private set; }
    [field: SerializeField] public List<PriceDef> Price { get; private set; }
}

[Serializable]
public class PriceDef : IStringId
{
    [field: SerializeField, ResourcesId] public string Id { get; private set; }
    [field: SerializeField] public int Amount { get; private set; }
}