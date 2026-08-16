using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] 
    private PauseScreen pauseScreen;
    
    public bool IsPaused { get; private set; }
    
    public void Pause()
    {
        if (IsPaused)
        {
            Debug.Log("Can't pause when paused");
            return;
        }
        
        IsPaused = true;
        Time.timeScale = 0f;
        
        pauseScreen.SetPauseScreen(true);
        ToggleCursor(true);
    }

    public void Resume()
    {
        if (!IsPaused)
        {
            Debug.Log("Can't resume when not paused");
            return;
        }
        
        IsPaused = false;
        Time.timeScale = 1f;
        
        pauseScreen.SetPauseScreen(false);
        ToggleCursor(false);
    }
    
    private void ToggleCursor(bool cursorVisible)
    {
        Cursor.lockState =
            (Cursor.lockState == CursorLockMode.Locked) ? 
                CursorLockMode.None : 
                CursorLockMode.Locked;

        Cursor.visible = cursorVisible;
    }
}
