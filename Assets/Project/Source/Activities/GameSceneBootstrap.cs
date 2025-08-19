using UnityEngine;

public class GameSceneBootstrap : MonoBehaviour
{
    [SerializeField] private BuildingGridView _buildingGridView;
    private void Awake()
    {
        
    }

    private void Start()
    {
        var presenter = new BuildingGridPresenter(_buildingGridView);
        presenter.Initialize();
        
        GameSession.I.BuildingGridPresenter = presenter;
    }
}
