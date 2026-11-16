using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneTransitionController : MonoBehaviour
{
    [Header("Transition UI")]
    public Image fadeImage;

    [Header("Settings")]
    public float fadeDuration = 0.5f;

    private bool isTransitioning;

    public void FadeOut()
    {
        if (!isTransitioning)
        {
            StartCoroutine(FadeRoutine(1f));
        }
    }

    public void FadeIn()
    {
        if (!isTransitioning)
        {
            StartCoroutine(FadeRoutine(0f));
        }
    }

    private IEnumerator FadeRoutine(float targetAlpha)
    {
        if (fadeImage == null)
        {
            Debug.LogWarning("Fade Image is not assigned!");
            yield break;
        }

        isTransitioning = true;

        Color color = fadeImage.color;
        float startAlpha = color.a;
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;

            float progress = fadeDuration > 0f
                ? Mathf.Clamp01(elapsed / fadeDuration)
                : 1f;

            color.a = Mathf.Lerp(startAlpha, targetAlpha, progress);
            fadeImage.color = color;

            yield return null;
        }

        color.a = targetAlpha;
        fadeImage.color = color;

        isTransitioning = false;
    }
}
