using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [SerializeField] private int currentHealth;
    [SerializeField] private int startingHealth = 100;
    [SerializeField] private SpriteRenderer spriteRenderer;
    public Sprite[] animationSprites;
    public Transform[] shootingPoints;
    public GameObject beamPrefab1;
    public GameObject beamPrefab2;
    public float fixedTimeIntervals = 3f;
    public int pointsWorth; // Points awarded for destroying this boss
    public int pointsGet; // Points awarded for hitting this boss

    public System.Action killed;

    private bool isGameActive = true;
    private Coroutine shootingCoroutine;

    private void Start()
    {

        // Ensure sprite renderer is assigned
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        startingHealth = currentHealth;
        // Start shooting beams
        shootingCoroutine = StartCoroutine(ShootBeams());

    }
    // Coroutine to shoot beams from random shooting points
    private IEnumerator ShootBeams()
    {
        while (true)
        {
            // Randomly select a shooting point
            Transform shootingPoint = shootingPoints[Random.Range(0, shootingPoints.Length)];

            // Set the shooting animation frame only when shooting
            SetShootingAnimationFrame(true);

            // Wait for a short duration to indicate the shooting action
            yield return new WaitForSeconds(0.5f);
            // Randomly select a beam type
            GameObject beamPrefabToUse = Random.Range(0, 2) == 0 ? beamPrefab1 : beamPrefab2;

            // Instantiate the selected beam prefab at the shooting point position and rotation
            GameObject beam = Instantiate(beamPrefabToUse, shootingPoint.position, Quaternion.identity);

            // Rotate the beam to point downwards (along the negative Y-axis)
            beam.transform.rotation = Quaternion.Euler(0f, 0f, -180f);

            // Update the animation frame based on current health
            UpdateAnimationFrame();

            // Wait for the fixed time interval between shots
            yield return new WaitForSeconds(fixedTimeIntervals);

            // Reset the shooting animation frame after shooting
            SetShootingAnimationFrame(false);
        }
    }

    // Set the animation frame for shooting state
    private void SetShootingAnimationFrame(bool isShooting)
    {
        // Set the sprite for shooting state
        if (isShooting)
        {
            spriteRenderer.sprite = animationSprites[5]; // Assuming the shooting sprite is at index 5
        }
        else
        {
            // Set the sprite based on the health status
            UpdateAnimationFrame();
        }
    }

    // Update animation frame based on current health
    private void UpdateAnimationFrame()
    {
        float healthPercentage = (float)currentHealth / startingHealth;
        int currentSpriteIndex = GetAnimationFrameIndex(healthPercentage);
        spriteRenderer.sprite = animationSprites[currentSpriteIndex];
    }

    // Get the index of the current animation frame based on health percentage
    private int GetAnimationFrameIndex(float healthPercentage)
    {
        if (healthPercentage >= 0.75f) return 0;
        else if (healthPercentage >= 0.5f) return 1;
        else if (healthPercentage >= 0.25f) return 2;
        else if (healthPercentage >= 0.15f) return 3;
        else return 4;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet1") || collision.CompareTag("Bullet"))
        {
            Bullet1 bullet1 = collision.GetComponent<Bullet1>();
            Bullet bullet = collision.GetComponent<Bullet>();
            if (bullet != null || bullet1 != null)
            {
                currentHealth -= bullet != null ? bullet.Damage : bullet1.Damage;
                GameManger.instance.UpdateScore(pointsGet);
                if (currentHealth <= 0) Die();
                else UpdateAnimationFrame();
            }
        }
    }

    private void Die()
    {
        killed?.Invoke();
        gameObject.SetActive(false);
        GameManger.instance.UpdateScore(pointsWorth);
    }
}