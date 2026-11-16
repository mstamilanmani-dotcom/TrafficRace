using UnityEngine;

public class VehicleDestroyManager : MonoBehaviour
{
    public void DestroyVehicle(GameObject vehicle)
    {
        if (vehicle == null)
        {
            Debug.LogWarning("Vehicle is missing!");
            return;
        }

        Destroy(vehicle);

        Debug.Log("Vehicle destroyed successfully!");
    }

    public void DestroyVehicleAfterDelay(
        GameObject vehicle,
        float delay)
    {
        if (vehicle == null)
        {
            Debug.LogWarning("Vehicle is missing!");
            return;
        }

        Destroy(vehicle, Mathf.Max(0f, delay));

        Debug.Log("Vehicle destruction scheduled!");
    }
}
