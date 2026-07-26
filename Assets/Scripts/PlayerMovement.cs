using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController _controller;
    private PlayerInputHandler _inputHandler;

    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float gravityConstant = -9.81f;

    private Vector3 _velocity;
    
    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _inputHandler = GetComponent<PlayerInputHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateWasd();
        UpdateJumpPressed();
        ApplyGravity();
        _controller.Move(_velocity * Time.deltaTime);
        //Debug.Log($"Grounded: {_controller.isGrounded}, Velocity: {_velocity.y}");
    }

    private void UpdateWasd()
    {
        //Debug.Log("Sprint Active: " + _inputHandler.SprintActive);
        float currentSpeed = _inputHandler.SprintActive ? (speed * 1.5f) : speed; // Adjust speed for sprint
        //Debug.Log("Current Speed: " + currentSpeed);
        
        Vector2 input = _inputHandler.MoveInput;
        
        Vector3 movement = transform.right * input.x +
                           transform.forward * input.y;

        _velocity.x = movement.x * currentSpeed;
        _velocity.z = movement.z * currentSpeed;
    }

    private void UpdateJumpPressed()
    {
        //Debug.Log($"Grounded: {_controller.isGrounded}");
        if (_controller.isGrounded && _inputHandler.JumpPressed)
        {
            _velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravityConstant);
        }
    }

    private void ApplyGravity()
    {
        if (_controller.isGrounded && _velocity.y < 0f)
        {
            _velocity.y = -2f;
        }
        _velocity.y += gravityConstant * Time.deltaTime;
    }
}
