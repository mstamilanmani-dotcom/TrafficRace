using UnityEngine;

public class RoadManager : MonoBehaviour
{
    public Transform playerCar;
    public float roadLength = 100f;
    public Transform[] roadSections;

    void Update()
    {
        if (playerCar == null || roadSections.Length == 0)
            return;

        foreach (Transform road in roadSections)
        {
            if (playerCar.position.z - road.position.z > roadLength)
            {
                road.position += Vector3.forward *
                                 roadLength *
                                 roadSections.Length;
            }
        }
    }
}
