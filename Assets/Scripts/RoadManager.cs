using UnityEngine;

public class RoadManager : MonoBehaviour
{
    [Header("Player")]
    public Transform playerCar;

    [Header("Road Settings")]
    public float roadLength = 100f;
    public Transform[] roadSections;

    [Header("Road Direction")]
    public Vector3 roadDirection = Vector3.forward;

    void Update()
    {
        if (playerCar == null)
            return;

        if (roadSections == null || roadSections.Length == 0)
            return;

        if (roadLength <= 0f)
            return;

        Vector3 direction = roadDirection.normalized;

        if (direction == Vector3.zero)
            return;

        float totalRoadLength =
            roadLength * roadSections.Length;

        foreach (Transform road in roadSections)
        {
            if (road == null)
                continue;

            float distance = Vector3.Dot(
                playerCar.position - road.position,
                direction
            );

            if (distance > roadLength)
            {
                road.position +=
                    direction * totalRoadLength;
            }
        }
    }
}
