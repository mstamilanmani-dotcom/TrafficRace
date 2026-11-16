using UnityEngine;
using System.Collections.Generic;

public class SpawnPointManager : MonoBehaviour
{
    [Header("Spawn Points")]
    public Transform[] spawnPoints;

    private readonly List<GameObject> spawnedVehicles =
        new List<GameObject>();

    public Transform GetSpawnPoint(int index)
    {
        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points assigned!");
            return null;
        }

        if (index < 0 || index >= spawnPoints.Length)
        {
            Debug.LogWarning("Invalid spawn point index!");
            return null;
        }

        return spawnPoints[index];
    }

    public GameObject SpawnVehicle(GameObject vehiclePrefab, int index)
    {
        if (vehiclePrefab == null)
        {
            Debug.LogError("Vehicle prefab is missing!");
            return null;
        }

        Transform spawnPoint = GetSpawnPoint(index);

        if (spawnPoint == null)
            return null;

        GameObject vehicle = Instantiate(
            vehiclePrefab,
            spawnPoint.position,
            spawnPoint.rotation
        );

        spawnedVehicles.Add(vehicle);

        return vehicle;
    }

    public void ClearSpawnedVehicles()
    {
        foreach (GameObject vehicle in spawnedVehicles)
        {
            if (vehicle != null)
            {
                Destroy(vehicle);
            }
        }

        spawnedVehicles.Clear();
    }
}
