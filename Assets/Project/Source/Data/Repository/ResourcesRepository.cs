using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Defs/Resources", fileName = "Resources")]
public class ResourcesRepository : BaseRepository<ResourcesDef>
{
    
#if UNITY_EDITOR
public List<ResourcesDef> ItemsForEditor => GetAll();
#endif
}

[Serializable]
public class ResourcesDef : IStringId
{
    [field: SerializeField] public string Id { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
    
}


