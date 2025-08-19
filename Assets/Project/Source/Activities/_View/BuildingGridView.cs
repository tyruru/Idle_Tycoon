using System;
using UnityEngine;

public class BuildingGridView : MonoBehaviour
{
    [SerializeField] private Vector2Int _gridSize = new Vector2Int(10, 10);
    
    private Building _flyingBuilding;
    private Building[,] _grid;
    private Camera _mainCamera;
    private bool _available = true;
    
    public event Action<Building> OnBuildingPlaced;
    private void Awake()
    {
        _grid = new Building[_gridSize.x, _gridSize.y]; 
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if(!_flyingBuilding)
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
            
            
            _flyingBuilding.transform.position = new Vector3(x, 0, y);
            _flyingBuilding.SetTransparent(_available);
            
            if (_available && Input.GetMouseButtonDown(0))
            {
                if (!BuyBuildingManager.BuyBuilding(_flyingBuilding)) 
                    return;
                
                PlaceBuilding(x, y);
                Debug.Log("Building placed at: " + x + ", " + y);
            }
            
        }
    }
    
    public void StartPlacingBuilding(Building buildingPrefab)
    {
        if (_flyingBuilding != null)
        {
            Destroy(_flyingBuilding.gameObject);
        }
        
        _flyingBuilding = Instantiate(buildingPrefab);
    }

    private bool IsPlaceTaken(int placeX, int placeY)
    {
        for (int x = 0; x < _flyingBuilding.Size.x; x++)
        {
            for (int y = 0; y < _flyingBuilding.Size.y; y++)
            {
                if(_grid[placeX + x, placeY + y])
                    return true;
            }
        }
        
        return false;
    }
    
    public void PlaceBuilding(int placeX, int placeY)
    {
        for (int x = 0; x < _flyingBuilding.Size.x; x++)
        {
            for (int y = 0; y < _flyingBuilding.Size.y; y++)
            {
                _grid[placeX + x, placeY + y] = _flyingBuilding;
            }
        }
        
        _flyingBuilding.SetNormal();
        OnBuildingPlaced?.Invoke(_flyingBuilding);       
        _flyingBuilding = null;
    }

    private bool IsAvailable(int x, int y)
    {
        Debug.Log("X: " + x + ", Y: " + y);

        if (x < transform.position.x || x > transform.position.x + _gridSize.x - _flyingBuilding.Size.x)
            return false;
        if (y < transform.position.y || y > transform.position.y + _gridSize.y - _flyingBuilding.Size.y)
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

    public Building Create(Building building, Vector3 bDataPosition, Quaternion identity)
    {
        var instance =  Instantiate(building, bDataPosition, identity);
        for (int x = 0; x < building.Size.x; x++)
        {
            for (int y = 0; y < building.Size.y; y++)
            {
                _grid[(int)bDataPosition.x + x, (int)bDataPosition.z + y] = building;
            }
        }

        return instance;
    }
}