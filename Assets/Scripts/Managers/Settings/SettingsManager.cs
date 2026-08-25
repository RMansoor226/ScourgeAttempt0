using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField]
    private AudioMixer audioMixer;

    [Header("Volume Sliders")]
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
    
    private const string MasterVolume = "MasterVolume";
    private const string MusicVolume = "MusicVolume";
    private const string SfxVolume = "SfxVolume";
    private const string UiVolume = "UiVolume";
    private const string AmbientVolume = "AmbientVolume";
    
    private float 
        _masterVolume, 
        _musicVolume, 
        _sfxVolume, 
        _uiVolume, 
        _ambientVolume;

    [Header("Video Settings")]
    [SerializeField] 
    private TMP_Dropdown resolutionDropdown;
    [SerializeField]
    private Toggle fullscreenCheckbox;
    
    private bool _fullScreenEnabled;
    
    private Resolution[] _resolutions;
    private List<Resolution> _filteredResolutions = new List<Resolution>();
    private List<string> _filteredOptions = new List<string>();
    
    private int _pendingResolutionOptionIndex;
    private int _currentResolutionOptionIndex = 0;

    [Header("Controls Settings")] 
    [SerializeField]
    private Slider sensitivitySlider;
    [SerializeField] 
    private PlayerLook playerLook;

    private float _baseSensitivity;
    private float _currentSensitivity;
    private float _pendingSensitivity;

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

        if (resolutionDropdown == null)
        {
            Debug.LogError("Resolution Dropdown is not instantiated");
        }

        if (fullscreenCheckbox == null)
        {
            Debug.LogError("Fullscreen Checkbox is not instantiated");
        }

        _masterVolume = 1f;
        _musicVolume = 0.5f;
        _sfxVolume = 0.75f;
        _uiVolume = 1f; 
        _ambientVolume = 1f;

        _fullScreenEnabled = Screen.fullScreen;
        fullscreenCheckbox.isOn = _fullScreenEnabled;
        //Debug.Log($"Fullscreen enabled is {_fullScreenEnabled}");

        _pendingResolutionOptionIndex = 0;
        _currentResolutionOptionIndex = 0;
        InitializeResolutionOptions();

        _baseSensitivity = 100f;
        sensitivitySlider.value = 0.5f;
        _pendingSensitivity = sensitivitySlider.value * _baseSensitivity;
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

    private void ApplyVolumeSettings()
    {
        audioMixer.SetFloat(MasterVolume, _masterVolume);
        audioMixer.SetFloat(MusicVolume, _musicVolume);
        audioMixer.SetFloat(SfxVolume, _sfxVolume);
        audioMixer.SetFloat(UiVolume, _uiVolume);
        audioMixer.SetFloat(AmbientVolume, _ambientVolume);
        
        // Debug.Log("Audio Settings Applied!");
    }

    private void ApplyVideoSettings()
    {
        Resolution resolution = _filteredResolutions[_pendingResolutionOptionIndex];

        Screen.SetResolution(
            resolution.width, 
            resolution.height, 
            Screen.fullScreenMode
        );
        
        // Debug.Log($"Resolution is {resolution.width} x {resolution.height}");
        
        Screen.fullScreen = _fullScreenEnabled;

        _currentResolutionOptionIndex = _pendingResolutionOptionIndex;
    }
    
    private void ApplyControlsSettings()
    {
        _currentSensitivity = _pendingSensitivity;
        
        // Debug.Log($"Sensitivity is now = {_currentSensitivity}");
        
        playerLook.SetSensitivity(_currentSensitivity);
        // Debug.Log("Controls Settings Applied!");
    }
    
    private void ApplyGameSettings()
    {
        Debug.Log("Game Settings Applied!");
    }

    public void ApplySettings(SettingsCategory category)
    {
        switch (category)
        {
            case SettingsCategory.Audio:
                ApplyVolumeSettings();
                break;
            case SettingsCategory.Video:
                ApplyVideoSettings();
                break;
            case SettingsCategory.Controls:
                ApplyControlsSettings();
                break;
            case SettingsCategory.Game:
                ApplyGameSettings();
                break;
            default:
                Debug.LogError("Invalid Settings Category Reached");
                break;
        }
    }

    public void ToggleFullScreen()
    {
        _fullScreenEnabled = fullscreenCheckbox.isOn;
    }

    public void SetResolution(int index)
    {
        _pendingResolutionOptionIndex = index;
    }
    
    private void InitializeResolutionOptions()
    {
        resolutionDropdown.ClearOptions();
        _resolutions = Screen.resolutions;

        FilterResolutionOptions();

        _pendingResolutionOptionIndex = _currentResolutionOptionIndex;
        
        resolutionDropdown.AddOptions(_filteredOptions);
        resolutionDropdown.value = _currentResolutionOptionIndex;
        resolutionDropdown.RefreshShownValue();
        
        //Debug.Log("Resolutions Initialized!");

        resolutionDropdown.onValueChanged.AddListener(SetResolution);
    }

    private void FilterResolutionOptions()
    {
        _filteredResolutions.Clear();
        _filteredOptions.Clear();

        HashSet<string> addedResolutions = new HashSet<string>();
        
        for (int i = 0; i < _resolutions.Length; i++)
        {
            float aspectRatio = 
                (float) _resolutions[i].width / _resolutions[i].height;

            if (!Mathf.Approximately(aspectRatio, 16f / 9f))
            {
                continue;
            }

            Resolution resolution = _resolutions[i];
            string option = resolution.width + " x " + resolution.height;
            
            // Debug.Log($"Adding {resolution.width} x {resolution.height}");
            
            if (!addedResolutions.Add(option))
            {
                // Debug.Log($"Rejecting duplicate entry: {option}");
                continue;
            }
            
            _filteredResolutions.Add(resolution);
            _filteredOptions.Add(option);
            
            int filteredIndex = _filteredResolutions.Count - 1;
            
            if (resolution.width == Screen.width &&
                resolution.height == Screen.height)
            {
                _currentResolutionOptionIndex = filteredIndex;
            }
        }
    }

    public void SetPendingSensitivity(float sliderValue)
    {
        // Debug.Log($"Slider value is {sliderValue}");
        float sensitivity = _baseSensitivity * sliderValue;
        _pendingSensitivity = sensitivity;
    }
}