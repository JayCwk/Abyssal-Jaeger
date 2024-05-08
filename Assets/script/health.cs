using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class health : MonoBehaviour
{
    [SerializeField] private int currentHealth;
    [SerializeField] private int startingHealth;
    public System.Action killed;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private Color hitColor = Color.black; // Color to indicate hit

    public GameObject bleedingEffectPrefab; // Reference to the bleeding particle effect prefab
    public BuffSystem.BuffType[] possibleBuffs; // Array of possible buff types
    public GameObject[] buffPrefabs; // Array of corresponding buff prefabs

    AudioManager audiomg;
    public int pointsWorth; // Points awarded for destroying this invader


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startingHealth;

        // Get the SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color; // Store original color

        audiomg = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Handle damage when colliding with objects
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if collided with a bullet
        if (collision.CompareTag("Bullet") || collision.CompareTag("Bullet1"))
        {
            IndicateHit();

            // Get the bullet component
            Bullet bullet = collision.GetComponent<Bullet>();


            // Check if the bullet component exists
            if (bullet != null)
            {
                // Apply damage to the health
                currentHealth -= bullet.Damage;

                // Check if health is zero or less
                if (currentHealth <= 0)
                {
                    Die(); // Die if health reaches zero or less
                    audiomg.PlaySFX(audiomg.EnemyDeath);

                }
                else
                {
                    // Show bleeding particle effect if bullet deals damage
                    // Call a method to show bleeding effect on the enemy
                    ShowBleedingEffect();
                    audiomg.PlaySFX(audiomg.EnemyonHit);
                }
            }
        }
        

    }

    // Method to show bleeding effect (replace with your own implementation)
    private void ShowBleedingEffect()
    {
        // Instantiate the bleeding particle effect prefab at the enemy's position
        Instantiate(bleedingEffectPrefab, transform.position, Quaternion.identity);
    }

    private void IndicateHit()
    {
        // Change color to hit color
        spriteRenderer.color = hitColor;

        // Reset color back to original after a delay
        StartCoroutine(ResetColorAfterDelay());
    }

    private IEnumerator ResetColorAfterDelay()
    {
        // Wait for a short duration
        yield return new WaitForSeconds(0.1f);

        // Reset color back to original
        spriteRenderer.color = originalColor;
    }

    // Handle enemy death
    private void Die()
    {
        // Call the killed action
        killed?.Invoke();

        // Destroy the GameObject
        Destroy(gameObject);

        DropRandomBuff();

        GameManger.instance.UpdateScore(pointsWorth);
    }

    // Method to drop a random buff
    private void DropRandomBuff()
    {
        // Check if either possibleBuffs or buffPrefabs is null or empty
        if (possibleBuffs == null || possibleBuffs.Length == 0 || buffPrefabs == null || buffPrefabs.Length == 0)
        {
            Debug.LogWarning("No possible buffs or buff prefabs defined.");
            return;
        }

        // Randomly select a buff type
        int randomIndex = UnityEngine.Random.Range(0, possibleBuffs.Length);
        BuffSystem.BuffType selectedBuffType = possibleBuffs[randomIndex];

        // Instantiate the corresponding buff prefab
        GameObject buffPrefab = buffPrefabs[randomIndex];
        Instantiate(buffPrefab, transform.position, Quaternion.identity);
    }

}
