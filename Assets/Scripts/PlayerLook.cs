using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    private PlayerInputHandler _inputHandler;

    [SerializeField] private Transform cameraHolder;
    
    [SerializeField] private float lookSensitivity = 50f;
    
    private float _xRotation = 0f;

    private void Awake()
    {
        _inputHandler = GetComponent<PlayerInputHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLook();
    }
    
    private void UpdateLook()
    {
        Vector2 look = _inputHandler.LookInput;
        
        float mouseX = look.x * lookSensitivity * Time.deltaTime;
        float mouseY = look.y * lookSensitivity * Time.deltaTime;
        
        // Vertical camera rotation
        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

        cameraHolder.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        
        // Horizontal player rotation
        transform.Rotate(Vector3.up * mouseX);
    }
}
