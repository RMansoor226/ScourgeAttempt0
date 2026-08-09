using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Player player;
    
    void Start()
    {
        player.Initialize();
    }
}
