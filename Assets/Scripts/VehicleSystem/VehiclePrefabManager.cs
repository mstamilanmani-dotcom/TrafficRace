using UnityEngine;
using System.Collections.Generic;

// VehicleType enum is shared by the whole vehicle system.
// Prefabs now live in VehiclePrefabFactory; the list below is legacy.
public class VehiclePrefabManager : MonoBehaviour
{
    public enum VehicleType
    {
        Car,
        Bike,
        Bus,
        Truck,
        SUV,
        ElectricCar,
        Ather,
        OlaElectric
    }

    [System.Serializable]
    public class VehicleData
    {
        public VehicleType type;
        public GameObject prefab;
    }

    [Header("Legacy (use VehiclePrefabFactory)")]
    public List<VehicleData> vehicles = new List<VehicleData>();

    [System.Obsolete("Use VehicleSpawnManager.", false)]
    public GameObject SpawnVehicle(
        VehicleType type,
        Vector3 position,
        Quaternion rotation)
    {
        foreach (VehicleData vehicle in vehicles)
        {
            if (vehicle.type == type && vehicle.prefab != null)
            {
                return Instantiate(
                    vehicle.prefab,
                    position,
                    rotation
                );
            }
        }

        Debug.LogWarning("Vehicle prefab not found: " + type);
        return null;
    }

    public bool HasVehicle(VehicleType type)
    {
        foreach (VehicleData vehicle in vehicles)
        {
            if (vehicle.type == type && vehicle.prefab != null)
            {
                return true;
            }
        }

        return false;
    }
}
