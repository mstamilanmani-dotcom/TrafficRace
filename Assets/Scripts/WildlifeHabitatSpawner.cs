using UnityEngine;

public class WildlifeHabitatSpawner : MonoBehaviour
{
    [Header("Habitat")]
    public string habitatName = "Safari Forest";

    [Header("Animal Prefabs")]
    public GameObject[] animalPrefabs;

    [Header("Spawn Area")]
    public Transform[] spawnPoints;
    public int animalCount = 5;
    public float animalWanderRadius = 8f;

    void Start()
    {
        SpawnHabitatAnimals();
    }

    void SpawnHabitatAnimals()
    {
        if (animalPrefabs == null ||
            animalPrefabs.Length == 0 ||
            spawnPoints == null)
            return;

        int count = Mathf.Min(
            animalCount, spawnPoints.Length
        );

        for (int i = 0; i < count; i++)
        {
            Transform point = spawnPoints[i];

            if (point == null)
                continue;

            GameObject prefab = animalPrefabs[
                Random.Range(0, animalPrefabs.Length)
            ];

            if (prefab == null)
                continue;

            GameObject animal = Instantiate(
                prefab,
                point.position,
                point.rotation,
                transform
            );

            WildAnimalAIController ai =
                animal.GetComponent<WildAnimalAIController>();

            if (ai != null)
                ai.wanderRadius = animalWanderRadius;
        }
    }
}
