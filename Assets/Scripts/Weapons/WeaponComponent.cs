using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class WeaponComponent : MonoBehaviour
{
    [SerializeField]
    private Transform muzzle;
    [SerializeField] 
    private Transform shellEject;
    
    [SerializeField]
    private AudioManager audioManager;
    // private Animator animator;

    [SerializeField]
    private WeaponData weaponData;
    
    [SerializeField] 
    private Camera camera;
    
    [SerializeField] 
    private LayerMask hitMask;
    
    [SerializeField]
    private PlayerLook playerView;
    
    [SerializeField] 
    private AmmoCounter ammoCounter;
    [SerializeField] 
    private ParticleSystem muzzleFlash;
    [SerializeField] 
    private GameObject hitEffectPrefab;

    private bool _isReloading = false;
    private int _currentMagazine;
    private int _reserveAmmo;

    private void Awake()
    {
        // Verify audio source exists
        if (audioManager == null)
        {
            Debug.Log("Audio Manager is not instantiated!");
        }
        
        _currentMagazine = weaponData.MagazineCapacity;
        _reserveAmmo = weaponData.ReserveAmmo;
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

            //muzzleFlash.Play();
            
            
            if (muzzleFlash == null)
            {
                Debug.LogError("Muzzle Flash Particle System is NOT assigned!");
            }
            else
            {
                muzzleFlash.Play();
                //Debug.Log("Muzzle Flash triggered. Is Playing: " + muzzleFlash.isPlaying);
            }
            
            audioManager.PlaySfx(weaponData.GunshotClip);
            //PlaySoundClip(weaponData.GunshotClip);
            
            playerView.AddRecoil(
                weaponData.VerticalRecoil, 
                weaponData.HorizontalRecoil,
                weaponData.RecoilRate,
                weaponData.CenterSpeed);
            
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

                DisplayHitEffect(hit);
            
                if (hit.collider.TryGetComponent(out IDamageable damageable))
                {
                    damageable.TakeDamage(weaponData.DamagePerShot);
                }
            }
        } else if (OutOfAmmo())
        {
            audioManager.PlaySfx(weaponData.DryFireClip);
            // PlaySoundClip(weaponData.DryFireClip);
        }
    }

    public IEnumerator Reload()
    {
        if (_isReloading ||
            _reserveAmmo <= 0 ||
            _currentMagazine == weaponData.MagazineCapacity)
        {
            yield break;
        }
        
        _isReloading = true;
        
        //Debug.Log("Reloading");
        
        audioManager.PlaySfx(weaponData.ReloadClip);
        // PlaySoundClip(weaponData.ReloadClip);

        yield return new WaitForSeconds(weaponData.ReloadTime);
        
        int bulletsNeeded = weaponData.MagazineCapacity - _currentMagazine;
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

    private bool OutOfAmmo()
    {
        return _currentMagazine <= 0 && _reserveAmmo <= 0;
    }

    private void DisplayHitEffect(RaycastHit hit)
    {
        if (hitEffectPrefab == null)
        {
            Debug.Log("Hit Effect Prefab is not instantiated");
        }
                
        Vector3 hitEffectSpawnPoint = hit.point + hit.normal * 0.05f;
                
        //Debug.Log("Hit effect spawns here: " + hitEffectSpawnPoint);
        
        GameObject hitEffect = Instantiate(
            hitEffectPrefab,
            hitEffectSpawnPoint,
            Quaternion.LookRotation(hit.normal * -1f, Vector3.up)
        );

        ParticleSystem hitParticles = hitEffect.GetComponentInChildren<ParticleSystem>();

        if (hitParticles != null)
        {
            hitParticles.Play();
        }
                
        Destroy(hitEffect, 5f);
    }
}