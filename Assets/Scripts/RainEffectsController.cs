using UnityEngine;

public class RainEffectsController : MonoBehaviour
{
    [Header("Environment")]
    public RealTimeEnvironmentManager environmentManager;

    [Header("Rain Effects")]
    public ParticleSystem rainParticles;
    public CanvasGroup windshieldRainOverlay;
    public AudioSource rainAudio;

    [Header("Settings")]
    [Range(0f, 1f)]
    public float windshieldRainOpacity = 0.8f;

    private bool rainEnabled;

    void Start()
    {
        UpdateRainEffects();
    }

    void Update()
    {
        UpdateRainEffects();
    }

    void UpdateRainEffects()
    {
        if (environmentManager == null)
            return;

        bool isRainy =
            environmentManager.weatherMode ==
            RealTimeEnvironmentManager.WeatherMode.RealTime
            ? environmentManager.currentWeather ==
              RealTimeEnvironmentManager.WeatherType.Rainy
            : environmentManager.manualWeather ==
              RealTimeEnvironmentManager.WeatherType.Rainy;

        if (rainEnabled == isRainy)
            return;

        rainEnabled = isRainy;

        if (rainParticles != null)
        {
            if (rainEnabled)
                rainParticles.Play();
            else
                rainParticles.Stop();
        }

        if (windshieldRainOverlay != null)
        {
            windshieldRainOverlay.alpha =
                rainEnabled ? windshieldRainOpacity : 0f;
        }

        if (rainAudio != null)
        {
            if (rainEnabled)
                rainAudio.Play();
            else
                rainAudio.Stop();
        }
    }
}
