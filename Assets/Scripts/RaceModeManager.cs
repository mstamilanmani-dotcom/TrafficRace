using UnityEngine;

public class RaceModeManager : MonoBehaviour
{
    [Range(1, 10)]
    public int opponentCount = 1;

    public GameObject opponentCarPrefab;
    public Transform[] spawnPoints;

    public void SetOpponentCount(int count)
    {
        opponentCount = Mathf.Clamp(count, 1, 10);
    }

    public void StartRace()
    {
        if (opponentCarPrefab == null || spawnPoints == null)
        {
            Debug.LogWarning("Assign opponent prefab and spawn points.");
            return;
        }

        int count = Mathf.Min(opponentCount, spawnPoints.Length);

        for (int i = 0; i < count; i++)
        {
            if (spawnPoints[i] != null)
            {
                Instantiate(
                    opponentCarPrefab,
                    spawnPoints[i].position,
                    spawnPoints[i].rotation
                );
            }
        }
    }
}
