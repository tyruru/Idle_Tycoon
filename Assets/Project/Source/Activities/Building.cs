using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private Vector2Int _size = Vector2Int.one;
    [SerializeField] private Color _debugColor = new Color(1f, 0.02f, 0.97f, 0.3f);
    [SerializeField] private Renderer _renderer;
    
    private Color _defaultMaterialColor;
    public Vector2Int Size => _size;

    private void Awake()
    {
        _defaultMaterialColor = _renderer.material.color;
    }

    public void SetTransparent(bool available)
    {
        _renderer.material.color = available ? Color.green : Color.red;
    }

    public void SetNormal()
    {
        _renderer.material.color = _defaultMaterialColor;
    }
    
    private void OnDrawGizmos()
    {
        for (int x = 0; x < _size.x; x++)
        {
            for (int y = 0; y < _size.y; y++)
            {
                Gizmos.color = _debugColor;
                Gizmos.DrawCube(transform.position + new Vector3(x, 0, y), new Vector3(1, .1f, 1));
            }
        }
    }
}
