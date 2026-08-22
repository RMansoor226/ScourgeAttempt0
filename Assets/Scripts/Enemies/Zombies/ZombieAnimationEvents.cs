using UnityEngine;

public class ZombieAnimationEvents : MonoBehaviour
{
    private ZombieAI _zombieAI;
    private ZombieAudio _zombieAudio;

    private void Awake()
    {
        _zombieAI = GetComponentInParent<ZombieAI>();
        _zombieAudio = GetComponentInParent<ZombieAudio>();
    }

    public void DamagePlayerEvent()
    {
        //Debug.Log("DamagePlayerEvent called");
        _zombieAI.Attack();
    }

    public void AttackSoundEvent()
    {
        //Debug.Log("AttackSoundEvent called");
        _zombieAI.PlayAttackSound();
=======
        //Debug.Log("AttackSoundEvent called");
        _zombieAudio.PlayAttackSound();
>>>>>>> Stashed changes
    }
}
