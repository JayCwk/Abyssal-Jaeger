using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrallaxBackground : MonoBehaviour
{
    public float parallaxSpeed ; // Adjust this to control the speed of the layer
    private Transform mainCameraTransform;
    private Vector3 lastCameraPosition;

    void Start()
    {
        mainCameraTransform = Camera.main.transform;
        lastCameraPosition = mainCameraTransform.position;
    }

    void Update()
    {
        float deltaX = mainCameraTransform.position.x - lastCameraPosition.x;
        float deltaY = mainCameraTransform.position.y - lastCameraPosition.y;

        // Calculate the parallax effect based on camera movement
        Vector3 parallax = new Vector3(deltaX * parallaxSpeed, deltaY * parallaxSpeed, 0f);
        transform.position += parallax;

        lastCameraPosition = mainCameraTransform.position;
    }
}
