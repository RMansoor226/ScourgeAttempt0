using System.Collections;
using System.Numerics;
using UnityEngine;
using UnityEngine.Experimental.Audio;
using Vector2 = UnityEngine.Vector2;

public class PlayerWeaponController : MonoBehaviour
{
    private PlayerInputHandler _inputHandler;
    
    [SerializeField] 
    private Camera camera;
    
    [SerializeField] 
    private LayerMask hitMask;

    [SerializeField] 
    private AmmoCounter ammoCounter;
    
    [SerializeField]
    private PlayerLook playerView;
    
    private WeaponData _weapon;

    private Vector2 currentRecoil;
    private Vector2 targetRecoil;
    
    private int _currentMagazine;
    private int _reserveAmmo;
    private bool _isReloading;
    
    private void Awake()
    {
        _inputHandler = GetComponent<PlayerInputHandler>();

        _weapon = new Pistol();
        
        _currentMagazine = _weapon.MagazineCapacity;
        _reserveAmmo = _weapon.ReserveAmmo;
        
        ammoCounter.UpdateAmmoCounter(_currentMagazine, _reserveAmmo);
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
            // Reduce magazine count
            _currentMagazine--;
            ammoCounter.UpdateAmmoCounter(_currentMagazine, _reserveAmmo);
            
            //Debug.Log($"Magazine currently has {_currentMagazine} bullets");

            playerView.AddRecoil(
                _weapon.VerticalRecoil, 
                _weapon.HorizontalRecoil,
                _weapon.RecoilRate,
                _weapon.CenterSpeed);
            
            // Raycast bullet
            RaycastHit hit;
            Debug.DrawRay(
                camera.transform.position, 
                camera.transform.forward, 
                Color.red,
                5.0f);

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
        
        ammoCounter.UpdateAmmoCounter(_currentMagazine, _reserveAmmo);
        
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
