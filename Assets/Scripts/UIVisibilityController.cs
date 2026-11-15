using UnityEngine;

public class UIVisibilityController : MonoBehaviour
{
    [Header("UI Visibility")]
    [Range(0f, 100f)]
    public float visibility = 100f;

    [Header("UI Panels")]
    public CanvasGroup[] controlPanels;

    public void SetVisibility(float value)
    {
        visibility = Mathf.Clamp(value, 0f, 100f);

        float alpha = visibility / 100f;

        foreach (CanvasGroup panel in controlPanels)
        {
            if (panel != null)
            {
                panel.alpha = alpha;
                panel.interactable = visibility > 0f;
                panel.blocksRaycasts = visibility > 0f;
            }
        }
    }
}
