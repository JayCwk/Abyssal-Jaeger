using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float moveSpeed = 0.5f;
    public float minX = -4.84f; // Define left boundary
    public float maxX = 5.3f;  // Define right boundary

    private Vector3 targetPosition;

    void Start()
    {
        // Set initial target position to current position
        targetPosition = transform.position;

        // Start the coroutine to increase speed over time
        StartCoroutine(IncreaseSpeedOverTime());
    }

    void Update()
    {
        // Calculate new target position based on automatic movement
        float newX = Mathf.PingPong(Time.time * moveSpeed, maxX - minX) + minX;
        targetPosition = new Vector3(newX, transform.position.y, transform.position.z);

        // Apply boundary constraints
        targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);

        // Smoothly move the camera towards the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);
    }
    private IEnumerator IncreaseSpeedOverTime()
    {
        // Wait for 30 seconds before starting to increase speed
        yield return new WaitForSeconds(30f);

        while (true)
        {
            // Increase speed by 0.3
            moveSpeed += 0.3f;

            // Wait for the next 30 seconds
            yield return new WaitForSeconds(30f);
        }
    }
}
