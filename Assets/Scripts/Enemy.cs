using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    float angle , initAngle;
    float movementSpeed = 1f;
    float radius;
    public float Radius
    {
        set
        {
            radius = value;
        }
    }
    

	void Start () {
        do
        {
            angle = Random.Range(0f, 360f);
        } while (Mathf.Abs(Player.angle % 360 - angle) < 120f) ;
        //Debug.Log(Player.angle);
        transform.position = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad) * radius, Mathf.Sin(angle * Mathf.Deg2Rad) * radius);

        initAngle = angle;
        movementSpeed *= Random.Range(0f, 1f) > 0.5f ? 1 : -1;

       // Destroy(this.gameObject, 10f);

    }


    void Update () {
        RotatePlayer();
        CheckRotation();
	}

    void RotatePlayer()
    {
        Vector3 pos = transform.position;
        angle += movementSpeed;
        pos.x = Mathf.Cos(angle * Mathf.Deg2Rad) * radius ;
        pos.y = Mathf.Sin(angle * Mathf.Deg2Rad) * radius ;
        transform.position = new Vector3(pos.x, pos.y, pos.z);
        transform.localEulerAngles = new Vector3(0, 0, angle);
    }
    void CheckRotation()
    {
        if (Mathf.Abs(angle - initAngle) >= 720f)
        {
            Destroy(this.gameObject);
            EnemySpawner.count--;

        }
    }

}
