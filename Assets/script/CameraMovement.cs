using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private float minXBoundary;
    private float maxXBoundary;

    void Start()
    {
        // Start the coroutine to increase speed over time
        StartCoroutine(IncreaseSpeedOverTime());
    }

    void Update()
    {
        // Calculate new target position based on automatic movement
        float newX = Mathf.PingPong(Time.time * moveSpeed, maxXBoundary - minXBoundary) + minXBoundary;
        Vector3 targetPosition = new Vector3(newX, transform.position.y, transform.position.z);

        // Smoothly move the camera towards the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);
    }

    public void SetBoundaries(float minX, float maxX)
    {
        // Set the boundaries for camera movement
        minXBoundary = minX;
        maxXBoundary = maxX;
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
