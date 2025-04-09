using System.Collections.Generic;
using UnityEngine;

public class SceneScroller : MonoBehaviour
{
    public GameObject[] scenePrefabs;
    public float scrollSpeed = 2f;
    public int maxParts = 3;

    private float sceneWidth = 16f;
    private Queue<GameObject> activeParts = new Queue<GameObject>();

    void Start()
    {
        // Başlangıçta maxParts kadar sahne parçası oluştur
        for (int i = 0; i < maxParts; i++)
        {
            Vector3 spawnPos = new Vector3(i * sceneWidth, 0f, 0f);
            SpawnNextPart(spawnPos);
        }
    }

    void Update()
    {
        foreach (GameObject part in activeParts)
        {
            part.transform.position += Vector3.left * scrollSpeed * Time.deltaTime;
        }

        GameObject firstPart = activeParts.Peek();

        if (firstPart.transform.position.x < -sceneWidth)
        {
            Destroy(activeParts.Dequeue());

            // EN SON parça artık güvenli şekilde bulunuyor
            GameObject[] partsArray = activeParts.ToArray();
            GameObject lastPart = partsArray[partsArray.Length - 1];

            Vector3 newSpawnPos = lastPart.transform.position + Vector3.right * sceneWidth;
            SpawnNextPart(newSpawnPos);
        }
    }


    void SpawnNextPart(Vector3 position)
    {
        int index = UnityEngine.Random.Range(0, scenePrefabs.Length);
        GameObject newPart = Instantiate(scenePrefabs[index], position, Quaternion.identity);
        activeParts.Enqueue(newPart);
    }
}
