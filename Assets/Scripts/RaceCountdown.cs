
using UnityEngine;
using TMPro;
using System.Collections;

public class RaceCountdown : MonoBehaviour
{
    public TMP_Text countdownText;

    public void StartCountdown()
    {
        StartCoroutine(CountdownRoutine());
    }

    IEnumerator CountdownRoutine()
    {
        if (countdownText == null)
            yield break;

        countdownText.gameObject.SetActive(true);

        for (int i = 3; i >= 1; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }

        countdownText.text = "GO!";
        yield return new WaitForSeconds(1f);

        countdownText.gameObject.SetActive(false);
    }
}
