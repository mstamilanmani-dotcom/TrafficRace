using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadingSceneController : MonoBehaviour
{
    [Header("UI References")]
    public Slider progressBar;
    public Text progressText;

    [Header("Loading Settings")]
    public string targetSceneName = "MainMenu";

    private void Start()
    {
        StartCoroutine(LoadTargetScene());
    }

    private IEnumerator LoadTargetScene()
    {
        if (string.IsNullOrEmpty(targetSceneName))
        {
            Debug.LogError("Target scene name is empty!");
            yield break;
        }

        AsyncOperation operation =
            SceneManager.LoadSceneAsync(targetSceneName);

        if (operation == null)
        {
            Debug.LogError("Unable to load scene: " + targetSceneName);
            yield break;
        }

        operation.allowSceneActivation = false;

        while (operation.progress < 0.9f)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            UpdateLoadingUI(progress);
            yield return null;
        }

        UpdateLoadingUI(1f);
        yield return new WaitForSeconds(0.3f);

        operation.allowSceneActivation = true;
    }

    private void UpdateLoadingUI(float progress)
    {
        if (progressBar != null)
        {
            progressBar.value = progress;
        }

        if (progressText != null)
        {
            progressText.text = Mathf.RoundToInt(progress * 100f) + "%";
        }
    }
}
