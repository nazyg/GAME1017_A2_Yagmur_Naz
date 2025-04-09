using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public Transform[] spawnPoints;
    public float spawnInterval = 3f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnObstacle();
            timer = 0f;
        }
    }

    void SpawnObstacle()
    {
        int randPrefab = Random.Range(0, obstaclePrefabs.Length);
        int randPoint = Random.Range(0, spawnPoints.Length);

        Instantiate(obstaclePrefabs[randPrefab], spawnPoints[randPoint].position, Quaternion.identity);
    }
}
