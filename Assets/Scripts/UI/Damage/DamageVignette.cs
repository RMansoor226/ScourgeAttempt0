using UnityEngine;
using UnityEngine.UI;

public class DamageVignette : MonoBehaviour
{
    private Image _damageVignette;
    private float _intensity;

    private void Awake()
    {
        _damageVignette = GetComponent<Image>();
        if (_damageVignette == null)
        {
            Debug.Log("Damage Vignette is not assigned");
        }

        _intensity = 0;
    }

    public void UpdateVignetteIntensity(float percentHealth)
    {
        _intensity = 1f - percentHealth;
        
        //Debug.Log($"Updating Vignette intensity to {_intensity}%");
        
        Color currentColor = _damageVignette.color;
        currentColor.a = _intensity;
        _damageVignette.color = currentColor;
    }
}
