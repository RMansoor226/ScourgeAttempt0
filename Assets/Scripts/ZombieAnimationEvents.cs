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
        Debug.Log("DamagePLayerEvent called");
        _zombieAI.Attack();
    }
}
