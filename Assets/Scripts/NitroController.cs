
using UnityEngine;

public class NitroController : MonoBehaviour
{
    public Rigidbody carRigidbody;
    public float nitroForce = 5000f;
    public float nitroDuration = 3f;

    private float nitroTimer;
    private bool nitroActive;

    void Update()
    {
        if (nitroActive)
        {
            nitroTimer -= Time.deltaTime;

            if (nitroTimer <= 0f)
                nitroActive = false;
        }
    }

    void FixedUpdate()
    {
        if (nitroActive && carRigidbody != null)
        {
            carRigidbody.AddForce(
                transform.forward * nitroForce,
                ForceMode.Force
            );
        }
    }

    public void ActivateNitro()
    {
        if (nitroActive) return;

        nitroActive = true;
        nitroTimer = nitroDuration;
    }
}
