using UnityEngine;

public class WeaponData
{
    protected float damagePerShot;

    protected float verticalRecoil;
    protected float horizontalRecoil;
    protected float recoilRate;
    protected float centerSpeed;
    
    protected float reloadTime;
    protected int magazineCapacity;
    protected int reserveAmmo;

    public float Damage => damagePerShot;
    public float VerticalRecoil => verticalRecoil;
    public float HorizontalRecoil => horizontalRecoil;
    public float RecoilRate => recoilRate;
    public float CenterSpeed => centerSpeed;
    public float ReloadTime => reloadTime;
    public int MagazineCapacity => magazineCapacity;
    public int ReserveAmmo => reserveAmmo;
}