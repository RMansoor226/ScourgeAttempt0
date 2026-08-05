using UnityEngine;

[CreateAssetMenu(
    fileName = "NewWeapon",
    menuName = "Weapons/Weapon Data")]
public class WeaponData : ScriptableObject
{
    // ---------- General ----------
    [Header("General")]
    [SerializeField] private string weaponName;
    [SerializeField] private GameObject weaponPrefab;
    
    // ---------- DPS ----------
    [Header("DPS")]
    [SerializeField] private float damagePerShot = 0f;
    [SerializeField] private float fireRate = 0f;
    [SerializeField] private float range = 0f;
    
    // ---------- Recoil ----------
    [Header("Recoil")]
    [SerializeField] private float verticalRecoil = 0f;
    [SerializeField] private float horizontalRecoil = 0f;
    [SerializeField] private float recoilRate = 0f;
    [SerializeField] private float centerSpeed = 0;
    
    // ---------- Ammo ----------
    [Header("Ammo")]
    [SerializeField] private float reloadTime = 0f;
    [SerializeField] private int magazineCapacity = 0;
    [SerializeField] private int reserveAmmo = 0;
    
    // ---------- Audio ----------
    [Header("Audio")]
    [SerializeField] private AudioClip gunshotClip;
    [SerializeField] private AudioClip dryFireClip;
    [SerializeField] private AudioClip reloadClip;
    
    // ---------- Properties ----------
    public string WeaponName => weaponName;
    public GameObject WeaponPrefab => weaponPrefab;
    
    public float DamagePerShot => damagePerShot;
    public float FireRate => fireRate;
    public float Range => range;
    
    public float VerticalRecoil => verticalRecoil;
    public float HorizontalRecoil => horizontalRecoil;
    public float RecoilRate => recoilRate;
    public float CenterSpeed => centerSpeed;
    
    public float ReloadTime => reloadTime;
    public int MagazineCapacity => magazineCapacity;
    public int ReserveAmmo => reserveAmmo;
    
    public AudioClip GunshotClip => gunshotClip;
    public AudioClip DryFireClip => dryFireClip;
    public AudioClip ReloadClip => reloadClip;
}