using UnityEngine;

public class WeaponData
{
    protected float damagePerShot;
    protected float reloadTime;
    protected int magazineCapacity;
    protected int reserveAmmo;

    public float Damage => damagePerShot;
    public float ReloadTime => reloadTime;
    public int MagazineCapacity => magazineCapacity;
    public int ReserveAmmo => reserveAmmo;
}