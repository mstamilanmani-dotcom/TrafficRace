using UnityEngine;

public class GarageSceneController : MonoBehaviour
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

    public void StartRace()
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

    public void BackToMainMenu()
    {
        if (sceneManager != null)
        {
            sceneManager.LoadMainMenu();
        }
        else
        {
            Debug.LogError("Scene Manager not found!");
        }
    }
}
