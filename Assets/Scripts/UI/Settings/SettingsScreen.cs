using UnityEngine;

public enum SettingsCategory
{
    Audio,
    Video,
    Controls,
    Game
}

public class SettingsScreen : MonoBehaviour
{
    private bool _settingsVisible = false;
    private bool _isInitialized = false;
    private SettingsCategory _currentSettingsCategory;
    private Transform _categoryScreens;
    private Transform _currentCategoryScreen;

    [SerializeField]
    private PauseScreen pauseScreen;

    private void Awake()
    {
        if (!_isInitialized)
        {
            _categoryScreens = transform.Find("Category Screens");
            _currentCategoryScreen = _categoryScreens.Find("Audio");
            _isInitialized = true;
        }
    }

    public void SetSettingsScreen(bool isVisible)
    {
        _settingsVisible = isVisible;
        gameObject.SetActive(_settingsVisible);
        _categoryScreens.gameObject.SetActive(_settingsVisible); // Enable Settings Category Screens
        _categoryScreens.Find("Audio").gameObject.SetActive(_settingsVisible);
    }

    private void SwitchCurrentSettingsScreen(SettingsCategory category)
    {
        if (category == _currentSettingsCategory)
        {
            return;
        }

        _currentCategoryScreen.gameObject.SetActive(false);
        //Debug.Log($"Deactivated Component: {_currentCategoryScreen.transform.name}");
        
        Transform newCategoryScreen = _categoryScreens.GetChild(category.GetHashCode());
        newCategoryScreen.gameObject.SetActive(true);

        _currentSettingsCategory = category;
        _currentCategoryScreen = newCategoryScreen;
    }

    public void SwitchToAudioCategory()
    {
        SwitchCurrentSettingsScreen(SettingsCategory.Audio);
    }
    
    public void SwitchToVideoCategory()
    {
        SwitchCurrentSettingsScreen(SettingsCategory.Video);
    }
    
    public void SwitchToControlsCategory()
    {
        SwitchCurrentSettingsScreen(SettingsCategory.Controls);
    }
    
    public void SwitchToGameCategory()
    {
        SwitchCurrentSettingsScreen(SettingsCategory.Game);
    }

    public void ExitSettingsScreen()
    {
        gameObject.SetActive(false);
        pauseScreen.SetPauseScreen(true);
    }
}