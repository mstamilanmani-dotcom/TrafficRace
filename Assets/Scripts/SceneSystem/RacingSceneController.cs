using UnityEngine;

public class RacingSceneController : MonoBehaviour
{
    [Header("Scene Manager")]
    public SceneManagerController sceneManager;

    private bool raceStarted;

    private void Start()
    {
        if (sceneManager == null)
        {
            sceneManager = SceneManagerController.Instance;
        }

        raceStarted = false;
        Debug.Log("TrafficRace Racing Scene Ready");
    }

    public void StartRace()
    {
        if (raceStarted)
            return;

        raceStarted = true;
        Debug.Log("Race Started!");
    }

    public void RestartRace()
    {
        if (sceneManager != null)
        {
            sceneManager.LoadRacing();
        }
        else
        {
            Debug.LogError("Scene Manager not found!");
        }
    }

    public void BackToGarage()
    {
        if (sceneManager != null)
        {
            sceneManager.LoadGarage();
        }
        else
        {
            Debug.LogError("Scene Manager not found!");
        }
    }
}
