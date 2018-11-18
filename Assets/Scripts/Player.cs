using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour {

    [SerializeField] Text healthText;
    [SerializeField] Text ammoText;

    [SerializeField] GameObject bulletPrefab;
    float spawnRate = 0.4f;
    float TimeForNextSpawn;

    [SerializeField] Transform ground;

    float radius;
    float movementSpeed = 1.2f;
    public static float angle = 270f;
    public float spriteAngle = 0f;

    float horizontalextent, verticalExtent;
    /*
    float vel;
    float accel;
    float JumpHeight = 0f;

    bool canJump = true;
    bool isJumping = false;
    bool canShoot = false;
    bool canToggle = true;

    float time = 0f;
    float tempTime = 0f;
    [SerializeField] Text TimeText;*/
        
    public static int toggle = -1;// -1 for inside circle , 1 for outside circle

    CircleCollider2D groundcol;
    BoxCollider2D playercol;
    SpriteRenderer sr;

    public static int health = 10;
    public static int ammo = 5;
    public static bool IsDead = false;

    void Start () {

        verticalExtent = Camera.main.orthographicSize;
        horizontalextent = verticalExtent * Camera.main.aspect;

        spawnRate = 0.4f;
        movementSpeed = 1.2f;

        healthText.transform.position = new Vector2(-horizontalextent + 1f, -verticalExtent + 2f);
        ammoText.transform.position = new Vector2(-horizontalextent + 1f, -verticalExtent + 1f);

        /*
        JumpHeight = 0f;
        
        canJump = true;
        isJumping = false;
        canShoot = false;
        canToggle = true;
        time = 0f;
        tempTime = 0f;*/

        toggle = -1;

        groundcol = ground.GetComponent<CircleCollider2D>();
        playercol = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();

        radius = groundcol.bounds.extents.x + (Player.toggle * playercol.bounds.extents.y);
        //Debug.Log(radius);

        Vector3 pos = transform.position;
        transform.position = new Vector3(pos.x, -radius, pos.z);//Sets the player position on the circumference of the circle
        //vel = 7f;
        //accel = -16f;
        TimeForNextSpawn = 0f;

        healthText.text = "Health : " + health.ToString();
        ammoText.text = "Ammo : " + ammo.ToString();

    }
	
	void Update () {
        radius = groundcol.bounds.extents.x + (Player.toggle * playercol.bounds.extents.y);

        RotatePlayer();
        ChangePosition();

        if (Input.GetMouseButtonDown(0) && ammo > 0)
        {
            Shoot();
            ammo--;
            ammoText.text = "Ammo : " + ammo.ToString();

        }

        if (IsDead)
            Debug.Log("Dead");
    }

    void RotatePlayer()
    {
        Vector3 pos = transform.position;
        angle +=  movementSpeed;
        spriteAngle += movementSpeed;

        if (angle >= 360f) angle -= 360f;
        if (spriteAngle >= 360f) spriteAngle -= 360f;

        pos.x = Mathf.Cos(angle * Mathf.Deg2Rad) * (radius);// + Player.toggle * JumpHeight) );
        pos.y = Mathf.Sin(angle * Mathf.Deg2Rad) * (radius);// + Player.toggle * JumpHeight) );
        transform.position = new Vector3(pos.x, pos.y, pos.z);
        transform.localEulerAngles = new Vector3(0, 0, spriteAngle);
    }

    private void ChangePosition()
    {
        if (Input.GetKeyDown(KeyCode.Space) )
        {
            sr.flipY = !sr.flipY; 
            playercol.offset = new Vector2(playercol.offset.x,-playercol.offset.y);
           // Debug.Log("Changed");
            if (Player.toggle == 1)
                Player.toggle = -1;
            else
                Player.toggle = 1;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.name + " : " + collision.gameObject.tag);

        if (collision.gameObject.tag == "Fire")
        {
            Destroy(collision.gameObject);
            Debug.Log("Fire");
            health--;
            healthText.text = "Health : " + health.ToString();

            if (health == 0)
            {
                gameObject.SetActive(false);
                IsDead = true;
            }
        }
        if (collision.gameObject.tag == "Water")
        {
            Destroy(collision.gameObject);

            Debug.Log("Water");
            AddAmmo();
            ammoText.text = "Ammo : " + ammo.ToString();

        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.transform.position = transform.position;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 dir = (new Vector2(mousePos.x,mousePos.y) - new Vector2(transform.position.x,transform.position.y)).normalized;

        bullet.transform.GetChild(0).GetComponent<Bullet>().Dir = dir;
    }
    void AddAmmo()
    {
        ammo += 5;

        if (ammo > 20)
            ammo = 20;

    }
    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Fire")
            Debug.Log("Fire");
    }*/


}
