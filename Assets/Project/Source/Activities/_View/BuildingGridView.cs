using System;
using UnityEngine;

public class BuildingGridView : MonoBehaviour
{
    [SerializeField] private Vector2Int _gridSize = new Vector2Int(10, 10);
    [SerializeField] private Transform _buildingsRoot;
    
    private BuildingView _flyingBuildingView;
    private BuildingView[,] _grid;
    private Camera _mainCamera;
    private bool _available = true;
    
    public string Id { get; private set; }
    public event Action<BuildingView> OnBuildingPlaced;
    private void Awake()
    {
        _grid = new BuildingView[_gridSize.x, _gridSize.y]; 
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if(!_flyingBuildingView)
            return;
        
        var groundPlane = new Plane(Vector3.up, transform.position);
        var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        
        if (groundPlane.Raycast(ray, out float position))
        {
            Vector3 worldPosition = ray.GetPoint(position);

            int x = Mathf.RoundToInt(worldPosition.x);
            int y = Mathf.RoundToInt(worldPosition.z);

            _available = IsAvailable(x, y);
            if(_available && IsPlaceTaken(x, y))
                _available = false;
            
            
            _flyingBuildingView.transform.position = new Vector3(x, 0, y);
            _flyingBuildingView.SetTransparent(_available);

            if (InputService.I.Actions.Building.Cancel.WasPressedThisFrame())
            {
                if (_flyingBuildingView)
                    Destroy(_flyingBuildingView.gameObject);
            }
            
            if (_available && InputService.I.Actions.Building.Approve.WasPressedThisFrame())
            {
                if (!BuyBuildingManager.BuyBuilding(_flyingBuildingView)) 
                    return;
                
                PlaceBuilding(x, y);
                Debug.Log("Building placed at: " + x + ", " + y);
            }
            
        }
    }
    
    public void StartPlacingBuilding(BuildingView buildingViewPrefab)
    {
        if (_flyingBuildingView != null)
        {
            Destroy(_flyingBuildingView.gameObject);
        }
        Id = buildingViewPrefab.Id;
        _flyingBuildingView = Instantiate(buildingViewPrefab, _buildingsRoot);
    }

    private bool IsPlaceTaken(int placeX, int placeY)
    {
        for (int x = 0; x < _flyingBuildingView.Size.x; x++)
        {
            for (int y = 0; y < _flyingBuildingView.Size.y; y++)
            {
                if(_grid[placeX + x, placeY + y])
                    return true;
            }
        }
        
        return false;
    }
    
    public void PlaceBuilding(int placeX, int placeY)
    {
        try
        {
            for (int x = 0; x < _flyingBuildingView.Size.x; x++)
            {
                for (int y = 0; y < _flyingBuildingView.Size.y; y++)
                {
                    _grid[placeX + x, placeY + y] = _flyingBuildingView;
                }
            }
        
            _flyingBuildingView.SetNormal();
            OnBuildingPlaced?.Invoke(_flyingBuildingView);       
            _flyingBuildingView = null;
        }
        catch (Exception e)
        {
            Debug.LogWarning("Building put of range");
        }
       
    }

    private bool IsAvailable(int x, int y)
    {
        Debug.Log("Is available X: " + x + ",Is available Y: " + y);

        if (x < 0 || x >  _gridSize.x - _flyingBuildingView.Size.x)
            return false;
        if (y < 0 || y >  _gridSize.y - _flyingBuildingView.Size.y)
            return false;

        return true;
    }

    private void OnDrawGizmos()
    {
        if (_gridSize.x <= 0 || _gridSize.y <= 0)
            return;

        Vector3 origin = transform.position;

        Gizmos.color = Color.red;
        Vector3 cellSize = new Vector3(1, 0.01f, 1);

        for (int x = 0; x < _gridSize.x; x++)
        {
            for (int y = 0; y < _gridSize.y; y++)
            {
                Vector3 cellCorner = origin + new Vector3(x, 0, y);

                Gizmos.DrawWireCube(cellCorner, cellSize);
            }
        }
    }

    public BuildingView Create(BuildingView buildingView, Vector3 bDataPosition, Quaternion quarterion)
    {
        var instance =  Instantiate(buildingView, bDataPosition, quarterion, _buildingsRoot);
        for (int x = 0; x < buildingView.Size.x; x++)
        {
            for (int y = 0; y < buildingView.Size.y; y++)
            {
                _grid[(int)bDataPosition.x + x, (int)bDataPosition.z + y] = buildingView;
            }
        }

        return instance;
    }
}