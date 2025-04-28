using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using TopDownCharacter2D.Controllers; // Required for LINQ queries (like Count)

public class GameMaster: MonoBehaviour {
    public GameObject statController;
    public CardSelectionScreen cardSelectionScreen;
    public int maxEnemies = 5;
    public GameObject goblinPrefab;
    public GameObject knightPrefab;
    public float spawnDelay = 1;
    private List<Transform> spawners;
    private int currentEnemies = 0;
    private float lastSpawnTime = -1f;

    private bool chestSpawned = false;

    public GameObject chestPrefab;
    public Transform player;

    void Start() {
        spawners = GameObject.FindGameObjectsWithTag("Spawner")
                           .Select(go => go.transform)
                           .ToList();

        if(spawners.Count == 0) {
            Debug.LogError("No spawners found with the tag 'Spawner'!");
            return;
        }
    }

    void FixedUpdate() {
        currentEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if(currentEnemies < maxEnemies && Time.time > lastSpawnTime + spawnDelay) {
            SpawnEnemy();
        }

        if(currentEnemies == 0 && GameObject.FindGameObjectWithTag("Chest") == null && player != null && chestSpawned == false) //Check if there are no enemies, there is no chest already and the player exists
        {
            SpawnChest();
            chestSpawned = true;
        }
    }

    void SpawnEnemy() {
        if(spawners.Count == 0) return;

        int randomIndex = Random.Range(0, spawners.Count);
        Transform selectedSpawner = spawners[randomIndex];

        float randomValue = Random.value;

        GameObject enemyToSpawn;

        if(randomValue < 0.7f)
        {
            enemyToSpawn = goblinPrefab;
        } else {
            enemyToSpawn = knightPrefab;
        }
        GameObject newEnemy = Instantiate(enemyToSpawn, selectedSpawner.position, selectedSpawner.rotation);

        TopDownContactEnemyController enemyController = newEnemy.GetComponent<TopDownContactEnemyController>();

        if(enemyController != null && statController != null) {
            enemyController.statController = statController;
        } else {
            if(enemyController == null)
                Debug.LogError("TopDownContactEnemyController not found on spawned enemy!");
            if(statController == null)
                Debug.LogError("StatController script not found in the scene. Assign it to the GameMaster!");
        }

        lastSpawnTime = Time.time;
    }


    void SpawnChest() {
        Vector3 chestPosition = player.position + Vector3.up * 2f;
        GameObject chest = Instantiate(chestPrefab, chestPosition, Quaternion.identity);
        chest.GetComponent<ChestInteract>().cardSelectionScreen = cardSelectionScreen;
    }
}

