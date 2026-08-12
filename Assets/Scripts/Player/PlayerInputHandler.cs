using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInputActions _inputActions;
    
    [SerializeField]
    private PauseManager pauseManager;
    
    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }
    
    public bool JumpPressed { get; private set; }
    public bool SprintActive { get; private set; }
    
    public bool FireHeld { get; private set; }
    public bool ReloadPressed { get; private set; }
    public bool InteractActive { get; private set; }
    
    public bool PausedGame { get; private set; }

    private bool _gamePaused;
    
    private void Awake()
    {
        _inputActions = new PlayerInputActions();
        
        _gamePaused = false;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    private void OnEnable()
    {
        _inputActions.Player.Enable();
    }

    private void OnDisable()
    {
        _inputActions.Player.Disable();
    }

    // Update is called once per frame
    private void Update()
    {
        PausedGame = _inputActions.Player.Pause.WasPressedThisFrame();
        if (PausedGame)
        {
            PauseGame();
        }

        if (!_gamePaused)
        {
            MoveInput = _inputActions.Player.Move.ReadValue<Vector2>();
            LookInput = _inputActions.Player.Look.ReadValue<Vector2>();
        
            JumpPressed = _inputActions.Player.Jump.WasPressedThisFrame();
            SprintActive = _inputActions.Player.Sprint.IsPressed();
        
            FireHeld = _inputActions.Player.Fire.WasPressedThisFrame();
            ReloadPressed = _inputActions.Player.Reload.WasPressedThisFrame();
        }
    }

    public void PauseGame()
    {
        if (!_gamePaused)
        {
            //Debug.Log("_gamePaused was false but is now true !");
            pauseManager.Pause();
            _gamePaused = true;
        }
        else
        {
            //Debug.Log("_gamePaused was true but is now false!");
            pauseManager.Resume();
            _gamePaused = false;
        }

        ToggleCursor();
    }

    private void ToggleCursor()
    {
        Cursor.lockState =
            (Cursor.lockState == CursorLockMode.Locked) ? 
                CursorLockMode.None : 
                CursorLockMode.Locked;

        Cursor.visible = !Cursor.visible;
    }
}
