using UnityEngine;

public class VehicleAvailabilityChecker : MonoBehaviour
{
    public VehiclePrefabFactory prefabFactory;

    public bool IsVehicleAvailable(
        VehiclePrefabManager.VehicleType type)
    {
        if (prefabFactory == null)
        {
            Debug.LogError("Vehicle Prefab Factory missing!");
            return false;
        }

        GameObject prefab = prefabFactory.GetPrefab(type);

        return prefab != null;
    }

    public int GetAvailableVehicleCount()
    {
        if (prefabFactory == null)
            return 0;

        int count = 0;

        if (prefabFactory.carPrefab != null) count++;
        if (prefabFactory.bikePrefab != null) count++;
        if (prefabFactory.busPrefab != null) count++;
        if (prefabFactory.truckPrefab != null) count++;
        if (prefabFactory.suvPrefab != null) count++;
        if (prefabFactory.electricCarPrefab != null) count++;
        if (prefabFactory.atherPrefab != null) count++;
        if (prefabFactory.olaPrefab != null) count++;

        return count;
    }
}
