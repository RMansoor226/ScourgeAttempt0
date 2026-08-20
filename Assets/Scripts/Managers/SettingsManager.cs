using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField]
    private AudioMixer audioMixer;
    
    private const string MasterVolume = "MasterVolume";
    private const string MusicVolume = "MusicVolume";
    private const string SfxVolume = "SfxVolume";
    private const string UiVolume = "UiVolume";
    private const string AmbientVolume = "AmbientVolume";

    [SerializeField] 
    private Slider masterVolumeSlider;
    [SerializeField] 
    private Slider musicVolumeSlider;
    [SerializeField] 
    private Slider sfxVolumeSlider;
    [SerializeField] 
    private Slider uiVolumeSlider;
    [SerializeField] 
    private Slider ambientVolumeSlider;

    private float 
        _masterVolume, 
        _musicVolume, 
        _sfxVolume, 
        _uiVolume, 
        _ambientVolume;

    private void Awake()
    {
        if (masterVolumeSlider == null)
        {
            Debug.LogError("Master Volume Slider is not instantiated");
        }
        
        if (musicVolumeSlider == null)
        {
            Debug.LogError("Music Volume Slider is not instantiated");
        }
        
        if (sfxVolumeSlider == null)
        {
            Debug.LogError("SFX Volume Slider is not instantiated");
        }
        
        if (uiVolumeSlider == null)
        {
            Debug.LogError("UI Volume Slider is not instantiated");
        }
        
        if (ambientVolumeSlider == null)
        {
            Debug.LogError("Ambient Volume Slider is not instantiated");
        }
        
        _masterVolume = 1f;
        _musicVolume = 1f;
        _sfxVolume = 1f;
        _uiVolume = 1f; 
        _ambientVolume = 1f;
    }

    private void OnEnable()
    {
        if (masterVolumeSlider == null || musicVolumeSlider == null || sfxVolumeSlider == null || uiVolumeSlider == null || ambientVolumeSlider == null)
        {
            return;
        }
        
        masterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
        musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxVolumeSlider.onValueChanged.AddListener(SetSfxVolume);
        uiVolumeSlider.onValueChanged.AddListener(SetUiVolume);
        ambientVolumeSlider.onValueChanged.AddListener(SetAmbientVolume);
    }

    private void OnDisable()
    {
        masterVolumeSlider.onValueChanged.RemoveListener(SetMasterVolume);
        musicVolumeSlider.onValueChanged.RemoveListener(SetMusicVolume);
        sfxVolumeSlider.onValueChanged.RemoveListener(SetSfxVolume);
        uiVolumeSlider.onValueChanged.RemoveListener(SetUiVolume);
        ambientVolumeSlider.onValueChanged.RemoveListener(SetAmbientVolume);
    }
    
    public void SetMasterVolume(float sliderValue)
    {
        _masterVolume = CalculateDecibels(sliderValue, 20f);
    }
    
    public void SetMusicVolume(float sliderValue)
    {
        _musicVolume = CalculateDecibels(sliderValue, 10f);
    }
    
    public void SetSfxVolume(float sliderValue)
    {
        _sfxVolume = CalculateDecibels(sliderValue, 20f);
    }
    
    public void SetUiVolume(float sliderValue)
    {
        _uiVolume = CalculateDecibels(sliderValue, 20f);
    }
    
    public void SetAmbientVolume(float sliderValue)
    {
        _ambientVolume = CalculateDecibels(sliderValue, 20f);
    }

    private float CalculateDecibels(float percentVolume, float maxVolume)
    {
        return percentVolume <= 0.0001f 
            ? -80f
            : Mathf.Log10(percentVolume) * maxVolume;
    }

    public void ApplyVolumeSettings()
    {
        audioMixer.SetFloat(MasterVolume, _masterVolume);
        audioMixer.SetFloat(MusicVolume, _musicVolume);
        audioMixer.SetFloat(SfxVolume, _sfxVolume);
        audioMixer.SetFloat(UiVolume, _uiVolume);
        audioMixer.SetFloat(AmbientVolume, _ambientVolume);
    }
}
