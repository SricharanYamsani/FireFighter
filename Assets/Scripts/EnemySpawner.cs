using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField] Transform Ground;
    float radius;

    [SerializeField] GameObject enemyPrefab;
    float SpawnRate = 2f;
    float TimeForNextSpawn;

    public static int count = 0;
    public int maxCount ;
    static float time = 0;
	void Start () {
        TimeForNextSpawn = 0f;
        time = 0;
        
        SpawnRate = 2f;
        maxCount = 1;
	}
	
	
	void Update () {
        radius = Ground.transform.localScale.x / 2 + (Player.toggle * enemyPrefab.transform.localScale.x / 2);
        EnemySpawner.time += Time.deltaTime;
        Spawn();
        IncreaseCount();
	}

    void Spawn()
    {
        if(Time.time > TimeForNextSpawn && EnemySpawner.count < maxCount)
        {
            bool Boss = Random.Range(0f, 1f) < 0.2f ? true : false;
            
            if (!Boss)
            {
                GameObject enemy = Instantiate(enemyPrefab);
                enemy.GetComponent<Enemy>().Radius = radius;
                if (Player.toggle == 1)
                    enemy.GetComponent<MeshRenderer>().material.color = Color.yellow;
                else
                    enemy.GetComponent<MeshRenderer>().material.color = Color.cyan;
            }
            else
            {
                GameObject enemy = Instantiate(enemyPrefab);
                Vector3 scale = enemy.transform.localScale;
                enemy.transform.localScale = new Vector3(scale.x * 2, scale.y , scale.z);
                enemy.GetComponent<Enemy>().Radius = Ground.transform.localScale.x / 2;
                enemy.GetComponent<MeshRenderer>().material.color = Color.green;
            }
            TimeForNextSpawn = Time.time + SpawnRate;
            EnemySpawner.count++;
        }
    }

    void IncreaseCount()
    {
        if (EnemySpawner.time > 8f)
        {
            maxCount += 1;
            EnemySpawner.time = 0f;
        }
    }
}
