using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    [Header("Environment Settings")]
    public Material skyboxMaterial;
    public Light directionalLight;

    [Header("Lighting Settings")]
    [Range(0f, 2f)]
    public float lightIntensity = 1f;

    [Header("Fog Settings")]
    public bool enableFog;
    public Color fogColor = Color.gray;
    [Range(0f, 0.1f)]
    public float fogDensity = 0.01f;

    private void Start()
    {
        ApplyEnvironment();
    }

    public void ApplyEnvironment()
    {
        if (skyboxMaterial != null)
        {
            RenderSettings.skybox = skyboxMaterial;
        }

        RenderSettings.fog = enableFog;
        RenderSettings.fogColor = fogColor;
        RenderSettings.fogDensity = fogDensity;

        if (directionalLight != null)
        {
            directionalLight.intensity = lightIntensity;
        }

        DynamicGI.UpdateEnvironment();

        Debug.Log("TrafficRace Environment Applied");
    }

    public void SetDayEnvironment()
    {
        enableFog = false;
        lightIntensity = 1f;
        ApplyEnvironment();
    }

    public void SetEveningEnvironment()
    {
        enableFog = true;
        fogColor = new Color(0.8f, 0.55f, 0.35f);
        fogDensity = 0.005f;
        lightIntensity = 0.6f;
        ApplyEnvironment();
    }

    public void SetNightEnvironment()
    {
        enableFog = true;
        fogColor = new Color(0.08f, 0.1f, 0.2f);
        fogDensity = 0.015f;
        lightIntensity = 0.2f;
        ApplyEnvironment();
    }
}
