using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ButtonSounds : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Button _button;

    [SerializeField]
    private AudioManager audioManager;
    [SerializeField]
    private AudioClip clickSound;
    [SerializeField]
    private AudioClip hoverOnSound;
    [SerializeField]
    private AudioClip hoverOffSound;
    
    private void Awake()
    {
        _button = GetComponent<Button>();

        if (_button != null)
        {
            _button.onClick.AddListener(OnButtonClick);
        }

        if (audioManager == null)
        {
            Debug.Log("AudioManager is not instantiated!");
        }
    }

    public void OnButtonClick()
    {
        //Debug.Log($"Button clicked: {_button.gameObject.name}");

        if (clickSound == null)
        {
            Debug.Log("No button click sound is assigned");
            return;
        }

        audioManager.PlayUiSfx(clickSound);
    }

    // Handle button hover on
    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log($"Button hovered on: {_button.gameObject.name}");
        
        if (hoverOnSound == null)
        {
            Debug.Log("No button hover on sound is assigned");
            return;
        }

        audioManager.PlayUiSfx(hoverOnSound);
    }

    // Handle button hover off
    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log($"Button hovered off: {_button.gameObject.name}");
        
        if (hoverOffSound == null)
        {
            Debug.Log("No button hover off sound is assigned");
            return;
        }

        audioManager.PlayUiSfx(hoverOffSound);
    }

    public void OnDestroy()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }
}
