using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Invader : MonoBehaviour
{
    public Sprite[] animationSprites;

    public float animationTime = 1.0f;
    private SpriteRenderer _spriteRenderer;
    private int _animationFrame;

    public float moveSpeed = 2f;

    public int pointsWorth; // Points awarded for destroying this invader

    public GameObject projectilePrefab;
    public Transform shootPoint;
    public float shootInterval = 10f; // Adjust this to change the time between shots
    public float shootForce = 10f;

    private bool isAlive = true; // Flag to track if the invader is alive

    public int damage = 1; // Damage value for the invader


    private Transform bottomBoundary;
    private Transform topBoundary;

    // Buff system variables
    public BuffSystem.BuffType[] possibleBuffs; // Array of possible buff types
    public GameObject[] buffPrefabs; // Array of corresponding buff prefabs

    // Method to set the boundaries
    public void SetBoundaries(Transform bottomBoundary, Transform topBoundary)
    {
        this.bottomBoundary = bottomBoundary;
        this.topBoundary = topBoundary;
    }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        // Start shooting automatically when the object is enabled
        InvokeRepeating(nameof(Shoot), 1f, shootInterval);
        InvokeRepeating(nameof(AnimateSprite), animationTime, animationTime);
    }

    private void AnimateSprite()
    {
        if (isAlive)
        {
            _animationFrame++;
            if (_animationFrame >= animationSprites.Length)
            {
                _animationFrame = 0;
            }

            _spriteRenderer.sprite = animationSprites[_animationFrame];
        }
    }

    private void Shoot()
    {
        if (!isAlive)
            return; // If the invader is not alive, stop shooting

        GameObject beam = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);

        // Rotate the beam to point downwards (along the negative Y-axis)
        beam.transform.rotation = Quaternion.Euler(0f, 0f, -180f);
    }

    public void Die()
    {
        isAlive = false;

        // Deactivate the GameObject
        gameObject.SetActive(false);
        DropRandomBuff();
    }


    // Method to set the movement speed of the invader
    public void SetMoveSpeed(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
    }

    private void FixedUpdate()
    {
        // Move the invader vertically within the boundaries
        Vector3 newPosition = transform.position + Vector3.down * moveSpeed * Time.fixedDeltaTime;
        newPosition.y = Mathf.Clamp(newPosition.y, bottomBoundary.position.y, topBoundary.position.y);
        transform.position = newPosition;
    }

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

    public int GetDamage()
    {
        return damage;
    }

    
}