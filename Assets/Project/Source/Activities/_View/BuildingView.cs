using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingView : MonoBehaviour
{
    [SerializeField] private Vector2Int _size = Vector2Int.one;
    [SerializeField] private Color _debugColor = new Color(1f, 0.02f, 0.97f, 0.3f);
    [SerializeField] private Renderer _renderer;
    [SerializeField] private CollectResourcesWidget _prefabWidget;
    [SerializeField] private Transform _widgetsRoot;


    [SerializeField] private BuildingModel _buildingModel; //fore Debugging
    
    private Color _defaultMaterialColor;
    private BuildingPresenter _buildingPresenter;
    private Coroutine _timerCoroutine;
    private readonly List<CollectResourcesWidget> _activeWidgets = new List<CollectResourcesWidget>();
    
    public Vector2Int Size => _size;
    public string Id;
    
    public event Action OnTimerEnd;
    
    private void Awake()
    {
        _defaultMaterialColor = _renderer.material.color;
        _buildingPresenter = new BuildingPresenter();
        _buildingPresenter.Initialize(this);
    }

    private void OnDestroy()
    {
        _buildingPresenter.Dispose();
    }

    public void StartTimer(float duration)
    {
        if (_timerCoroutine != null)
            StopCoroutine(_timerCoroutine);

        _timerCoroutine = StartCoroutine(TimerRoutine(duration));
    }
    

    public void SetTransparent(bool available)
    {
        _renderer.material.color = available ? Color.green : Color.red;
    }

    public void SetNormal()
    {
        _renderer.material.color = _defaultMaterialColor;
    }
    
    public void SetModel(BuildingModel model)
    {
        _buildingModel = model;
        _buildingPresenter.SetModel(model);
    }
    
    [ContextMenu("Call Upgrade")]
    public void CallUpgrade()
    {
        _buildingPresenter.Upgrade();
    }

    public void ShowCollectWidget(List<ResourceForDisplaying> resources)
    {
        foreach (var widget in _activeWidgets)
        {
            Destroy(widget.gameObject);
        }
        _activeWidgets.Clear();

        foreach (var resource in resources)
        {
            var widget = Instantiate(_prefabWidget, _widgetsRoot);
            widget.Show(resource.Icon, resource.Amount);
            _activeWidgets.Add(widget);
        }
        
        ArrangeWidgets();
    }
    
    public void HideWidgets()
    {
        foreach (var widget in _activeWidgets)
        {
            Destroy(widget.gameObject);
        }
        _activeWidgets.Clear();
    }
    
    private IEnumerator TimerRoutine(float duration)
    {
        yield return new WaitForSeconds(duration);
        Debug.Log("Timer ended for building: " + _buildingModel.Id);
        OnTimerEnd?.Invoke();
        _timerCoroutine = null; 
    }
    
    private void ArrangeWidgets()
    {
        float offset = 0.5f; 
        for (int i = 0; i < _activeWidgets.Count; i++)
        {
            _activeWidgets[i].transform.localPosition = new Vector3(i * offset, 0, 0);
        }
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
