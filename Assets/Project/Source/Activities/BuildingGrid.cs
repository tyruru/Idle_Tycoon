using UnityEngine;

public class BuildingGrid : MonoBehaviour
{
    [SerializeField] private Vector2Int _gridSize = new Vector2Int(10, 10);

    private Building[,] _grid;
    private Building _flyingBuilding;
    private Camera _mainCamera;
    
    private bool _available = true;
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
                PlaceBuilding(x, y);
            }
            
        }
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
    
    private void PlaceBuilding(int placeX, int placeY)
    {
        for (int x = 0; x < _flyingBuilding.Size.x; x++)
        {
            for (int y = 0; y < _flyingBuilding.Size.y; y++)
            {
                _grid[placeX + x, placeY + y] = _flyingBuilding;
            }
        }
        
        _flyingBuilding.SetNormal();
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

    public void StartPlacingBuilding(Building buildingPrefab)
    {
        if (_flyingBuilding != null)
        {
            Destroy(_flyingBuilding.gameObject);
        }        
        _flyingBuilding = Instantiate(buildingPrefab);
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
}
