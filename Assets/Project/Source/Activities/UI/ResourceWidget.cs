using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceWidget : MonoBehaviour
{
    [SerializeField, ResourcesId] string _resourceId;
    [SerializeField] TextMeshProUGUI _amountText;
    [SerializeField]private Image _image;

    private PlayerInventoryPresenter _inventoryPresenter;
    private void Start()
    {
        _inventoryPresenter = GameSession.I.PlayerInventory;
        _inventoryPresenter.OnChanged += UpdateView;
        
        UpdateView();
    }

    private void UpdateView()
    {
        var resourceData = _inventoryPresenter.FindResourceInInventory(_resourceId);
        _amountText.text = resourceData == null ? "0" : resourceData.Amount.ToString();
    }

    private void OnDestroy()
    {
        _inventoryPresenter.OnChanged -= UpdateView;
    }
}
