using UnityEngine;

public class TrafficSpawner : MonoBehaviour
{
    [Header("Traffic Settings")]
    public GameObject[] trafficCarPrefabs;
    public Transform[] spawnPoints;

    [Header("Vehicle Counts")]
    public int lowTrafficCount = 3;
    public int mediumTrafficCount = 7;
    public int highTrafficCount = 12;

    [Header("Spawn Settings")]
    public float spawnInterval = 2f;

    private int spawnedCount;
    private int targetCount;
    private float timer;

    void Start()
    {
        int trafficLevel = PlayerPrefs.GetInt("TrafficLevel", 1);

        switch (trafficLevel)
        {
            case 0:
                targetCount = lowTrafficCount;
                break;

            case 2:
                targetCount = highTrafficCount;
                break;

            default:
                targetCount = mediumTrafficCount;
                break;
        }
    }

    void Update()
    {
        if (spawnedCount >= targetCount)
            return;

        if (trafficCarPrefabs == null ||
            trafficCarPrefabs.Length == 0 ||
            spawnPoints == null ||
            spawnPoints.Length == 0)
            return;

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnTrafficCar();
            timer = 0f;
        }
    }

    void SpawnTrafficCar()
    {
        Transform spawn = spawnPoints[
            Random.Range(0, spawnPoints.Length)
        ];

        GameObject prefab = trafficCarPrefabs[
            Random.Range(0, trafficCarPrefabs.Length)
        ];

        if (spawn == null || prefab == null)
            return;

        Instantiate(prefab, spawn.position, spawn.rotation);
        spawnedCount++;
    }
}
