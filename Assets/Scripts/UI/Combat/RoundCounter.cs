using TMPro;
using UnityEngine;

public class RoundCounter : MonoBehaviour
{
    private TMP_Text _roundText;

    private void Awake()
    {
        _roundText = GetComponent<TMP_Text>();
    }

    public void UpdateRoundCounter(int round)
    {
        //Debug.Log("Updating round");
        _roundText.text = "Round " + (round + 1);
    }
}
