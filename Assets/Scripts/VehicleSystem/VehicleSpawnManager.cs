using UnityEngine;

public class VehicleSpawnManager : MonoBehaviour
{
    [Header("Vehicle System")]
    public VehicleSelectionManager selectionManager;
    public VehiclePrefabFactory prefabFactory; // optional

    [Header("Spawn Settings")]
    public SpawnPointManager spawnPointManager; // optional
    public int spawnPointIndex = 0;
    public Transform spawnPoint; // fallback

    private VehiclePrefabFactory Factory
    {
        get
        {
            if (prefabFactory != null)
                return prefabFactory;

            if (selectionManager != null)
                return selectionManager.prefabFactory;

            return null;
        }
    }

    public GameObject SpawnSelectedVehicle()
    {
        if (selectionManager == null)
        {
            Debug.LogError("Vehicle Selection Manager missing!");
            return null;
        }

        if (!selectionManager.HasSelection)
        {
            Debug.LogWarning("No vehicle selected yet. Spawn cancelled.");
            return null;
        }

        return SpawnVehicle(selectionManager.selectedVehicle);
    }

    public GameObject SpawnVehicle(
        VehiclePrefabManager.VehicleType type)
    {
        VehiclePrefabFactory factory = Factory;

        if (factory == null)
        {
            Debug.LogError("Vehicle Prefab Factory missing!");
            return null;
        }

        GameObject prefab = factory.GetPrefab(type);

        if (prefab == null)
        {
            Debug.LogWarning("Prefab missing: " + type);
            return null;
        }

        GameObject vehicle = null;

        if (spawnPointManager != null)
        {
            vehicle = spawnPointManager.SpawnVehicle(
                prefab,
                spawnPointIndex
            );
        }
        else if (spawnPoint != null)
        {
            vehicle = factory.CreateVehicle(
                type,
                spawnPoint.position,
                spawnPoint.rotation
            );
        }
        else
        {
            Debug.LogError("No spawn point assigned!");
            return null;
        }

        CheckDrivable(vehicle);
        return vehicle;
    }

    public GameObject SpawnVehicleAt(
        VehiclePrefabManager.VehicleType type,
        Vector3 position,
        Quaternion rotation)
    {
        VehiclePrefabFactory factory = Factory;

        if (factory == null)
        {
            Debug.LogError("Vehicle Prefab Factory missing!");
            return null;
        }

        GameObject vehicle = factory.CreateVehicle(
            type,
            position,
            rotation
        );

        CheckDrivable(vehicle);
        return vehicle;
    }

    private void CheckDrivable(GameObject vehicle)
    {
        if (vehicle != null &&
            vehicle.GetComponent<DrivableVehicle>() == null)
        {
            Debug.LogWarning(
                "Spawned vehicle has no DrivableVehicle: " + vehicle.name
            );
        }
    }
}
