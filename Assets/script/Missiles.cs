using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missiles : MonoBehaviour
{
    public Vector3 direction = Vector3.down;
    public float speed = 20f;
    public int Damage = 1;

    private bool hasCollided = false; // Flag to track if the bullet has collided with an enemy

    private void Update()
    {
        transform.position += speed * Time.deltaTime * direction;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasCollided && other.CompareTag("Player") || other.CompareTag("BottomBoundary"))
        {
            // Set the flag to true to prevent further collisions
            hasCollided = true;

            // Destroy the game object after colliding with one enemy
            Destroy(gameObject);
        }

    }
}

