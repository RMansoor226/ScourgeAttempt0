using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Audio;

public class PlayerWeaponController : MonoBehaviour
{
    private PlayerInputHandler _inputHandler;
    
    [SerializeField] 
    private Camera camera;
    
    [SerializeField] 
    private LayerMask hitMask;
    
    private WeaponData _weapon;
    
    private int _currentMagazine;
    private int _reserveAmmo;
    private bool _isReloading;
    
    private void Awake()
    {
        _inputHandler = GetComponent<PlayerInputHandler>();

        _weapon = new Pistol();
        
        _currentMagazine = _weapon.MagazineCapacity;
        _reserveAmmo = _weapon.ReserveAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        if (_inputHandler.FireHeld)
        {
            //Debug.Log("Fire Held is: "+ _inputHandler.FireHeld);
            Fire();
        }

        if (!_isReloading && 
            (_inputHandler.ReloadPressed || _currentMagazine <= 0))
        {
            StartCoroutine(Reload());
        }
    }

    private void Fire()
    {
        if (CanFire())
        {
            RaycastHit hit;
            Debug.DrawRay(
                camera.transform.position, 
                camera.transform.forward, 
                Color.red,
                5.0f);

            _currentMagazine--;
            //Debug.Log($"Magazine currently has {_currentMagazine} bullets");

            if (Physics.Raycast(
                    camera.transform.position,
                    camera.transform.forward,
                    out hit, 
                    50f,
                    hitMask))
            {
                //Debug.Log("Weapon has been fired!");
                //Debug.Log("Object Hit: " + hit.collider.gameObject.name);
            
                if (hit.collider.TryGetComponent(out IDamageable damageable))
                {
                    damageable.TakeDamage(_weapon.Damage);
                }
            }
        }
        
    }

    IEnumerator Reload()
    {
        if (_isReloading ||
            _reserveAmmo <= 0 ||
            _currentMagazine == _weapon.MagazineCapacity)
        {
            yield break;
        }
        
        _isReloading = true;
        
        //Debug.Log("Reloading");

        yield return new WaitForSeconds(_weapon.ReloadTime);


        int bulletsNeeded = _weapon.MagazineCapacity - _currentMagazine;
        int bulletsReloaded = Mathf.Min(bulletsNeeded, _reserveAmmo);
        
        _reserveAmmo -= bulletsReloaded;
        _currentMagazine += bulletsReloaded;
        
        _isReloading = false;
        
        //Debug.Log($"Reload complete. Reserve ammo is {_reserveAmmo}");
    }

    private bool CanFire()
    {
        if (!_isReloading && _currentMagazine > 0)
        {
            return true;
        }
        return false;
    }
}
