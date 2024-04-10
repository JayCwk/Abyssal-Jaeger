using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class invaders : MonoBehaviour
{
    public invader[] prefabs;
    public int columns = 5;
    public float speed = 0.5f;
    public Transform leftBoundary;
    public Transform rightBoundary;

    private Vector3 _direction = Vector2.right;
    private List<Transform> inVader = new List<Transform>();
    private float rowHeight = 2.0f;
    private bool moveDown = false;

    private void Start()
    {
        SpawnInitialRow();
    }

    private void Update()
    {
        MoveInvaders();
        CheckEdges();
    }

    private void SpawnInitialRow()
    {
        Vector3 initialRowPosition = CalculateInitialRowPosition();
        SpawnRow(initialRowPosition);
    }

    private void MoveInvaders()
    {
        transform.position += _direction * speed * Time.deltaTime;


        if (moveDown)
        {
            MoveDown();
            moveDown = false; // Reset moveDown after moving down
            Vector3 newPosition = inVader[inVader.Count - columns].position + new Vector3(0.0f, rowHeight, 0.0f);
            SpawnRow(newPosition);
        }
    }

    private void CheckEdges()
    {
        bool invaderTriggeredMoveDown = false; // Flag to track if any invader has triggered move down

        foreach (Transform inv in inVader)
        {
            if (inv.position.x <= leftBoundary.position.x || inv.position.x >= rightBoundary.position.x)
            {
                // Check if the invader's position is within the descent range of the left or right boundary
                if ((inv.position.x <= leftBoundary.position.x && _direction == Vector3.left) ||
                   (inv.position.x >= rightBoundary.position.x && _direction == Vector3.right) //||
                   //(inv.position.x <= leftBoundary.position.x && _direction == Vector3.right) ||
                   //(inv.position.x >= rightBoundary.position.x && _direction == Vector3.left)
                   )
                {
                    if (!invaderTriggeredMoveDown) // Check if move down has not been triggered yet
                    {
                        moveDown = true; // Move down if the invader is within the descent range
                        invaderTriggeredMoveDown = true; // Set the flag to true
                    }
                }

                _direction *= -1; // Change direction
                break;
            }
        }
    }

    private Vector3 CalculateInitialRowPosition()
    {
        float width = 2.0f * (columns - 1);
        float height = 2.0f * (1);
        Vector2 centering = new Vector2(-width / 2, -height / 2);
        return new Vector3(centering.x, centering.y, 0.0f);
    }

    private void MoveDown()
    {
        // Calculate the distance each invader should move down
        float distanceToMove = rowHeight;

        // Move each invader down by the calculated distance
        foreach (Transform inv in inVader)
        {
            Vector3 pos = inv.position;
            pos.y -= distanceToMove;
            inv.position = pos;
        }
    }

    private void SpawnRow(Vector3 position)
    {
        for (int column = 0; column < columns; column++)
        {
            Vector3 spawnPosition = position + new Vector3(column * 2.0f, 0.0f, 0.0f);
            invader inv = Instantiate(prefabs[Random.Range(0, prefabs.Length)], spawnPosition, Quaternion.identity, transform);
            inVader.Add(inv.transform);
        }
    }
}
