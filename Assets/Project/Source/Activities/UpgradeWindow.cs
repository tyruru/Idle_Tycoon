using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeWindow : MonoBehaviour
{
    [SerializeField] private PriceWidget _priceWidgetPrefab;
    [SerializeField] private Transform _container;
    [SerializeField] private float _offset = 200f; 
    [SerializeField] private Button _upgradeButton;
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _showUpgradeWindowButton;
    [SerializeField] private GameObject _content;
    
    private List<PriceWidget> _priceWidgets = new List<PriceWidget>();
    
    private BuildingsMediator _mediator;
    private IBuilding _building;

    private void Start()
    {
        _upgradeButton.onClick.AddListener(Upgrade);
        CloseWindow();
        _closeButton.onClick.AddListener(CloseWindow);
        _showUpgradeWindowButton.onClick.AddListener(ShowUpgradeWindow);
        _showUpgradeWindowButton.gameObject.SetActive(false);
    }

    private void ShowUpgradeWindow()
    {
        
        _content.SetActive(true);
        
        var buildingDef = DefsFacade.I.BuildingRepository.GetById(_building.Id);
        var price = buildingDef.Price;

        if (price == null)
        {
            Debug.LogWarning("Price is null");
            return;
        }

        SetPrice(price);
    }

    private void CloseWindow()
    {
        _content.SetActive(false);
        _showUpgradeWindowButton.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _upgradeButton.onClick.RemoveListener(Upgrade);
        _closeButton.onClick.RemoveListener(CloseWindow);
        _showUpgradeWindowButton.onClick.RemoveListener(ShowUpgradeWindow);
    }
    
    public void CloseUpgradeButton()
    {
        _showUpgradeWindowButton.gameObject.SetActive(false);
        CloseWindow();
    }

    public void Show(IBuilding building, BuildingsMediator mediator)
    {
        _showUpgradeWindowButton.gameObject.SetActive(true);
       
        _building = building;
        _mediator = mediator;
    }

    public void Upgrade()
    {
        _mediator.TryUpgrade(_building);
        CloseWindow();
        _showUpgradeWindowButton.gameObject.SetActive(false);
    }
    
    private void SetPrice(List<PriceDef> priceList)
    {
        foreach (Transform child in _container)
        {
            Destroy(child.gameObject);
            _priceWidgets.Clear();
        }

        foreach (var priceDef in priceList)
        {
            var resource = DefsFacade.I.ResourcesRepository.GetById(priceDef.Id);

            var widget = Instantiate(_priceWidgetPrefab, _container);
            widget.SetData(resource.Icon, priceDef.Amount);
            _priceWidgets.Add(widget);
        }
        
        ArrangeWidgets();
    }
    
    private void ArrangeWidgets()
    {
        int count = _priceWidgets.Count;
        if (count == 0) return;
        
        float totalWidth = (count - 1) * _offset;

        for (int i = 0; i < count; i++)
        {
            var widget = _priceWidgets[i];
            if(widget == null) continue;
            RectTransform rt = widget.GetComponent<RectTransform>();
            float x = i * _offset - totalWidth / 2f;
            rt.anchoredPosition = new Vector2(x, 0);
        }
    }
    
    
    
}
