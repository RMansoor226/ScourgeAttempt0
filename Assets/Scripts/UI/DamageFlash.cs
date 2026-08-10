using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DamageFlash : MonoBehaviour
{
    private Image _damageFlash;
    private float _intensity;

    private void Awake()
    {
        _damageFlash = GetComponent<Image>();
        if (_damageFlash == null)
        {
            Debug.Log("Damage Flash is not assigned");
        }

        _intensity = 0;
    }
    
    public IEnumerator UpdateDamageFlash()
    {
        _intensity = 1f;
        
        Color currentColor = _damageFlash.color;
        currentColor.a = _intensity;
        _damageFlash.color = currentColor;
        
        float duration = 1f;
        float elapsed = 0f;

        Debug.Log("Flash starts");

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            
            float alpha = Mathf.Lerp(_intensity, 0f, elapsed / duration);
            
            currentColor.a = alpha;
            _damageFlash.color = currentColor;

            yield return null; // Wait one frame
        }
        
        Debug.Log("Ending Flash!");
    }

}
