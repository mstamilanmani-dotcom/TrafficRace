
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerCarController : MonoBehaviour
{
    public float acceleration = 8000f;
    public float maxSpeed = 45f;
    public float brakePower = 12000f;
    public float steeringSpeed = 80f;

    private Rigidbody rb;
    private float steering;
    private bool accelerating;
    private bool braking;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0f, -0.5f, 0f);
    }

    void FixedUpdate()
    {
        float speed = rb.velocity.magnitude;

        if (accelerating && speed < maxSpeed)
            rb.AddForce(transform.forward * acceleration * Time.fixedDeltaTime,
                ForceMode.Force);

        if (braking)
            rb.AddForce(-transform.forward * brakePower * Time.fixedDeltaTime,
                ForceMode.Force);

        if (speed > 1f)
        {
            float turn = steering * steeringSpeed *
                         Time.fixedDeltaTime;

            rb.MoveRotation(rb.rotation *
                Quaternion.Euler(0f, turn, 0f));
        }
    }

    public void SetSteering(float value)
    {
        steering = Mathf.Clamp(value, -1f, 1f);
    }

    public void SetAcceleration(bool value)
    {
        accelerating = value;
    }

    public void SetBraking(bool value)
    {
        braking = value;
    }
}
