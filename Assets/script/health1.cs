using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class health1 : MonoBehaviour
{
    [SerializeField] private int currentHealth;
    [SerializeField] private int startingHealth;
    public System.Action killed;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startingHealth;
    }

    // Handle damage when colliding with objects
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if collided with a bullet
        if (collision.CompareTag("Bullet1"))
        {
            // Get the bullet component
            Bullet1 bullet = collision.GetComponent<Bullet1>();

            // Check if the bullet component exists
            if (bullet != null)
            {
                // Apply damage to the health
                currentHealth -= bullet.Damage;

                // Check if health is zero or less
                if (currentHealth <= 0)
                {
                    Die(); // Die if health reaches zero or less
                }
            }
        }
    }

    // Die function to deactivate game object
    private void Die()
    {
        // Invoke the killed action if it's assigned
        killed?.Invoke();

        // Deactivate the GameObject
        gameObject.SetActive(false);
    }
}
