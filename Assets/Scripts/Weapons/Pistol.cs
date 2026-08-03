using UnityEngine;

public class Pistol : WeaponData
{
    public Pistol()
    {
        damagePerShot = 25f;
        reloadTime = 1.5f;
        magazineCapacity = 8;
        reserveAmmo = 40;
    }
}
