using UnityEngine;

public class VehicleSelectionManager : MonoBehaviour
{
    [Header("Vehicle Systems")]
    public VehiclePrefabFactory prefabFactory;
    public VehicleAvailabilityChecker availabilityChecker; // optional

    [Header("Selection")]
    public VehiclePrefabManager.VehicleType selectedVehicle;

    private bool hasSelection;

    public bool HasSelection
    {
        get { return hasSelection; }
    }

    public bool IsAvailable(VehiclePrefabManager.VehicleType type)
    {
        if (availabilityChecker != null)
            return availabilityChecker.IsVehicleAvailable(type);

        return prefabFactory != null && prefabFactory.GetPrefab(type) != null;
    }

    public GameObject SelectVehicle(
        VehiclePrefabManager.VehicleType type)
    {
        if (prefabFactory == null)
        {
            Debug.LogError("Vehicle Prefab Factory missing!");
            return null;
        }

        if (!IsAvailable(type))
        {
            Debug.LogWarning("Vehicle unavailable: " + type);
            return null;
        }

        selectedVehicle = type;
        hasSelection = true;

        Debug.Log("Selected Vehicle: " + type);

        return prefabFactory.GetPrefab(type);
    }

    public GameObject GetSelectedPrefab()
    {
        if (!hasSelection || prefabFactory == null)
            return null;

        return prefabFactory.GetPrefab(selectedVehicle);
    }

    public void ClearSelection()
    {
        hasSelection = false;
    }

    public string GetSelectedVehicleName()
    {
        return hasSelection ? selectedVehicle.ToString() : "None";
    }
}
