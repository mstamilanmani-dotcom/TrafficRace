using UnityEngine;

public class BirdFlockSpawner : MonoBehaviour
{
    [Header("Bird Settings")]
    public GameObject birdPrefab;
    public int birdCount = 8;

    [Header("Spawn Settings")]
    public float spawnRadius = 12f;
    public float spawnHeight = 12f;

    void Start()
    {
        SpawnBirds();
    }

    void SpawnBirds()
    {
        if (birdPrefab == null)
        {
            Debug.LogWarning("Bird Prefab is not assigned!");
            return;
        }

        for (int i = 0; i < birdCount; i++)
        {
            Vector3 spawnPosition = transform.position +
                new Vector3(
                    Random.Range(-spawnRadius, spawnRadius),
                    Random.Range(5f, spawnHeight),
                    Random.Range(-spawnRadius, spawnRadius)
                );

            GameObject bird = Instantiate(
                birdPrefab,
                spawnPosition,
                Quaternion.identity
            );

            BirdFlockController controller =
                bird.GetComponent<BirdFlockController>();

            if (controller != null)
            {
                controller.SetFlockCenter(transform);
            }
        }
    }
}0

