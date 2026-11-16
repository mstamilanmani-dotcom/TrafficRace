using UnityEngine;

public enum VehicleKind
{
    Car,
    Bike
}

[RequireComponent(typeof(Rigidbody))]
public class DrivableVehicle : MonoBehaviour
{
    public VehicleKind vehicleKind;
    public PlayerCarController drivingController;

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        if (drivingController == null)
            drivingController = GetComponent<PlayerCarController>();

        if (drivingController == null)
            drivingController = gameObject.AddComponent<PlayerCarController>();

        rb.mass = vehicleKind == VehicleKind.Car ? 1200f : 180f;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.isKinematic = true;

        drivingController.enabled = false;
    }

    public void SetDriving(bool active)
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();

        if (drivingController != null)
            drivingController.enabled = active;

        rb.isKinematic = !active;

        if (!active)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
