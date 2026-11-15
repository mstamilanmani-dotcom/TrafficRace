using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarCollisionController : MonoBehaviour
{
    public float impactThreshold = 5f;
    public float impactCooldown = 1f;

    private float lastImpactTime;

    void OnCollisionEnter(Collision collision)
    {
        if (Time.time - lastImpactTime < impactCooldown)
            return;

        if (collision.relativeVelocity.magnitude >= impactThreshold)
        {
            lastImpactTime = Time.time;

            Debug.Log("Car Collision: " + collision.gameObject.name);
        }
    }
}
