using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invaders : MonoBehaviour
{
    public Invader[] prefabs;
    public int columns = 4;
    public float speed = 0.5f;
    public Transform bottomBoundary;
    public Transform leftBoundary;
    public Transform rightBoundary;

    private Vector3 _direction = Vector3.right;
    private List<Invader> invaders = new List<Invader>();
    private float rowHeight = 2.0f;
    private bool moveDown = false;

    private void Start()
    {
        SpawnInitialRow();
    }

    private void FixedUpdate()
    {
        MoveInvaders();
    }

    private void SpawnInitialRow()
    {
        if (prefabs != null && prefabs.Length > 0)
        {
            Vector3 initialRowPosition = CalculateInitialRowPosition();
            SpawnRow(initialRowPosition);
        }
        else
        {
            Debug.LogError("No invader prefabs assigned!");
        }
    }

    private void MoveInvaders()
    {
        // Calculate the distance to the left and right edges of the screen
        float leftEdge = leftBoundary.position.x;
        float rightEdge = rightBoundary.position.x;

        // Calculate the position of the leftmost and rightmost invaders
        float leftmostInvaderX = float.MaxValue;
        float rightmostInvaderX = float.MinValue;
        foreach (Invader inv in invaders)
        {
            float invaderX = inv.transform.position.x;
            if (invaderX < leftmostInvaderX)
            {
                leftmostInvaderX = invaderX;
            }
            if (invaderX > rightmostInvaderX)
            {
                rightmostInvaderX = invaderX;
            }
        }

        // Check if invaders are about to move out of the screen
        if (leftmostInvaderX <= leftEdge && _direction == Vector3.left)
        {
            _direction = Vector3.right; // Change direction to right
            moveDown = true; // Trigger move down
        }
        else if (rightmostInvaderX >= rightEdge && _direction == Vector3.right)
        {
            _direction = Vector3.left; // Change direction to left
            moveDown = true; // Trigger move down
        }

        // Move invaders horizontally
        transform.position += _direction * speed * Time.fixedDeltaTime;

        // Move invaders down if necessary
        if (moveDown)
        {
            MoveDown();
            moveDown = false; // Reset moveDown after moving down
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
        // Move all invaders down by the row height
        foreach (Invader inv in invaders)
        {
            Vector3 pos = inv.transform.position;
            pos.y -= rowHeight;
            inv.transform.position = pos;

            // Disable invader if it's below the bottom boundary
            if (pos.y < bottomBoundary.position.y)
            {
                inv.gameObject.SetActive(false);
            }
        }

        // Remove disabled invaders from the list
        invaders.RemoveAll(inv => !inv.gameObject.activeSelf);

        // Spawn a new row of invaders
        Vector3 newPosition = invaders[invaders.Count - columns].transform.position + new Vector3(0.0f, rowHeight, 0.0f);
        SpawnRow(newPosition);
    }

    private void SpawnRow(Vector3 position)
    {
        for (int column = 0; column < columns; column++)
        {
            Vector3 spawnPosition = position + new Vector3(column * 2.0f, 0.0f, 0.0f);
            Invader inv;
            if (prefabs != null && prefabs.Length > 0)
            {
                inv = Instantiate(prefabs[Random.Range(0, prefabs.Length)], spawnPosition, Quaternion.identity, transform);
                invaders.Add(inv);
            }
            else
            {
                Debug.LogError("No invader prefabs assigned!");
            }
        }
    }
}
