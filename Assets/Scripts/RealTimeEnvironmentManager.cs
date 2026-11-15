using System;
using System.Collections;
using System.Globalization;
using UnityEngine;
using UnityEngine.Networking;

public class RealTimeEnvironmentManager : MonoBehaviour
{
    public enum TimeMode
    {
        RealTime,
        Manual
    }

    public enum WeatherMode
    {
        RealTime,
        Manual
    }

    public enum WeatherType
    {
        Sunny,
        Cloudy,
        Rainy,
        Foggy
    }

    [Header("Time Settings")]
    public TimeMode timeMode = TimeMode.RealTime;
    [Range(0f, 24f)]
    public float manualHour = 12f;

    [Header("Weather Settings")]
    public WeatherMode weatherMode = WeatherMode.RealTime;
    public WeatherType manualWeather = WeatherType.Sunny;

    [Header("Real Weather Location")]
    public float latitude = 12.92f;
    public float longitude = 80.25f;

    [Header("Lighting")]
    public Light sunLight;

    [Header("Weather Information")]
    public float temperatureC;
    public int humidity;
    public WeatherType currentWeather = WeatherType.Sunny;

    private float updateTimer;

    [Serializable]
    private class WeatherResponse
    {
        public CurrentWeather current;
    }

    [Serializable]
    private class CurrentWeather
    {
        public float temperature_2m;
        public int relative_humidity_2m;
        public int weather_code;
    }

    void Start()
    {
        if (sunLight == null)
            sunLight = FindObjectOfType<Light>();

        ApplyEnvironment();

        if (weatherMode == WeatherMode.RealTime)
            RefreshRealWeather();

        InvokeRepeating(
            nameof(RefreshRealWeather),
            900f,
            900f
        );
    }

    void Update()
    {
        updateTimer += Time.deltaTime;

        if (updateTimer >= 60f)
        {
            ApplyEnvironment();
            updateTimer = 0f;
        }
    }

    public void UseRealTimeClock()
    {
        timeMode = TimeMode.RealTime;
        ApplyEnvironment();
    }

    public void UseManualClock()
    {
        timeMode = TimeMode.Manual;
        ApplyEnvironment();
    }

    public void SetManualHour(float hour)
    {
        manualHour = Mathf.Clamp(hour, 0f, 24f);
        timeMode = TimeMode.Manual;
        ApplyEnvironment();
    }

    public void UseRealTimeWeather()
    {
        weatherMode = WeatherMode.RealTime;
        RefreshRealWeather();
    }

    public void UseManualWeather()
    {
        weatherMode = WeatherMode.Manual;
        ApplyEnvironment();
    }

    public void SetManualWeather(int value)
    {
        if (Enum.IsDefined(typeof(WeatherType), value))
        {
            manualWeather = (WeatherType)value;
            weatherMode = WeatherMode.Manual;
            ApplyEnvironment();
        }
    }

    public void RefreshRealWeather()
    {
        if (weatherMode == WeatherMode.RealTime)
            StartCoroutine(GetWeather());
    }

    IEnumerator GetWeather()
    {
        string lat = latitude.ToString(
            CultureInfo.InvariantCulture
        );

        string lon = longitude.ToString(
            CultureInfo.InvariantCulture
        );

        string url =
            "https://api.open-meteo.com/v1/forecast" +
            "?latitude=" + lat +
            "&longitude=" + lon +
            "&current=temperature_2m,relative_humidity_2m,weather_code" +
            "&timezone=Asia%2FKolkata";

        using (UnityWebRequest request =
               UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogWarning(
                    "Weather unavailable: " + request.error
                );
                yield break;
            }

            WeatherResponse data =
                JsonUtility.FromJson<WeatherResponse>(
                    request.downloadHandler.text
                );

            if (data == null || data.current == null)
                yield break;

            temperatureC = data.current.temperature_2m;
            humidity = data.current.relative_humidity_2m;

            currentWeather =
                ConvertWeatherCode(data.current.weather_code);

            ApplyEnvironment();
        }
    }

    WeatherType ConvertWeatherCode(int code)
    {
        if (code == 0)
            return WeatherType.Sunny;

        if (code == 45 || code == 48)
            return WeatherType.Foggy;

        if (code >= 51 && code <= 67)
            return WeatherType.Rainy;

        if (code >= 71 && code <= 77)
            return WeatherType.Cloudy;

        if (code >= 80 && code <= 82)
            return WeatherType.Rainy;

        if (code >= 95 && code <= 99)
            return WeatherType.Rainy;

        return WeatherType.Cloudy;
    }

    void ApplyEnvironment()
    {
        float hour = timeMode == TimeMode.RealTime
            ? DateTime.Now.Hour +
              DateTime.Now.Minute / 60f
            : manualHour;

        WeatherType weather =
            weatherMode == WeatherMode.RealTime
            ? currentWeather
            : manualWeather;

        float daylight = Mathf.Clamp01(
            Mathf.Sin((hour - 6f) * Mathf.PI / 12f)
        );

        float weatherBrightness = 1f;

        if (weather == WeatherType.Cloudy)
            weatherBrightness = 0.8f;

        if (weather == WeatherType.Rainy)
            weatherBrightness = 0.65f;

        if (weather == WeatherType.Foggy)
            weatherBrightness = 0.7f;

        if (sunLight != null)
        {
            sunLight.transform.rotation =
                Quaternion.Euler(
                    hour * 15f - 90f,
                    170f,
                    0f
                );

            sunLight.intensity =
                Mathf.Lerp(0.05f, 1.2f, daylight) *
                weatherBrightness;
        }

        RenderSettings.ambientLight = Color.Lerp(
            new Color(0.08f, 0.10f, 0.18f),
            new Color(0.65f, 0.68f, 0.72f),
            daylight * weatherBrightness
        );

        RenderSettings.fog =
            weather == WeatherType.Foggy ||
            weather == WeatherType.Rainy;

        RenderSettings.fogDensity =
            weather == WeatherType.Foggy ? 0.018f :
            weather == WeatherType.Rainy ? 0.003f :
            0.0005f;
    }
}
