using UnityEditor;
using UnityEngine;

public class PauseScreen : MonoBehaviour
{
    [SerializeField] 
    private SettingsScreen settingsScreen;
    
    private bool _pauseScreenVisible;

    private void Awake()
    {
        _pauseScreenVisible = false;
    }

    public void SetPauseScreen(bool isVisible)
    {
        _pauseScreenVisible = isVisible;
        gameObject.SetActive(_pauseScreenVisible);
    }

    public void SwitchToSettings()
    {
        SetPauseScreen(false);
        settingsScreen.SetSettingsScreen(true);
    }
}
