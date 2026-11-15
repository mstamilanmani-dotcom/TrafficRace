using UnityEngine;

public class CarCustomizationSystem : MonoBehaviour
{
    [Header("Car Paint")]
    public Renderer[] carBodyParts;

    [Header("Wheel Rims")]
    public Renderer[] wheelRims;

    [Header("Accessories")]
    public GameObject spoiler;
    public GameObject headlights;

    public void SetCarColor(Color color)
    {
        foreach (Renderer part in carBodyParts)
        {
            if (part != null)
                part.material.color = color;
        }
    }

    public void SetRimMaterial(Material material)
    {
        if (material == null) return;

        foreach (Renderer rim in wheelRims)
        {
            if (rim != null)
                rim.material = material;
        }
    }

    public void SetSpoiler(bool enabled)
    {
        if (spoiler != null)
            spoiler.SetActive(enabled);
    }

    public void SetHeadlights(bool enabled)
    {
        if (headlights != null)
            headlights.SetActive(enabled);
    }
}
