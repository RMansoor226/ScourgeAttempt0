using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    private PlayerInputHandler _inputHandler;
    
    [SerializeField]
    private WeaponComponent weapon;
    
    private void Awake()
    {
        _inputHandler = GetComponent<PlayerInputHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_inputHandler.FireHeld)
        {
            //Debug.Log("Fire Held is: "+ _inputHandler.FireHeld);
            weapon.Fire();
        }
        
        if (weapon.CanReload(_inputHandler.ReloadPressed))
        {
            StartCoroutine(weapon.Reload());
        }
    }
}
