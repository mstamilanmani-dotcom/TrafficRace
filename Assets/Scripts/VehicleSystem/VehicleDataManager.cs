using UnityEngine;
using System.Collections.Generic;

public class VehicleDataManager : MonoBehaviour
{
    [Header("Vehicle Database")]
    public VehiclePrefabFactory prefabFactory;

    public GameObject GetVehiclePrefab(
        VehiclePrefabManager.VehicleType type)
    {
        if (prefabFactory == null)
        {
            Debug.LogError("Vehicle Prefab Factory missing!");
            return null;
        }

        return prefabFactory.GetPrefab(type);
    }

    public bool IsVehicleRegistered(
        VehiclePrefabManager.VehicleType type)
    {
        return GetVehiclePrefab(type) != null;
    }

    public int GetTotalVehicleCount()
    {
        return GetRegisteredVehicleTypes().Count;
    }

    public List<VehiclePrefabManager.VehicleType>
        GetRegisteredVehicleTypes()
    {
        List<VehiclePrefabManager.VehicleType> types =
            new List<VehiclePrefabManager.VehicleType>();

        if (prefabFactory == null)
            return types;

        foreach (VehiclePrefabManager.VehicleType type in
                 System.Enum.GetValues(
                     typeof(VehiclePrefabManager.VehicleType)))
        {
            if (prefabFactory.GetPrefab(type) != null)
                types.Add(type);
        }

        return types;
    }
}
