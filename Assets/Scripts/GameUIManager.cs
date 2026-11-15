
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUIManager : MonoBehaviour
{
    public TMP_Text speedText;
    public TMP_Text timerText;
    public TMP_Text distanceText;

    private float raceTime;
    private bool raceStarted;

    void Update()
    {
        if (!raceStarted) return;

        raceTime += Time.deltaTime;

        if (timerText != null)
            timerText.text = "TIME: " + raceTime.ToString("F1");
    }

    public void StartTimer()
    {
        raceTime = 0f;
        raceStarted = true;
    }

    public void StopTimer()
    {
        raceStarted = false;
    }

    public void UpdateSpeed(float speed)
    {
        if (speedText != null)
            speedText.text = "SPEED: " + speed.ToString("F0");
    }

    public void UpdateDistance(float distance)
    {
        if (distanceText != null)
            distanceText.text = "DISTANCE: " + distance.ToString("F0") + " m";
    }
}
