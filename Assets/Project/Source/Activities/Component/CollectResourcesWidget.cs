using TMPro;
using UnityEngine;

public class CollectResourcesWidget : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private TextMeshPro _amountText;
    
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    
    public void Show(Sprite icon, int amount)
    {
        _spriteRenderer.sprite = icon;
        _amountText.text = amount.ToString();
        
        gameObject.SetActive(true);
    }
}
