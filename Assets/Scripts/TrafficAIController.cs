using UnityEngine;

public class TrafficAIController : MonoBehaviour
{
    public float speed = 15f;
    public float laneChangeSpeed = 2f;
    public float laneOffset = 0f;

    void Update()
    {
        Vector3 targetPosition = transform.position +
                                 transform.forward * speed *
                                 Time.deltaTime;

        transform.position = targetPosition;

        Vector3 sidePosition = transform.position +
                               transform.right * laneOffset;

        transform.position = Vector3.Lerp(
            transform.position,
            sidePosition,
            laneChangeSpeed * Time.deltaTime
        );
    }
}
