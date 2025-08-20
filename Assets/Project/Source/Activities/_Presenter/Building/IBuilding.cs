
public interface IBuilding
{
    public string Id { get; }
    public void Execute();
    public void SetMediator(BuildingsMediator mediator);
    public int CurrentLevel { get; }
    void SetStats(BuildingStats nextStats, int currentLevel);
}
