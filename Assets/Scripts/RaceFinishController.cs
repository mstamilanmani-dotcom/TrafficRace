
using UnityEngine;
using TMPro;

public class RaceFinishController : MonoBehaviour
{
    public TMP_Text resultText;
    public GameObject resultPanel;

    private bool raceFinished;

    void Start()
    {
        if (resultPanel != null)
            resultPanel.SetActive(false);
    }

    public void FinishRace(string winnerName)
    {
        if (raceFinished) return;

        raceFinished = true;

        if (resultPanel != null)
            resultPanel.SetActive(true);

        if (resultText != null)
            resultText.text = winnerName + " WINS!";
    }

    public void PlayerFinished()
    {
        FinishRace("PLAYER");
    }
}
