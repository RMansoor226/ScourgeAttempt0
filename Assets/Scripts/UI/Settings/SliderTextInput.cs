using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderTextInput : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    private TMP_InputField _inputField;

    private void Awake()
    {
        _inputField = GetComponent<TMP_InputField>();

        if (slider == null)
        {
            Debug.LogError("Slider is not initialized!");
        }

        if (_inputField == null)
        {
            Debug.LogError("Input Field is not initialized!");
        }
    }

    private void OnEnable()
    {
        if (slider == null || _inputField == null)
        {
            return;
        }
        
        slider.onValueChanged.AddListener(OnSliderValueChanged);
        _inputField.onEndEdit.AddListener(OnInputFieldEndEdit);
        
        ChangeInputFieldText(slider.value);
    }

    private void OnDisable()
    {
        slider.onValueChanged.RemoveListener(OnSliderValueChanged);
        _inputField.onEndEdit.RemoveListener(OnInputFieldEndEdit);
    }

    private void OnInputFieldEndEdit(string text)
    {
        if (float.TryParse(text, out float result))
        {
            float clampedValue = Mathf.Clamp(result / 100, slider.minValue, slider.maxValue);

            slider.value = clampedValue;
            
            ChangeInputFieldText(clampedValue);
        }
        else
        {
            Debug.LogError("Invalid input entered!");
            ChangeInputFieldText(slider.value);
        }
    }

    private void OnSliderValueChanged(float value)
    {
        ChangeInputFieldText(value);
    }

    private void ChangeInputFieldText(float value)
    {
        _inputField.text = (value * 100).ToString("F0") + "%";
    }
}
