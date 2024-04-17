using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [SerializeField] private int currentHealth = 100;
    [SerializeField] private SpriteRenderer spriteRenderer;
    public Sprite[] animationSprites;

    private void Start()
    {
        // Ensure sprite renderer is assigned
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }

    // Handle damage when colliding with objects
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if collided with a bullet
        if (collision.CompareTag("Bullet"))
        {
            // Get the bullet component
            Bullet bullet = collision.GetComponent<Bullet>();

            // Check if the bullet component exists
            if (bullet != null)
            {
                // Apply damage to the health
                currentHealth -= bullet.Damage;

                // Update animation frame based on health
                UpdateAnimationFrame();

                // Check if health is zero or less
                if (currentHealth <= 0)
                {
                    Die(); // Die if health reaches zero or less
                }
            }
        }
    }

    // Update animation frame based on current health
    private void UpdateAnimationFrame()
    {
        float healthPercentage = (float)currentHealth / 100f;
        if (healthPercentage == 1.00f )
        {
            // Update animation frame for 100% health
            spriteRenderer.sprite = animationSprites[0];
        }

        if (healthPercentage <= 0.75f && healthPercentage > 0.5f)
        {
            // Update animation frame for 75% health
            spriteRenderer.sprite = animationSprites[1];
        }
        else if (healthPercentage <= 0.5f && healthPercentage > 0.25f)
        {
            // Update animation frame for 50% health
            spriteRenderer.sprite = animationSprites[2];
        }
        else if (healthPercentage <= 0.25f)
        {
            // Update animation frame for 25% health
            spriteRenderer.sprite = animationSprites[3];
        }
        else if (healthPercentage <= 0.15f)
        {
            // Update animation frame for 15% health
            spriteRenderer.sprite = animationSprites[4];
        }

        // No need to check for 0% health because the Die method will handle it
    }

    // Die function to deactivate game object
    private void Die()
    {
        
        // Deactivate the GameObject or perform other actions
        gameObject.SetActive(false);
    }
}
