using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public Vector2 Dir { get; set; }

    float speed = 30f;
	void Start () {
        Destroy(this.gameObject, 2f);
       }
	
	void Update () {
        transform.Translate(Dir * speed * Time.deltaTime);
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Trigger Collision with : " + collision.gameObject.name);

        if (collision.gameObject.tag == "Fire")
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }

        if(collision.gameObject.tag == "Building")
        {
            Destroy(this.gameObject);
            FireSpawner.fireCount--;
            Debug.Log(FireSpawner.fireCount);
            Destroy(collision.transform.GetChild(collision.transform.childCount - 1).gameObject);
        }
    }
}
