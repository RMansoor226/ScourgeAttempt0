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
    
    private Vector2 _currentFlinch;
    private Vector2 _targetFlinch;
    private float _flinchRate;
    private float _recoverySpeed;

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
        
        // Gradually accumulate recoil
        _currentRecoil = Vector2.Lerp(
            _currentRecoil,
            _targetRecoil,
            _recoilRate * Time.deltaTime);

        _targetRecoil = Vector2.Lerp(
            _targetRecoil,
            Vector2.zero,
            _centerSpeed * Time.deltaTime);
        
        // Gradually accumulate flinch
        _currentFlinch = Vector2.Lerp(
            _currentFlinch,
            _targetFlinch,
            _flinchRate * Time.deltaTime);

        _targetFlinch = Vector2.Lerp(
            _targetFlinch,
            Vector2.zero,
            _recoverySpeed * Time.deltaTime);
        
        // Vertical camera rotation
        _verticalRotation -= mouseY;
        _verticalRotation = Mathf.Clamp(_verticalRotation, -90f, 90f);

        float netPitch = _verticalRotation - (_currentRecoil.y + _currentFlinch.y);

        cameraHolder.localRotation = Quaternion.Euler(netPitch, 0f, 0f);
        
        // Horizontal player rotation
        _horizontalRotation += mouseX;
        
        float netYaw = _horizontalRotation + (_currentRecoil.x + _currentFlinch.x);
        
        transform.rotation = Quaternion.Euler(0f, netYaw, 0f);
    }

    public void AddRecoil(float vertical, float horizontal, float recoilRate, float centerSpeed)
    {
        _targetRecoil.y += vertical;
        _targetRecoil.x += Random.Range(-horizontal, horizontal);

        _recoilRate = recoilRate;
        _centerSpeed = centerSpeed;
        
        //Debug.Log("Recoil is active");
    }
    
    public void AddFlinch(float vertical, float horizontal, float flinchRate, float recoverySpeed)
    {
        _targetFlinch.y += vertical;
        _targetFlinch.x += Random.Range(-horizontal, horizontal);

        _flinchRate = flinchRate;
        _recoverySpeed = recoverySpeed;
        
        //Debug.Log("Flinch is active");
    }
}
