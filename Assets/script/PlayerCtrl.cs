using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;



public class PlayerCtrl : MonoBehaviour
{
    private Rigidbody2D rb;
    float dirX;
    float moveSpeed = 20f;

    public GameObject projectilePrefab;
    public GameObject projectile1Prefab;
    public Transform shootPoint;
    public Transform shootPoint1;
    public Transform shootPoint2;
    public Transform shieldPosition;
    public float shootInterval = 0.3f; // Adjust this to change the time between shots
    public float shootForce = 10f;

    public int StartingsharedHealth = 3; // Shared health value for both ships
    public int CurrentsharedHealth;

    public GameObject ExplosionEffectPrefab;

    private SpriteRenderer shipSprite;
    public Sprite[] ship1;
    public Sprite[] ship2;
    private Sprite[] activeShip;

    private bool isAlive = true; // Flag to track if the player is alive or not

    private bool isTripleShotEnabled = false;
    private bool isShieldEnabled = false;

    AudioManager audiomg;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        shipSprite = gameObject.GetComponent<SpriteRenderer>();
        activeShip = ship1;

        CurrentsharedHealth = StartingsharedHealth;

        // Load player preferences data
        LoadPlayerPrefs();

        // Start shooting automatically when the object is enabled
        InvokeRepeating("Shoot", 1f, shootInterval);

        audiomg = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Call this method when starting a new game
    public void StartNewGame()
    {
        // Reset player preferences data
        ResetPlayerPrefs();

        // Add any other initialization code for starting a new game here
    }
    // This method resets the player preferences data
    public static void ResetPlayerPrefs()
    {
        // Reset specific keys relevant to player preferences
        PlayerPrefs.DeleteKey("PlayerSharedHealth");
        PlayerPrefs.DeleteKey("ShipColor_R");
        PlayerPrefs.DeleteKey("ShipColor_G");
        PlayerPrefs.DeleteKey("ShipColor_B");
        PlayerPrefs.DeleteKey("ShipColor_A");

        // Save the changes
        PlayerPrefs.Save();
    }
    void LoadPlayerPrefs()
    {
        // Load the current shared health from player preferences
        CurrentsharedHealth = PlayerPrefs.GetInt("PlayerSharedHealth", StartingsharedHealth);

        // Load the ship color from player preferences
        LoadShipColor();
    }

    void SavePlayerPrefs()
    {
        // Save the current shared health to player preferences
        PlayerPrefs.SetInt("PlayerSharedHealth", CurrentsharedHealth);

        // Save the ship color to player preferences
        SaveShipColor();

        PlayerPrefs.Save(); // Optional: Save changes immediately
    }

    void LoadShipColor()
    {
        // Load the ship color from player preferences
        Color savedColor = new Color(
            PlayerPrefs.GetFloat("ShipColor_R", 1f),
            PlayerPrefs.GetFloat("ShipColor_G", 1f),
            PlayerPrefs.GetFloat("ShipColor_B", 1f),
            PlayerPrefs.GetFloat("ShipColor_A", 1f)
        );

        // Apply the loaded color to the ship sprite renderer
        shipSprite.color = savedColor;
    }

    void SaveShipColor()
    {
        // Save the current ship color to player preferences
        PlayerPrefs.SetFloat("ShipColor_R", shipSprite.color.r);
        PlayerPrefs.SetFloat("ShipColor_G", shipSprite.color.g);
        PlayerPrefs.SetFloat("ShipColor_B", shipSprite.color.b);
        PlayerPrefs.SetFloat("ShipColor_A", shipSprite.color.a);
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

        int shipIndex = Mathf.Clamp(StartingsharedHealth - CurrentsharedHealth, 0, activeShip.Length - 1);
        shipSprite.sprite = activeShip[shipIndex];
        if (shipIndex >= 0 && shipIndex < activeShip.Length)
        {
            shipSprite.sprite = activeShip[shipIndex];
        }
        else
        {
            // Handle the case where the index is out of bounds
            Debug.LogWarning("Invalid ship index: " + shipIndex);
        }

    }

    private void ShowBleedingEffect()
    {
        // Instantiate the bleeding particle effect prefab at the enemy's position
        Instantiate(ExplosionEffectPrefab, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isAlive && collision.CompareTag("Missile"))
        {
            Debug.Log("Player was hit by a bullet!");
            Missiles bullet = collision.GetComponent<Missiles>();
            if (bullet != null)
            {

                Debug.Log("Bullet Damage: " + bullet.Damage);
                CurrentsharedHealth -= bullet.Damage;
                ShowBleedingEffect();
                audiomg.PlaySFX(audiomg.PlayeronHit);
                Debug.Log("Current Health: " + CurrentsharedHealth);
                SavePlayerPrefs();
                if (CurrentsharedHealth <= 0)
                {
                    Debug.Log("Player died!");
                    Die();
                    audiomg.PlaySFX(audiomg.Playerdeath);
                }

            }

        }

        if (isAlive && collision.CompareTag("Variant") || collision.CompareTag("Enemy"))
        {
            Debug.Log("Player was hit by a invader!");
            Invader invader = collision.GetComponent<Invader>();
            if (invader != null)
            {
                TakeDamage(invader.GetDamage());
                audiomg.PlaySFX(audiomg.PlayeronHit);
                invader.Die(); // Destroy the invader upon collision with the player
                SavePlayerPrefs();
                if (CurrentsharedHealth <= 0)
                {
                    Debug.Log("Player died!");
                    Die();
                    audiomg.PlaySFX(audiomg.Playerdeath);
                }

            }

        }

        if (collision.CompareTag("Buff"))
        {
            BuffSystem buffSystem = FindObjectOfType<BuffSystem>();
            if (buffSystem != null)
            {
                // Generate a random index to select a random buff type from the BuffType enum
                int randomIndex = Random.Range(0, System.Enum.GetValues(typeof(BuffSystem.BuffType)).Length);
                BuffSystem.BuffType randomBuffType = (BuffSystem.BuffType)randomIndex;

                // Apply the randomly selected buff to the player
                buffSystem.ApplyBuff(randomBuffType, 10f); // Adjust duration as needed
                Destroy(collision.gameObject); // Destroy the buff GameObject
                audiomg.PlaySFX(audiomg.PowerUp);
            }
            else
            {
                Debug.LogWarning("BuffSystem not found!");
            }
        }
    }

    private void TakeDamage(int damage)
    {
        CurrentsharedHealth -= damage;
        Debug.Log("Player took " + damage + " damage. Current Health: " + CurrentsharedHealth);

        ShowBleedingEffect();
        if (CurrentsharedHealth <= 0)
        {

            Die();
        }
    }

    public void EnableTripleShot()
    {
        isTripleShotEnabled = true;
    }

    public void ActivateShield()
    {
        isShieldEnabled = true;

    }

    public void DisableTripleShot()
    {
        isTripleShotEnabled = false;
    }

    public void DeactivateShield()
    {
        isShieldEnabled = false;

    }


    private void Die()
    {
        isAlive = false;

        // Deactivate the GameObject
        gameObject.SetActive(false);
        SceneManager.LoadScene("GameOver");
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(dirX, 0f);
    }

    void Shoot()
    {
        if (!isAlive)
        {
            return;
        }

        GameObject projectilePrefabToUse;
        Vector3 shootPointPosition;
        Vector3 shootPointPosition1;
        Vector3 shootPointPosition2;

        if (isTripleShotEnabled)
        {
            // Implement logic to shoot from three shooting points
            projectilePrefabToUse = projectilePrefab;
            shootPointPosition = shootPoint.position;
            shootPointPosition1 = shootPoint1.position;
            shootPointPosition2 = shootPoint2.position;

            projectilePrefabToUse = activeShip == ship1 ? projectilePrefab : projectile1Prefab;
            Instantiate(projectilePrefabToUse, shootPointPosition, shootPoint.rotation);
            Instantiate(projectilePrefabToUse, shootPointPosition1, shootPoint1.rotation);
            Instantiate(projectilePrefabToUse, shootPointPosition2, shootPoint2.rotation);

            audiomg.PlaySFX(audiomg.shoot);
        }
        else if (isShieldEnabled) //changed to double shot
        {
            // Implement logic to shoot from two shooting points when shield is enabled
            projectilePrefabToUse = activeShip == ship1 ? projectilePrefab : projectile1Prefab;
            shootPointPosition1 = shootPoint1.position;
            shootPointPosition2 = shootPoint2.position;

            Instantiate(projectilePrefabToUse, shootPointPosition1, shootPoint1.rotation);
            Instantiate(projectilePrefabToUse, shootPointPosition2, shootPoint2.rotation);

            audiomg.PlaySFX(audiomg.shoot);
        }
        else
        {
            // Implement logic to shoot from the middle shooting point
            projectilePrefabToUse = activeShip == ship1 ? projectilePrefab : projectile1Prefab;
            shootPointPosition = shootPoint.position;

            Instantiate(projectilePrefabToUse, shootPointPosition, shootPoint.rotation);

            audiomg.PlaySFX(audiomg.shoot);
        }
    }
}


