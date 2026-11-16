using UnityEngine;

public class MainMenuSceneController : MonoBehaviour
{
    [Header("Scene Manager")]
    public SceneManagerController sceneManager;

    private void Start()
    {
        if (sceneManager == null)
        {
            sceneManager = SceneManagerController.Instance;
        }
    }

    public void OpenGarage()
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

    public void OpenRacing()
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

    public void ExitGame()
    {
        Debug.Log("TrafficRace Exit Requested");

        Application.Quit();
    }
}
