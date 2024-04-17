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
    public float shootInterval = 1f; // Adjust this to change the time between shots
    public float shootForce = 10f;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Start shooting automatically when the object is enabled
        InvokeRepeating("Shoot", 0f, shootInterval);
    }

    
    void Update()
    {
        dirX = Input.acceleration.x * moveSpeed;
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -5.8f, 5.8f), transform.position.y);
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
}
