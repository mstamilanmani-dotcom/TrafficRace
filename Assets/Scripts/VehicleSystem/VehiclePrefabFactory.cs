
using UnityEngine;

public class VehiclePrefabFactory : MonoBehaviour
{
    [Header("Vehicle Prefabs")]
    public GameObject carPrefab;
    public GameObject bikePrefab;
    public GameObject busPrefab;
    public GameObject truckPrefab;
    public GameObject suvPrefab;
    public GameObject electricCarPrefab;
    public GameObject atherPrefab;
    public GameObject olaPrefab;

    public GameObject GetPrefab(
        VehiclePrefabManager.VehicleType type)
    {
        switch (type)
        {
            case VehiclePrefabManager.VehicleType.Car:
                return carPrefab;

            case VehiclePrefabManager.VehicleType.Bike:
                return bikePrefab;

            case VehiclePrefabManager.VehicleType.Bus:
                return busPrefab;

            case VehiclePrefabManager.VehicleType.Truck:
                return truckPrefab;

            case VehiclePrefabManager.VehicleType.SUV:
                return suvPrefab;

            case VehiclePrefabManager.VehicleType.ElectricCar:
                return electricCarPrefab;

            case VehiclePrefabManager.VehicleType.Ather:
                return atherPrefab;

            case VehiclePrefabManager.VehicleType.OlaElectric:
                return olaPrefab;

            default:
                Debug.LogWarning("Vehicle type not supported: " + type);
                return null;
        }
    }

    public GameObject CreateVehicle(
        VehiclePrefabManager.VehicleType type,
        Vector3 position,
        Quaternion rotation)
    {
        GameObject prefab = GetPrefab(type);

        if (prefab == null)
        {
            Debug.LogWarning("Prefab missing: " + type);
            return null;
        }

        return Instantiate(prefab, position, rotation);
    }
}

