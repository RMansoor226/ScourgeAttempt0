using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInputActions _inputActions;
    
    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }
    
    public bool JumpPressed { get; private set; }
    public bool SprintActive { get; private set; }
    public bool FireHeld { get; private set; }
    public bool ReloadPressed { get; private set; }
    public bool InteractActive { get; private set; }

    private void Awake()
    {
        _inputActions = new PlayerInputActions();
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
        MoveInput = _inputActions.Player.Move.ReadValue<Vector2>();
        LookInput = _inputActions.Player.Look.ReadValue<Vector2>();
        
        JumpPressed = _inputActions.Player.Jump.WasPressedThisFrame();
        SprintActive = _inputActions.Player.Sprint.IsPressed();
        
        FireHeld = _inputActions.Player.Fire.WasPressedThisFrame();
        ReloadPressed = _inputActions.Player.Reload.WasPressedThisFrame();
    }
}
