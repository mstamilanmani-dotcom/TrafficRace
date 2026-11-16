using UnityEngine;

public class VehicleSpawnManager : MonoBehaviour
{
    [Header("Vehicle System")]
    public VehiclePrefabFactory prefabFactory;

    [Header("Spawn Settings")]
    public Transform spawnPoint;

    public GameObject SpawnVehicle(
        VehiclePrefabManager.VehicleType type)
    {
        if (prefabFactory == null)
        {
            Debug.LogError("Vehicle Prefab Factory missing!");
            return null;
        }

        if (spawnPoint == null)
        {
            Debug.LogError("Vehicle Spawn Point missing!");
            return null;
        }

        return prefabFactory.CreateVehicle(
            type,
            spawnPoint.position,
            spawnPoint.rotation
        );
    }

    public GameObject SpawnVehicleAt(
        VehiclePrefabManager.VehicleType type,
        Vector3 position,
        Quaternion rotation)
    {
        if (prefabFactory == null)
        {
            Debug.LogError("Vehicle Prefab Factory missing!");
            return null;
        }

        return prefabFactory.CreateVehicle(
            type,
            position,
            rotation
        );
    }
}
