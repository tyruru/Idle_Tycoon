using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsWidget : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _value;
    
    public event Action<float> OnValueChanged;
    private void Start()
    {
        _slider.onValueChanged.AddListener(OnSliderChanged);
    }

    public void Initialize(float initialValue)
    {
        if(initialValue > 1)
            initialValue = 1;
        
        _slider.normalizedValue = initialValue;
        OnSliderChanged(initialValue);
    }
    
    private void OnSliderChanged(float newValue)
    {
        var textValue = Mathf.Round(newValue * 100);
        _value.text = textValue.ToString();
        _slider.normalizedValue = newValue;
        OnValueChanged?.Invoke(newValue);
    }

    private void OnDestroy()
    {
        _slider.onValueChanged.RemoveListener(OnSliderChanged);
    }
}
