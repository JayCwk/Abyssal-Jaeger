using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invader : MonoBehaviour
{
    public Sprite[] animationSprites;
    public float animationTime = 1.0f;
    private SpriteRenderer _spriteRenderer;
    private int _animationFrame;

    public float speed = 2f;

    public int pointsWorth ; // Points awarded for destroying this invader

    public GameObject projectilePrefab;
    public Transform shootPoint;
    public float shootInterval = 10f; // Adjust this to change the time between shots
    public float shootForce = 10f;

    private bool isAlive = true; // Flag to track if the invader is alive

    

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
       
            // Start shooting automatically when the object is enabled
            InvokeRepeating(nameof(Shoot), 1f, shootInterval);
            InvokeRepeating(nameof(AnimateSprite), animationTime, animationTime);

            // Subscribe to the Die method of the health script
            GetComponent<health>().killed += OnKilled;
        
    }

  

    private void OnKilled()
    {
        // Stop shooting when the invader dies
        CancelInvoke(nameof(Shoot));

        GameManger.instance.UpdateScore(pointsWorth);
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
    public void StopActions()
    {
        // Stop shooting
        CancelInvoke(nameof(Shoot));

        // Stop animation
        CancelInvoke(nameof(AnimateSprite));
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
    }
}
