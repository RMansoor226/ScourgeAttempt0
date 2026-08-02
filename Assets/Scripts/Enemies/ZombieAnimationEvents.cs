using UnityEngine;

public class ZombieAnimationEvents : MonoBehaviour
{
    private ZombieAI _zombieAI;

    private void Awake()
    {
        _zombieAI = GetComponentInParent<ZombieAI>();
    }

    public void DamagePlayerEvent()
    {
        Debug.Log("DamagePlayerEvent called");
        _zombieAI.Attack();
    }

    public void AttackSoundEvent()
    {
        Debug.Log("AttackSoundEvent called");
        _zombieAI.PlayAttackSound();
    }
}
