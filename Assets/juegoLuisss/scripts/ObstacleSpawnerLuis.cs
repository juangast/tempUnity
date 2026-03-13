using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObstacleSpawnerLuis : MonoBehaviour
{
    public GameObject barrel;
    public GameObject log;
    public GameObject ruinsStone;
    public GameObject stone;
    public GameObject truncatedTrunk;
    public GameObject venomousPlant;
    public GameObject treeDown;

    public Transform player;
    public float spawnDistanceAhead = 20f;

    public float minSpawnInterval = 1.5f;
    public float maxSpawnInterval = 3.5f;

    private float venomousPlantSpawnY = -2.6f;
    private float truncatedTrunkSpawnY = -2.6f;
    private float logSpanwnY = -2.5f;

    private GameObject[] jumpObstacles;
    private bool lastWasCrouch;
    private bool treeDownEnabled = true;

    void Start()
    {
        string excluded = PlayerPrefs.GetString("ExcludedObstacles", "");
        HashSet<string> excludedSet = new HashSet<string>();
        if (!string.IsNullOrEmpty(excluded))
        {
            foreach (string s in excluded.Split(','))
                excludedSet.Add(s.Trim());
        }

        treeDownEnabled = !excludedSet.Contains("treeDown");

        List<GameObject> obstacles = new List<GameObject>();
        if (!excludedSet.Contains("barrel")) obstacles.Add(barrel);
        if (!excludedSet.Contains("log")) obstacles.Add(log);
        if (!excludedSet.Contains("ruinsStone")) obstacles.Add(ruinsStone);
        if (!excludedSet.Contains("stone")) obstacles.Add(stone);
        if (!excludedSet.Contains("truncatedTrunk")) obstacles.Add(truncatedTrunk);
        if (!excludedSet.Contains("venomousPlant")) obstacles.Add(venomousPlant);
        jumpObstacles = obstacles.ToArray();

        minSpawnInterval = PlayerPrefs.GetFloat("MinSpawnInterval", minSpawnInterval);
        maxSpawnInterval = PlayerPrefs.GetFloat("MaxSpawnInterval", maxSpawnInterval);

        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        
        float wait = Random.Range(minSpawnInterval, maxSpawnInterval);
        yield return new WaitForSeconds(wait);

        bool canSpawnCrouch = !lastWasCrouch && treeDownEnabled;
        bool spawnCrouch = canSpawnCrouch && Random.value < 0.8f;

        GameObject prefab;
        if (spawnCrouch)
        {
            prefab = treeDown;
            lastWasCrouch = true;
        }
        else
        {
            prefab = jumpObstacles[Random.Range(0, jumpObstacles.Length)];
            lastWasCrouch = false;
        }

        float spawnY;
        if (prefab == treeDown)
            spawnY = 0f;
        else if (prefab == venomousPlant)
            spawnY = venomousPlantSpawnY;
        else if (prefab == truncatedTrunk)
            spawnY = truncatedTrunkSpawnY;
        else if (prefab == log)
            spawnY = logSpanwnY;
        else
            spawnY = -2.4f;
        Vector3 spawnPos = new Vector3(player.position.x + spawnDistanceAhead, spawnY, 0f);
        Instantiate(prefab, spawnPos, Quaternion.identity);
        StartCoroutine(SpawnLoop());
    }
}
