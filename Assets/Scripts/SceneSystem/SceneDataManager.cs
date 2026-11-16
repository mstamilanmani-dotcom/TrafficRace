using UnityEngine;

public class SceneDataManager : MonoBehaviour
{
    public static SceneDataManager Instance;

    [Header("Current Game Data")]
    public string selectedVehicleName;
    public string selectedRaceMode;
    public int selectedPlayerCount = 1;
    public int selectedRoadIndex;
    public int selectedVehicleIndex;

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

    public void SetVehicle(string vehicleName, int vehicleIndex)
    {
        selectedVehicleName = vehicleName;
        selectedVehicleIndex = vehicleIndex;
    }

    public void SetRaceMode(string raceMode, int playerCount)
    {
        selectedRaceMode = raceMode;
        selectedPlayerCount = Mathf.Max(1, playerCount);
    }

    public void SetRoad(int roadIndex)
    {
        selectedRoadIndex = Mathf.Max(0, roadIndex);
    }

    public void ResetRaceData()
    {
        selectedRaceMode = string.Empty;
        selectedPlayerCount = 1;
        selectedRoadIndex = 0;
    }
}
