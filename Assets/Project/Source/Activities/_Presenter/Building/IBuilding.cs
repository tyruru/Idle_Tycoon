
using System.Collections.Generic;

public interface IBuilding
{
    public string Id { get; }
    public void SetMediator(BuildingsMediator mediator);
    public int CurrentLevel { get; }
    List<ProducePerTimeDef> ProducePerTime { get; }
    
    void SetStats(BuildingStats nextStats, int currentLevel);
    void OnResourcesCollected();
}
