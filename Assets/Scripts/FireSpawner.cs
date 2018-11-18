using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpawner : MonoBehaviour {

    public GameObject firePrefab;

    public Transform ground;
    CircleCollider2D groundcol;
    BoxCollider2D firecol;
    SpriteRenderer fireSr;

    float radius;
    float offset;

    float SpawnRate = 2.0f;
    float TimeForNextSpawn;

    void Start () {
        transform.position = Vector2.zero;

        groundcol = ground.GetComponent<CircleCollider2D>();
        firecol = firePrefab.transform.GetChild(0).GetComponent<BoxCollider2D>();
        fireSr = firePrefab.transform.GetChild(0).GetComponent<SpriteRenderer>();

       /* if (firecol == null)
            Debug.Log("No Collider");
            */
        radius = groundcol.bounds.extents.x;
        offset = firecol.size.y;
        //Debug.Log(offset);

        TimeForNextSpawn = 0f;
	}
	
	void Update () {
       // Debug.Log(transform.position);
        Spawn();
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
}
