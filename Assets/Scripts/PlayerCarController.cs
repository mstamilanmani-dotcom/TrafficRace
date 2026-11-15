using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerCarController : MonoBehaviour
{
    [Header("Engine")]
    public float acceleration = 12f;
    public float maxSpeed = 45f;
    public float reverseSpeed = 12f;

    [Header("Brakes")]
    public float brakePower = 25f;

    [Header("Steering")]
    public float steeringSpeed = 65f;
    public float steeringSmoothness = 5f;

    [Header("Handling")]
    public float lateralGrip = 4f;

    private Rigidbody rb;
    private float steering;
    private float currentSteering;
    private bool accelerating;
    private bool braking;
    private bool reversing;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0f, -0.5f, 0f);
    }

    void FixedUpdate()
    {
        Vector3 velocity = rb.linearVelocity;

        float forwardSpeed =
            Vector3.Dot(velocity, transform.forward);

        if (accelerating && forwardSpeed < maxSpeed)
        {
            rb.AddForce(
                transform.forward * acceleration,
                ForceMode.Acceleration
            );
        }

        if (reversing && forwardSpeed > -reverseSpeed)
        {
            rb.AddForce(
                -transform.forward * acceleration * 0.6f,
                ForceMode.Acceleration
            );
        }

        if (braking)
        {
            rb.AddForce(
                -velocity.normalized * brakePower,
                ForceMode.Acceleration
            );
        }

        currentSteering = Mathf.Lerp(
            currentSteering,
            steering,
            steeringSmoothness * Time.fixedDeltaTime
        );

        if (Mathf.Abs(forwardSpeed) > 0.5f)
        {
            float direction = forwardSpeed >= 0f ? 1f : -1f;

            float turn = currentSteering *
                         steeringSpeed *
                         Time.fixedDeltaTime *
                         direction;

            rb.MoveRotation(
                rb.rotation *
                Quaternion.Euler(0f, turn, 0f)
            );
        }

        Vector3 localVelocity =
            transform.InverseTransformDirection(rb.linearVelocity);

        localVelocity.x = Mathf.Lerp(
            localVelocity.x,
            0f,
            lateralGrip * Time.fixedDeltaTime
        );

        rb.linearVelocity =
            transform.TransformDirection(localVelocity);
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

    public void SetReversing(bool value)
    {
        reversing = value;
    }
}
