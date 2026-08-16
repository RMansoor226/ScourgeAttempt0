using UnityEngine;

public class SettingsScreen : MonoBehaviour
{
    private bool _settingsVisible;

    private void Awake()
    {
        _settingsVisible = false;
    }

    public void SetSettingsScreen(bool isVisible)
    {
        _settingsVisible = isVisible;
        gameObject.SetActive(_settingsVisible);
    }
}
