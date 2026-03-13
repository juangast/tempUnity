using UnityEngine;
using System.Collections;

public class QuestionBoxSpawnerLuis : MonoBehaviour
{
    public GameObject questionBoxPrefab;
    public Transform player;
    public float spawnDistanceAhead = 20f;
    public float minSpawnInterval = 8f;
    public float maxSpawnInterval = 15f;
    public float spawnY = -0.5f;

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        float wait = Random.Range(minSpawnInterval, maxSpawnInterval);
        yield return new WaitForSeconds(wait);

        float spawnX = player.position.x + spawnDistanceAhead;
        float y = spawnY;
        Vector3 spawnPos = new Vector3(spawnX, y, 0f);

        Collider2D hit = Physics2D.OverlapCircle(spawnPos, 3f);
        if (hit != null && !hit.CompareTag("Player"))
        {
            spawnPos.x += 5f;
        }

        Instantiate(questionBoxPrefab, spawnPos, Quaternion.identity);

        StartCoroutine(SpawnLoop());
    }
}
