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
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public int CurrentLevel { get; private set; }
    [field: SerializeField] public BuildingView Prefab { get; private set; }
    [field: SerializeField] public List<PriceDef> Price { get; private set; }
    [field: SerializeField] public List<BuildingStats> Stats { get; private set; }
}

[Serializable]
public class PriceDef : IStringId
{
    [field: SerializeField, ResourcesId] public string Id { get; private set; }
    [field: SerializeField] public int Amount { get; private set; }
}

[Serializable]
public class ProducePerTimeDef : IStringId
{
    [field: SerializeField, ResourcesId] public string Id { get; private set; }
    [field: SerializeField] public int Amount { get; private set; }
}

[Serializable]
public class BuildingStats
{
    [field: SerializeField] public int Level { get; private set; }
    [field: SerializeField] public List<ProducePerTimeDef> ProducePerTime { get; private set; }
    [field: SerializeField] public int SecondsForProduce { get; private set; }
}