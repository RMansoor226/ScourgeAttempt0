using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    private PlayerInputHandler _inputHandler;

    [SerializeField] private Transform cameraHolder;
    
    [SerializeField] private float lookSensitivity = 50f;
    
    private float _verticalRotation = 0f;
    private float _horizontalRotation = 0f;

    private Vector2 _currentRecoil;
    private Vector2 _targetRecoil;
    private float _recoilRate;
    private float _centerSpeed;

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
        
        _currentRecoil = Vector2.Lerp(
            _currentRecoil,
            _targetRecoil,
            _recoilRate * Time.deltaTime);

        _targetRecoil = Vector2.Lerp(
            _targetRecoil,
            Vector2.zero,
            _centerSpeed * Time.deltaTime);
        
        // Vertical camera rotation
        _verticalRotation -= mouseY;
        _verticalRotation = Mathf.Clamp(_verticalRotation, -90f, 90f);

        float pitchWithRecoil = _verticalRotation - _currentRecoil.y;

        cameraHolder.localRotation = Quaternion.Euler(pitchWithRecoil, 0f, 0f);
        
        // Horizontal player rotation
        _horizontalRotation += mouseX;
        
        float yawWithRecoil = _horizontalRotation + _currentRecoil.x;
        
        transform.rotation = Quaternion.Euler(0f, yawWithRecoil, 0f);
    }

    public void AddRecoil(float vertical, float horizontal, float recoilRate, float centerSpeed)
    {
        _targetRecoil.y += vertical;
        _targetRecoil.x += Random.Range(-horizontal, horizontal);

        _recoilRate = recoilRate;
        _centerSpeed = centerSpeed;
        
        //Debug.Log("Recoil is active");
    }
}
