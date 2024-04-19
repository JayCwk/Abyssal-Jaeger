using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerCtrl : MonoBehaviour
{
    private Rigidbody2D rb;
    float dirX;
    float moveSpeed = 20f;

    public GameObject projectilePrefab;
    public GameObject projectile1Prefab;
    public Transform shootPoint;
    public float shootInterval = 0.3f; // Adjust this to change the time between shots
    public float shootForce = 10f;

    public int StartingsharedHealth = 3; // Shared health value for both ships
    public int CurrentsharedHealth;

    private SpriteRenderer shipSprite;
    public Sprite[] ship1;
    public Sprite[] ship2;
    private Sprite[] activeShip;

    private bool isAlive = true; // Flag to track if the player is alive or not

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        shipSprite = gameObject.GetComponent<SpriteRenderer>();
        activeShip = ship1;

        CurrentsharedHealth = StartingsharedHealth;

        // Start shooting automatically when the object is enabled
        InvokeRepeating("Shoot", 1f, shootInterval);
    }


    void Update()
    {
        //player tilting movement
        dirX = Input.acceleration.x * moveSpeed;
        //set movement area
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -5.8f, 5.8f), transform.position.y);

        for (int i = 0; i < Input.touchCount; i++)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                Debug.Log("touch");
                if (activeShip == ship1)
                {
                    activeShip = ship2;
                    shipSprite.sprite = ship2[0];
                }
                else if (activeShip == ship2)
                {
                    activeShip = ship1;
                    shipSprite.sprite = ship1[0];
                }
            }
        }

        // Update sprites based on current shared health
        if (isAlive) 
        {
            shipSprite.sprite = activeShip[StartingsharedHealth - CurrentsharedHealth];
        }
            
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isAlive && collision.CompareTag("Missile"))
        {
            // Get the bullet component
            Missiles bullet = collision.GetComponent<Missiles>();

            // Check if the bullet component exists
            if (bullet != null)
            {
                // Apply damage to the health
                CurrentsharedHealth -= bullet.Damage;

                // Check if health is zero or less
                if (CurrentsharedHealth <= 0)
                {
                    Die(); // Die if health reaches zero or less
                }
            }
        }
    }

    private void Die()
    {
        isAlive = false;

        // Deactivate the GameObject
        gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(dirX, 0f);
    }


    void Shoot()
    {
        if(!isAlive)
        {
            return;
        }
        
        GameObject projectilePrefabToUse = activeShip == ship1 ? projectilePrefab : projectile1Prefab;

        GameObject projectile = Instantiate(projectilePrefabToUse, shootPoint.position, shootPoint.rotation);


        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = shootPoint.up * shootForce; // Shoot in the direction the shootPoint is facing
        }

    }
}
