using UnityEngine;

public class PlayerDamageFlinch : MonoBehaviour
{
    private PlayerLook _playerLook;

    [SerializeField]
    private float verticalFlinch = 40f;
    [SerializeField]
    private float horizontalFlinch = 10f;
    [SerializeField]
    private float flinchRate = 5f;
    [SerializeField]
    private float flinchRecovery = 2f;

    private void Awake()
    {
        _playerLook = GetComponent<PlayerLook>();
    }

    public void FlinchPlayer()
    {
        //Debug.Log("FlinchPlayer() called; Calling AddFlinch()");
        _playerLook.AddFlinch(verticalFlinch, horizontalFlinch, flinchRate, flinchRecovery);
    }
}
