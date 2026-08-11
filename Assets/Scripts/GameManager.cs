using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Player player;
    [SerializeField] 
    private DeathScreen deathScreen;

    private bool _isPlayerDead;
    
    private void Start()
    {
        player.Initialize();
    }

    public void PlayerDied(
        PlayerInputHandler playerInputs,
        PlayerLook playerLook,
        PlayerMovement playerMovement,
        PlayerWeaponController playerWeapons,
        PlayerDamageFlinch playerFlinch)
    {
        if (_isPlayerDead)
        {
            return;
        }
        _isPlayerDead = true;
        
        // Disable all player-controllable gameplay elements 
        playerInputs.enabled = false;
        playerLook.enabled = false;
        playerMovement.enabled = false;
        playerWeapons.enabled = false;
        playerFlinch.enabled = false;
        
        // Debug.Log("Everything is disabled!");
        
        deathScreen.ToggleDeathScreen();
        StopAllZombies();
    }

    public void StopAllZombies()
    {
        ZombieAI[] zombies = FindObjectsByType<ZombieAI>(
            FindObjectsSortMode.None
        );

        foreach (ZombieAI zombie in zombies)
        {
            zombie.EnterIdleState();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
