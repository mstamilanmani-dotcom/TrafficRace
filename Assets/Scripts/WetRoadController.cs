using UnityEngine;

public class WetRoadController : MonoBehaviour
{
    [Header("Environment")]
    public RealTimeEnvironmentManager environmentManager;

    [Header("Road Materials")]
    public Renderer[] asphaltRenderers;
    public Material wetRoadMaterial;

    private Material[][] dryMaterials;
    private bool isWet;

    void Start()
    {
        if (asphaltRenderers == null)
            return;

        dryMaterials = new Material[asphaltRenderers.Length][];

        for (int i = 0; i < asphaltRenderers.Length; i++)
        {
            if (asphaltRenderers[i] != null)
                dryMaterials[i] =
                    asphaltRenderers[i].sharedMaterials;
        }

        UpdateRoadSurface();
    }

    void Update()
    {
        UpdateRoadSurface();
    }

    void UpdateRoadSurface()
    {
        if (environmentManager == null ||
            wetRoadMaterial == null ||
            dryMaterials == null)
            return;

        bool rainy =
            environmentManager.weatherMode ==
            RealTimeEnvironmentManager.WeatherMode.RealTime
            ? environmentManager.currentWeather ==
              RealTimeEnvironmentManager.WeatherType.Rainy
            : environmentManager.manualWeather ==
              RealTimeEnvironmentManager.WeatherType.Rainy;

        if (rainy == isWet)
            return;

        isWet = rainy;

        for (int i = 0; i < asphaltRenderers.Length; i++)
        {
            Renderer road = asphaltRenderers[i];

            if (road == null || dryMaterials[i] == null)
                continue;

            if (isWet)
            {
                Material[] wetMaterials =
                    new Material[dryMaterials[i].Length];

                for (int j = 0; j < wetMaterials.Length; j++)
                    wetMaterials[j] = wetRoadMaterial;

                road.sharedMaterials = wetMaterials;
            }
            else
            {
                road.sharedMaterials = dryMaterials[i];
            }
        }
    }
}
