using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public float minOffset;
    public float maxOffset;
    public float minDistance;
    public float maxDistance;

    public float spawnIntervalMin;
    public float spawnIntervalMax;
    public float spawnTimer;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.C))
        {
            SpawnEnemy();
        }

        UpdateSpawnTimer();
	}

    void UpdateSpawnTimer()
    {
        if (PlayerController.instance.dayNightControl.currentTime < PlayerController.instance.dayNightControl.dayStart || PlayerController.instance.dayNightControl.currentTime > PlayerController.instance.dayNightControl.dayEnd)
        {
            if (spawnTimer > 0)
            {
                spawnTimer -= Time.deltaTime;

                if (spawnTimer < 0)
                {
                    SpawnEnemy();
                    spawnTimer = Random.Range(spawnIntervalMin, spawnIntervalMax);
                }
            }

        }
    }

    void SpawnEnemy()
    {
        //Spawn enemy object
        GameObject newEnemy = Instantiate(EnemyPrefab);

        //Spawns a certain distance away in any direction direction
        //Create temp position snapshot
        Vector3 enemySpawnPosition = PlayerController.instance.transform.position;

        //Sets the X and Z positions to ranomly set between min and max value, then randomly inverts it.
        enemySpawnPosition.x += Random.Range(minDistance, maxDistance) * (Random.Range(0, 2) == 0 ? -1 : 1);
        enemySpawnPosition.z += Random.Range(minDistance, maxDistance) * (Random.Range(0, 2) == 0 ? -1 : 1);

        Debug.Log("Spawning enemy at: " + enemySpawnPosition);

        //Apply calculated position
        newEnemy.transform.position = enemySpawnPosition;


    }
}
