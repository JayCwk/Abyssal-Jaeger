using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet1 : MonoBehaviour
{
    public Vector3 direction = Vector3.up;
    public float speed = 20f;
    public int Damage = 1;

    private bool hasCollided = false; // Flag to track if the bullet has collided with an enemy

    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private Color hitColor = Color.red; // Color to indicate hit

    private void Start()
    {
        // Get the SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color; // Store original color
    }

    private void Update()
    {
        transform.position += speed * Time.deltaTime * direction;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasCollided && (other.CompareTag("Variant") || other.CompareTag("Boss") || other.CompareTag("Enemy") || other.CompareTag("TopBoundary")))
        {
            // Set the flag to true to prevent further collisions
            hasCollided = true;

            // If the bullet doesn't deal damage, change its color to indicate hit
            StartCoroutine(IndicateHit());

            // Destroy the game object after colliding with an enemy
            Destroy(gameObject);
        }
    }

    // Coroutine to indicate hit by changing color
    private System.Collections.IEnumerator IndicateHit()
    {
        // Change color to hit color
        spriteRenderer.color = hitColor;

        // Wait for a short duration
        yield return new WaitForSeconds(0.1f);

        // Reset color back to original
        spriteRenderer.color = originalColor;
    }
}

