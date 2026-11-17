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

        return prefabFactory.GetPrefab(type) != null;
    }

    public int GetAvailableVehicleCount()
    {
        if (prefabFactory == null)
            return 0;

        int count = 0;

        foreach (VehiclePrefabManager.VehicleType type in
                 System.Enum.GetValues(
                     typeof(VehiclePrefabManager.VehicleType)))
        {
            if (prefabFactory.GetPrefab(type) != null)
                count++;
        }

        return count;
    }
}
