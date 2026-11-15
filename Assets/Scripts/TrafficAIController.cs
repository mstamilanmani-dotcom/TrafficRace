using UnityEngine;

public class TrafficAIController : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 15f;
    public float minSpeed = 10f;
    public float maxSpeed = 22f;

    [Header("Lane")]
    public float laneOffset = 0f;
    public float laneChangeSpeed = 2f;

    private Vector3 laneTarget;

    void Start()
    {
        speed = Random.Range(minSpeed, maxSpeed);
        laneTarget = transform.position + transform.right * laneOffset;
    }

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;

        laneTarget = new Vector3(
            laneTarget.x,
            transform.position.y,
            laneTarget.z
        );

        transform.position = Vector3.Lerp(
            transform.position,
            laneTarget,
            laneChangeSpeed * Time.deltaTime
        );
    }

    public void SetLaneOffset(float offset)
    {
        laneOffset = offset;
        laneTarget = transform.position + transform.right * laneOffset;
    }

    public void SetSpeed(float newSpeed)
    {
        speed = Mathf.Clamp(newSpeed, minSpeed, maxSpeed);
    }
}
