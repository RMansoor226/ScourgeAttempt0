using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private WaveManager _waveManager;
    
    [SerializeField]
    private Player player;
    
    [SerializeField] 
    private DeathScreen deathScreen;
    [SerializeField] 
    private CombatUI combatUI;
    
    [SerializeField]
    private AudioManager audioManager;

    [SerializeField] 
    private AudioClip baseGameMusic;
    [SerializeField] 
    private AudioClip roundStartMusic;
    [SerializeField] 
    private AudioClip roundEndMusic;
    
    private bool _isPlayerDead;

    private void Awake()
    {
        _waveManager = transform.parent.GetComponentInChildren<WaveManager>();
        if (_waveManager == null)
        {
            Debug.Log("Couldn't find Wave Manager");
        }
    }
    
    private void Start()
    {
        player.Initialize();

        PlayBaseMusic();

        _waveManager.OnRoundStart += PlayRoundStartMusic;
        _waveManager.OnRoundEnd += PlayRoundEndMusic;
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
        
        deathScreen.SetDeathScreen(true);
        combatUI.SetCombatUi(false);
        
        StopAllZombies();
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
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
        //Debug.Log("Restarting Game");
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void QuitGame()
    {
        //Debug.Log("Quitting Game");
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    private void PlayRoundStartMusic()
    {
        audioManager.PlayMusic(roundStartMusic, false);
    }
    
    private void PlayRoundEndMusic()
    {
        audioManager.PlayMusic(roundEndMusic, false);
    }

    private void PlayBaseMusic()
    {
        audioManager.PlayMusic(baseGameMusic, true);
    }
}
