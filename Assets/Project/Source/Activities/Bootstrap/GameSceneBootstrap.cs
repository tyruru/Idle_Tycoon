using UnityEngine;

public class GameSceneBootstrap : MonoBehaviour
{
    [SerializeField] private BuildingGridView _buildingGridView;
    [SerializeField] private PlaySoundsComponent _playSoundsComponent;

    private void Start()
    {
        var presenter = new BuildingGridPresenter(_buildingGridView);
        presenter.Initialize();
        
        GameSession.I.BuildingGridPresenter = presenter;
        GameSession.I.BuildingsMediator.SetSounds(_playSoundsComponent);
    }
}
