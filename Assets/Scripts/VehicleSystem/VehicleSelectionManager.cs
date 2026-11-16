
using UnityEngine;

public class VehicleSelectionManager : MonoBehaviour
{
    [Header("Vehicle Systems")]
    public VehiclePrefabFactory prefabFactory;
    public VehicleRegistration registration;

    [Header("Selection")]
    public VehiclePrefabManager.VehicleType selectedVehicle;

    public GameObject SelectVehicle(
        VehiclePrefabManager.VehicleType type)
    {
        if (prefabFactory == null)
        {
            Debug.LogError("Vehicle Prefab Factory missing!");
            return null;
        }

        GameObject prefab = prefabFactory.GetPrefab(type);

        if (prefab == null)
        {
            Debug.LogWarning("Vehicle unavailable: " + type);
            return null;
        }

        selectedVehicle = type;

        Debug.Log("Selected Vehicle: " + type);

        return prefab;
    }

    public GameObject SpawnSelectedVehicle(
        Vector3 position,
        Quaternion rotation)
    {
        if (prefabFactory == null)
        {
            Debug.LogError("Prefab Factory missing!");
            return null;
        }

        return prefabFactory.CreateVehicle(
            selectedVehicle,
            position,
            rotation
        );
    }

    public string GetSelectedVehicleName()
    {
        return selectedVehicle.ToString();
    }
}

