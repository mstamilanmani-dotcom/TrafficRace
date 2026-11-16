using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneManagerController : MonoBehaviour
{
    public static SceneManagerController Instance;

    [Header("Scene Names")]
    public string mainMenuScene = "MainMenu";
    public string garageScene = "Garage";
    public string racingScene = "Racing";

    private bool isLoading;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void LoadMainMenu()
    {
        LoadScene(mainMenuScene);
    }

    public void LoadGarage()
    {
        LoadScene(garageScene);
    }

    public void LoadRacing()
    {
        LoadScene(racingScene);
    }

    public void LoadScene(string sceneName)
    {
        if (isLoading)
            return;

        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("Scene name is empty!");
            return;
        }

        StartCoroutine(LoadSceneRoutine(sceneName));
    }

    private IEnumerator LoadSceneRoutine(string sceneName)
    {
        isLoading = true;

        AsyncOperation operation =
            SceneManager.LoadSceneAsync(sceneName);

        if (operation == null)
        {
            Debug.LogError("Scene could not be loaded: " + sceneName);
            isLoading = false;
            yield break;
        }

        while (!operation.isDone)
        {
            yield return null;
        }

        isLoading = false;
        Debug.Log("Scene loaded: " + sceneName);
    }
}
