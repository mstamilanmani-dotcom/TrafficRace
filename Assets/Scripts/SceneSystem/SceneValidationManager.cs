
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneValidationManager : MonoBehaviour
{
    [Header("Required Scene Objects")]
    public GameObject[] requiredObjects;

    [Header("Scene Names")]
    public string mainMenuScene = "MainMenu";
    public string garageScene = "Garage";
    public string racingScene = "Racing";

    private void Start()
    {
        ValidateCurrentScene();
        CheckSceneManager();
    }

    public void ValidateCurrentScene()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        Debug.Log("Validating Scene: " + currentScene);

        ValidateRequiredObjects();
        ValidateSceneName(currentScene);
    }

    private void ValidateRequiredObjects()
    {
        if (requiredObjects == null || requiredObjects.Length == 0)
        {
            Debug.LogWarning("No required objects assigned!");
            return;
        }

        foreach (GameObject sceneObject in requiredObjects)
        {
            if (sceneObject == null)
            {
                Debug.LogError("A required scene object is missing!");
            }
        }
    }

    private void ValidateSceneName(string sceneName)
    {
        if (sceneName == mainMenuScene ||
            sceneName == garageScene ||
            sceneName == racingScene)
        {
            Debug.Log("Scene name validation successful!");
        }
        else
        {
            Debug.LogWarning(
                "Scene name is not in the configured list: " + sceneName);
        }
    }

    public void CheckSceneManager()
    {
        if (SceneManagerController.Instance != null)
        {
            Debug.Log("Scene Manager is available.");
        }
        else
        {
            Debug.LogError("Scene Manager is missing!");
        }
    }
}

