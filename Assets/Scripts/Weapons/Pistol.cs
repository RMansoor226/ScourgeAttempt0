using UnityEngine;

public class Pistol : WeaponData
{
    public Pistol()
    {
        damagePerShot = 25f;

        verticalRecoil = 10f;
        horizontalRecoil = 2f;
        recoilRate = 4f;
        centerSpeed = 2f;
        
        reloadTime = 1.5f;
        magazineCapacity = 8;
        reserveAmmo = 40;
    }
}
