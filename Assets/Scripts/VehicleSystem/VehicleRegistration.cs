
using UnityEngine;
using System.Collections.Generic;

public class VehicleRegistration : MonoBehaviour
{
    [System.Serializable]
    public class VehicleEntry
    {
        public string vehicleName;
        public VehiclePrefabManager.VehicleType vehicleType;
        public GameObject vehiclePrefab;
    }

    [Header("Registered Vehicles")]
    public List<VehicleEntry> registeredVehicles =
        new List<VehicleEntry>();

    public bool RegisterVehicle(
        VehiclePrefabManager.VehicleType type,
        GameObject prefab,
        string vehicleName)
    {
        if (prefab == null)
        {
            Debug.LogWarning("Cannot register empty prefab.");
            return false;
        }

        foreach (VehicleEntry entry in registeredVehicles)
        {
            if (entry.vehicleType == type)
            {
                Debug.LogWarning("Vehicle already registered: " + type);
                return false;
            }
        }

        VehicleEntry newEntry = new VehicleEntry();

        newEntry.vehicleName = vehicleName;
        newEntry.vehicleType = type;
        newEntry.vehiclePrefab = prefab;

        registeredVehicles.Add(newEntry);

        Debug.Log("Vehicle registered: " + vehicleName);

        return true;
    }

    public GameObject GetRegisteredVehicle(
        VehiclePrefabManager.VehicleType type)
    {
        foreach (VehicleEntry entry in registeredVehicles)
        {
            if (entry.vehicleType == type)
            {
                return entry.vehiclePrefab;
            }
        }

        return null;
    }

    public int GetVehicleCount()
    {
        return registeredVehicles.Count;
    }
}

