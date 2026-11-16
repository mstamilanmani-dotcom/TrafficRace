using UnityEngine;
using System.Collections.Generic;

public class VehicleDataManager : MonoBehaviour
{
    [Header("Vehicle Database")]
    public VehiclePrefabManager prefabManager;

    public GameObject GetVehiclePrefab(
        VehiclePrefabManager.VehicleType type)
    {
        if (prefabManager == null)
        {
            Debug.LogError("Vehicle Prefab Manager missing!");
            return null;
        }

        foreach (VehiclePrefabManager.VehicleData vehicle
                 in prefabManager.vehicles)
        {
            if (vehicle.type == type)
            {
                return vehicle.prefab;
            }
        }

        Debug.LogWarning("Vehicle data not found: " + type);
        return null;
    }

    public bool IsVehicleRegistered(
        VehiclePrefabManager.VehicleType type)
    {
        return GetVehiclePrefab(type) != null;
    }

    public int GetTotalVehicleCount()
    {
        if (prefabManager == null)
            return 0;

        int count = 0;

        foreach (VehiclePrefabManager.VehicleData vehicle
                 in prefabManager.vehicles)
        {
            if (vehicle.prefab != null)
                count++;
        }

        return count;
    }

    public List<VehiclePrefabManager.VehicleType>
        GetRegisteredVehicleTypes()
    {
        List<VehiclePrefabManager.VehicleType> types =
            new List<VehiclePrefabManager.VehicleType>();

        if (prefabManager == null)
            return types;

        foreach (VehiclePrefabManager.VehicleData vehicle
                 in prefabManager.vehicles)
        {
            if (vehicle.prefab != null &&
                !types.Contains(vehicle.type))
            {
                types.Add(vehicle.type);
            }
        }

        return types;
    }
}
