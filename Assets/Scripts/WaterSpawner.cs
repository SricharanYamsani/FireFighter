using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSpawner : MonoBehaviour {

    public Transform ground;
    public GameObject waterPrefab;

    CircleCollider2D groundcol;
    SpriteRenderer watersr;
    BoxCollider2D watercol;

    float SpawnRate = 5.0f;
    float TimeForNextSpawn;

    float radius;
    float offset;

    void Start () {
        groundcol = ground.GetComponent<CircleCollider2D>();

        watercol = waterPrefab.transform.GetChild(0).GetComponent<BoxCollider2D>();
        watersr = waterPrefab.transform.GetChild(0).GetComponent<SpriteRenderer>();

        radius = groundcol.bounds.extents.x;
        offset = watercol.size.y/2;

        //Debug.Log("offset " + offset);
        TimeForNextSpawn = 1f;
    }
	
	void Update () {
        Spawn();
	}

    void Spawn()
    {
        if(Time.time > TimeForNextSpawn)
        {
            GameObject water = Instantiate(waterPrefab, transform);

            float angle = Random.Range(0, 360);
            int toggle = Random.Range(0, 2) == 0 ? 1 : -1;

            Vector2 pos = new Vector2(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle));
            pos *= (radius + toggle * offset*2);
            water.transform.position = pos;

            water.transform.localEulerAngles = new Vector3(0, 0, -toggle * 90 + angle);

            Destroy(water.gameObject, 5f);
            TimeForNextSpawn = Time.time + SpawnRate;
        }
    }
}
