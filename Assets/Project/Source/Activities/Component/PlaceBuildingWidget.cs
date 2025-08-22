using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceBuildingWidget : MonoBehaviour
{
    [SerializeField, BuildingId] private string _id;
    [SerializeField] private Image _image;
    [SerializeField] private Transform _pricesContainer;  
    [SerializeField] private PriceWidget _pricePrefab;     
    [SerializeField] private float _offset = 50f; 

    private List<PriceWidget> _priceWidgets = new ();
    private void Start()
    {
        var def = DefsFacade.I.BuildingRepository.GetById(_id);
        _image.sprite = def.Icon;
        SetPrice(def);
    }

    private void SetPrice(BuildingDef def)
    {
        foreach (Transform child in _pricesContainer)
            Destroy(child.gameObject);

        foreach (var price in def.Price)
        {
            var resource = DefsFacade.I.ResourcesRepository.GetById(price.Id);

            var widget = Instantiate(_pricePrefab, _pricesContainer);
            widget.SetData(resource.Icon, price.Amount);
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
            RectTransform rt = _priceWidgets[i].GetComponent<RectTransform>();
            float x = i * _offset - totalWidth / 2f;
            rt.anchoredPosition = new Vector2(x, 0);
        }
    }
}
