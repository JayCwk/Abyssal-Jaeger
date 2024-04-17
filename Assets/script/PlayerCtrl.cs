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
    public Transform shootPoint;
    public float shootInterval = 0.3f; // Adjust this to change the time between shots
    public float shootForce = 10f;

    public SpriteRenderer shipSprite;
    public Sprite ship1;
    public Sprite ship2;
    private Sprite activeShip;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        shipSprite = gameObject.GetComponent<SpriteRenderer>();
        activeShip = ship1;

        // Start shooting automatically when the object is enabled
        InvokeRepeating("Shoot", 0f, shootInterval);
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
                if(activeShip == ship1)
                {
                    activeShip = ship2;
                    shipSprite.sprite = ship2;
                }
                else if (activeShip == ship2)
                {
                    activeShip = ship1;
                    shipSprite.sprite = ship1;
                }

            }
        }

    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(dirX, 0f);
    }


    void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
        
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = shootPoint.up * shootForce; // Shoot in the direction the shootPoint is facing
        }
        // Add any additional logic or effects here
    }

    void ChangeSprite(Sprite sprite)
    {
        shipSprite.sprite = sprite;
    }
}
