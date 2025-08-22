using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PriceWidget : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _amount;

    public void SetData(Sprite icon, int amount)
    {
        _icon.sprite = icon;
        _amount.text = amount.ToString();
    }
}


