using UnityEngine;

public class SemiAutoRifle : WeaponData
{
    public SemiAutoRifle()
    {
        damagePerShot = 100f;
        reloadTime = 3f;
        magazineCapacity = 8;
        reserveAmmo = 80;
    }
}