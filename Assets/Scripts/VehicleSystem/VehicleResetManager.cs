using UnityEngine;

public class VehicleResetManager : MonoBehaviour
{
    public void ResetVehicle(
        GameObject vehicle,
        Vector3 position,
        Quaternion rotation)
    {
        if (vehicle == null)
        {
            Debug.LogWarning("Vehicle is missing!");
            return;
        }

        Rigidbody rb = vehicle.GetComponent<Rigidbody>();
        bool wasKinematic = false;

        if (rb != null)
        {
            wasKinematic = rb.isKinematic;

            if (!wasKinematic)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }

            rb.isKinematic = true;
        }

        vehicle.transform.SetPositionAndRotation(
            position,
            rotation
        );

        if (rb != null)
        {
            rb.isKinematic = wasKinematic;
        }

        Debug.Log("Vehicle reset completed!");
    }
}
