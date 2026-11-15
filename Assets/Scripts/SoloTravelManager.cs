using UnityEngine;

public class SoloTravelManager : MonoBehaviour
{
    public enum TravelType
    {
        FreeDrive,
        HighwayDrive,
        CityDrive
    }

    public enum TrafficLevel
    {
        Low,
        Medium,
        High
    }

    public enum WeatherType
    {
        Sunny,
        Rainy,
        Foggy
    }

    public enum TimeOfDay
    {
        Day,
        Night
    }

    [Header("Solo Travel Settings")]
    public TravelType travelType = TravelType.FreeDrive;
    public TrafficLevel traffic = TrafficLevel.Medium;
    public WeatherType weather = WeatherType.Sunny;
    public TimeOfDay timeOfDay = TimeOfDay.Day;

    public bool raceTimerEnabled = false;

    public void SetTravelType(int value)
    {
        if (System.Enum.IsDefined(typeof(TravelType), value))
            travelType = (TravelType)value;
    }

    public void SetTraffic(int value)
    {
        if (System.Enum.IsDefined(typeof(TrafficLevel), value))
            traffic = (TrafficLevel)value;
    }

    public void SetWeather(int value)
    {
        if (System.Enum.IsDefined(typeof(WeatherType), value))
            weather = (WeatherType)value;
    }

    public void SetTimeOfDay(int value)
    {
        if (System.Enum.IsDefined(typeof(TimeOfDay), value))
            timeOfDay = (TimeOfDay)value;
    }
}
