using TMPro;
using UnityEngine;

public class AmmoCounter : MonoBehaviour
{
    private TMP_Text _ammoText;

    private void Awake()
    {
        _ammoText = GetComponent<TMP_Text>();
    }

    public void UpdateAmmoCounter(int magazine, int reserve)
    {
        Debug.Log("Updating ammo counter");
        _ammoText.text = $"{magazine} / {reserve}";
    }
}
