using TMPro;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private RectTransform _healthBar;
    private float _maxWidth = 400f;
    
    private void Awake()
    {
        _healthBar = GetComponent<RectTransform>();
    }

    public void UpdateHealthBar(float percentHealthRemaining)
    {
        //Debug.Log($"Updating health bar size to {percentHealthRemaining}%");
        _healthBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _maxWidth * percentHealthRemaining);
    }
}
