using UnityEngine;

public class CameraMovementComponent : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 10f;        
    [SerializeField] private float zoomSpeed = 5f;       
    [SerializeField] private Vector2 zoomLimits = new Vector2(5f, 20f); 

    [Header("Clamp Settings")]
    [SerializeField] private Vector2 xLimits = new Vector2(-20f, 20f); 
    [SerializeField] private Vector2 zLimits = new Vector2(-20f, 20f); 

    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        HandleMovement();
        HandleZoom();
    }

    private void HandleMovement()
    {
        // заменить но новую инпут систему
        float h = Input.GetAxis("Horizontal"); 
        float v = Input.GetAxis("Vertical");  

        Vector3 move = new Vector3(h, 0, v) * moveSpeed * Time.deltaTime;
        transform.position += move;

        // Ограничение по карте
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, xLimits.x, xLimits.y);
        pos.z = Mathf.Clamp(pos.z, zLimits.x, zLimits.y);
        transform.position = pos;
    }

    private void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            float newSize = cam.orthographicSize - scroll * zoomSpeed;
            cam.orthographicSize = Mathf.Clamp(newSize, zoomLimits.x, zoomLimits.y);
        }
    }
}
