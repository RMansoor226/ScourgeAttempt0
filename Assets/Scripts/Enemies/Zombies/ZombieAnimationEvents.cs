using UnityEngine;

public class ZombieAnimationEvents : MonoBehaviour
{
    private Zombie _zombie;

    private void Awake()
    {
        _zombie = GetComponentInParent<Zombie>();
    }

    public void DamagePlayerEvent()
    {
        //Debug.Log("DamagePlayerEvent called");
        _zombie.Attack();
    }

    public void AttackSoundEvent()
    {
        //Debug.Log("AttackSoundEvent called");
        _zombie.PlayAttackSound();
    }
}
