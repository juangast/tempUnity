using UnityEngine;
using System.Collections;

public class CoinSpawnerLuis : MonoBehaviour
{
    public GameObject coinPrefab;
    public Transform player;
    public float spawnDistanceAhead = 20f;
    public float minSpawnInterval = 2.5f;
    public float maxSpawnInterval = 4.5f;
    public float coinSpacingX = 1.2f;
    public float coinSpacingY = 1.0f;

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        float wait = Random.Range(minSpawnInterval, maxSpawnInterval);
        yield return new WaitForSeconds(wait);

        float xOffset = Random.Range(-3f, 3f);
        float spawnX = player.position.x + spawnDistanceAhead + xOffset;
        float spawnY = Random.Range(-1.5f, -0.5f);

        Vector3 spawnPos = new Vector3(spawnX, spawnY, 0f);

        int groupSize = GetGroupSize();
        Vector2[] offsets = GetFormation(groupSize);

        float checkWidth = coinSpacingX + 3f;
        float checkHeight = (groupSize >= 3 ? coinSpacingY : 0f) + 3f;
        Collider2D hit = Physics2D.OverlapBox(spawnPos, new Vector2(checkWidth, checkHeight), 0f);
        if (hit != null && !hit.CompareTag("Player"))
        {
            spawnPos.x += checkWidth + 2f;
        }

        for (int i = 0; i < offsets.Length; i++)
        {
            Vector3 pos = spawnPos + new Vector3(offsets[i].x, offsets[i].y, 0f);
            Instantiate(coinPrefab, pos, Quaternion.identity);
        }

        StartCoroutine(SpawnLoop());
    }

    int GetGroupSize()
    {
        float roll = Random.value;
        if (roll < 0.15f) return 1;
        if (roll < 0.35f) return 2;
        if (roll < 0.70f) return 3;
        return 4;
    }

    Vector2[] GetFormation(int size)
    {
        switch (size)
        {
            case 2:
                return new[] {
                    new Vector2(-0.5f * coinSpacingX, 0f),
                    new Vector2(0.5f * coinSpacingX, 0f)
                };
            case 3:
                return new[] {
                    new Vector2(-0.5f * coinSpacingX, 0f),
                    new Vector2(0.5f * coinSpacingX, 0f),
                    new Vector2(0f, coinSpacingY)
                };
            case 4:
                return new[] {
                    new Vector2(-0.5f * coinSpacingX, 0f),
                    new Vector2(0.5f * coinSpacingX, 0f),
                    new Vector2(-0.5f * coinSpacingX, coinSpacingY),
                    new Vector2(0.5f * coinSpacingX, coinSpacingY)
                };
            default:
                return new[] { Vector2.zero };
        }
    }
}
