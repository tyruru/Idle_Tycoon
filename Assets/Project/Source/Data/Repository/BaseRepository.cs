using System.Collections.Generic;
using UnityEngine;

public abstract class BaseRepository<T> : ScriptableObject, IRepository<T> where T : IStringId
{
   [SerializeField] protected T[] _collection;

#if UNITY_EDITOR
   public List<T> ItemsForEditor => GetAll();
#endif
   
   public List<T> GetAll() => new List<T>(_collection);
    
    public T GetById(string id)
    {
        foreach (var item in GetAll())
        {
            if (item.Id == id) return item;
        }

        return default(T);
    }

}
