using UnityEngine;

[CreateAssetMenu(menuName = "Defs/DefsFacade", fileName = "DefsFacade")]
public class DefsFacade : ScriptableObject
{
    [SerializeField] private ResourcesRepository _resourcesRepository;
    
    private static DefsFacade _instance;
    
    public static DefsFacade I => _instance == null ? LoadDefs() : _instance;
    
    public ResourcesRepository ResourcesRepository => _resourcesRepository;
    
    private static DefsFacade LoadDefs()
    {
        return _instance = Resources.Load<DefsFacade>("DefsFacade");
    }
    
    
}
