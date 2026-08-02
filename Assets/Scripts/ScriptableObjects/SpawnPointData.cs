using UnityEngine;

[CreateAssetMenu(fileName = "SpawnSettings", menuName = "Scriptable Objects/RoundSettings")]
public class SpawnSettings : ScriptableObject
{
    public float spawnDelay = 3f;
    public int maxZombiesAlive = 3;
    public int maxZombiesPerRound = 6;
}
