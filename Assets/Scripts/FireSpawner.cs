using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpawner : MonoBehaviour {

    public GameObject firePrefab;

    public Transform ground;
    CircleCollider2D groundcol;
    BoxCollider2D firecol;
    SpriteRenderer fireSr;

    public Transform Building;

    float radius;
    float offset;

    float SpawnRate = 2.0f;
    float TimeForNextSpawn;

    float spawnX, spawnY;

    public static int fireCount = 0;
    void Start () {
        transform.position = Vector2.zero;

        groundcol = ground.GetComponent<CircleCollider2D>();
        firecol = firePrefab.transform.GetChild(0).GetComponent<BoxCollider2D>();
        fireSr = firePrefab.transform.GetChild(0).GetComponent<SpriteRenderer>();

        radius = groundcol.bounds.extents.x;
        offset = firecol.size.y;
        //Debug.Log(offset);

        spawnX = Building.GetComponent<BoxCollider2D>().bounds.extents.x;
        spawnY = Building.GetComponent<BoxCollider2D>().bounds.extents.y;

        TimeForNextSpawn = 0f;

        for (int i = 0; i < 10; i++)
            SpawnOnBuilding();

        InvokeRepeating("SpawnOnBuilding", 2.5f, 2.5f);
	}
	
	void Update () {
       // Debug.Log(transform.position);
         //Spawn();

        if (fireCount == 0)
            Debug.Log("You Win!");
        else if (fireCount > 25)
            Debug.Log("The building was burnt");
	}

    void Spawn()
    {
        if(Time.time > TimeForNextSpawn)
        {
            GameObject fire = Instantiate(firePrefab,transform);
            float angle = Random.Range(0, 360);
            int toggle = Random.Range(0, 2) == 0 ? 1 : -1;
            //int toggle = -1;
            Vector2 pos = new Vector2(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle)) * (radius + (toggle * offset/2));// + toggle * firecol.bounds.extents.x);
            fire.transform.position =  pos;
            fire.transform.localEulerAngles = new Vector3(0, 0, -toggle * 90 +angle);
            // Debug.Log(pos);
            /*if (toggle == -1)
                fireSr.flipY = false;
            else
                fireSr.flipY = true;
*/
            TimeForNextSpawn = Time.time + SpawnRate;
        }
    }

    void SpawnOnBuilding()
    {
        float randX = Random.Range(-spawnX / 2, spawnX / 2);
        float randY = Random.Range(-spawnY / 2, spawnY / 2);

        GameObject fire = Instantiate(firePrefab, Building);
        fire.transform.position = new Vector2(randX, randY);

        fire.transform.localScale = Vector3.one * 0.5f;
        fireCount++;
        Debug.Log(fireCount);
    }
}
