using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class WeaponComponent : MonoBehaviour
{
    [SerializeField]
    private Transform muzzle;

    [SerializeField] 
    private Transform shellEject;
    
    private AudioSource _audioSource;
    // private Animator animator;

    [SerializeField]
    private WeaponData _weaponData;
    
    [SerializeField] 
    private Camera camera;
    
    [SerializeField] 
    private LayerMask hitMask;
    
    [SerializeField]
    private PlayerLook playerView;
    
    [SerializeField] 
    private AmmoCounter ammoCounter;

    private bool _isReloading = false;
    private int _currentMagazine;
    private int _reserveAmmo;

    private void Awake()
    {
        // Verify audio source exists
        if (_audioSource == null)
        {
            _audioSource = GetComponent<AudioSource>();
        }
        
        // Configure audio settings

        _audioSource.playOnAwake = false;
        _audioSource.spatialBlend = 1f;
        _audioSource.volume = 1f;
        _audioSource.pitch = 1f;
        _audioSource.loop = false;
        
        _currentMagazine = _weaponData.MagazineCapacity;
        _reserveAmmo = _weaponData.ReserveAmmo;
    }

    private void Start()
    {
        if (ammoCounter != null)
        {
            ammoCounter.UpdateAmmoCounter(_currentMagazine, _reserveAmmo);
        }
    }

    public void Fire()
    {
        if (CanFire())
        {
            // Reduce magazine count
            _currentMagazine--;
            ammoCounter.UpdateAmmoCounter(_currentMagazine, _reserveAmmo);
            
            //Debug.Log($"Magazine currently has {_currentMagazine} bullets");

            PlaySoundClip(_weaponData.GunshotClip);
            playerView.AddRecoil(
                _weaponData.VerticalRecoil, 
                _weaponData.HorizontalRecoil,
                _weaponData.RecoilRate,
                _weaponData.CenterSpeed);
            
            // Raycast bullet
            RaycastHit hit;
            // Debug.DrawRay(
            //     camera.transform.position, 
            //     camera.transform.forward, 
            //     Color.red,
            //     50.0f);

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
                    damageable.TakeDamage(_weaponData.DamagePerShot);
                }
            }
        } else if (OutOfAmmo())
        {
            PlaySoundClip(_weaponData.DryFireClip);
        }
    }

    public IEnumerator Reload()
    {
        if (_isReloading ||
            _reserveAmmo <= 0 ||
            _currentMagazine == _weaponData.MagazineCapacity)
        {
            yield break;
        }
        
        _isReloading = true;
        
        //Debug.Log("Reloading");

        yield return new WaitForSeconds(_weaponData.ReloadTime);
        
        int bulletsNeeded = _weaponData.MagazineCapacity - _currentMagazine;
        int bulletsReloaded = Mathf.Min(bulletsNeeded, _reserveAmmo);
        
        _reserveAmmo -= bulletsReloaded;
        _currentMagazine += bulletsReloaded;
        
        _isReloading = false;
        
        ammoCounter.UpdateAmmoCounter(_currentMagazine, _reserveAmmo);
        
        //Debug.Log($"Reload complete. Reserve ammo is {_reserveAmmo}");
    }

    private bool CanFire()
    {
        return !_isReloading && 
               _currentMagazine > 0;
    }

    public bool CanReload(bool reloadPressed)
    {
        return !_isReloading &&
               (reloadPressed || _currentMagazine <= 0);
    }

    private void PlaySoundClip(AudioClip _clip)
    {
        if (_clip != null)
        {
            _audioSource.clip = _clip;
            _audioSource.Play();
        }
    }

    private bool OutOfAmmo()
    {
        return _currentMagazine <= 0 && _reserveAmmo <= 0;
    }
}
