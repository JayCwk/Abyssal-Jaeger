using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class invaders : MonoBehaviour
{
    public invader[] prefabs;
    public int columns = 5;
    public float speed = 0.5f;

    private Vector3 _direction = Vector2.right;
    private List<Transform> inVader = new List<Transform>();
    private float screenWidth;
    private float rowHeight = 2.0f;
    private Vector3 initialRowPosition;

    private void Start()
    {
        SpawnInitialRow();
        screenWidth = Screen.width * Camera.main.orthographicSize / Screen.height; // Calculate screen width based on screen size and orthographic size
    }

    private void Update()
    {
        MoveInvaders();
        CheckEdges();
    }

    private void SpawnInitialRow()
    {
        initialRowPosition = CalculateInitialRowPosition();
        SpawnRow(initialRowPosition);
    }

    private void MoveInvaders()
    {
        transform.position += _direction * speed * Time.deltaTime;
    }

    private void CheckEdges()
    {
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector2.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector2.right);

        if (_direction == Vector3.right && transform.position.x >= rightEdge.x)
        {
            MoveAndSpawnNewRow(Vector3.left);
        }
        else if (_direction == Vector3.left && transform.position.x <= leftEdge.x)
        {
            MoveAndSpawnNewRow(Vector3.right);
        }
    }

    private Vector3 CalculateInitialRowPosition()
    {
        float width = 2.0f * (columns - 1);
        float height = 2.0f * (1);
        Vector2 centering = new Vector2(-width / 2, -height / 2);
        return new Vector3(centering.x, centering.y, 0.0f);
    }

    private void MoveAndSpawnNewRow(Vector3 newDirection)
    {
        _direction = newDirection;
        MoveDown();
        Vector3 newPosition = inVader[inVader.Count - columns].position + new Vector3(0.0f, rowHeight, 0.0f);
        SpawnRow(newPosition);
    }

    private void MoveDown()
    {
        foreach (Transform inv in inVader)
        {
            Vector3 pos = inv.position;
            pos.y -= rowHeight;
            inv.position = pos;
        }
    }

    private void SpawnRow(Vector3 position)
    {
        for (int column = 0; column < columns; column++)
        {
            Vector3 spawnPosition = position + new Vector3(column * 2.0f, 0.0f, 0.0f);

            // Check if there's already an invader at the spawn position
            bool spawnPositionOccupied = false;
            foreach (Transform inv in inVader)
            {
                if (Vector3.Distance(inv.position, spawnPosition) < 0.01f)
                {
                    spawnPositionOccupied = true;
                    break;
                }
            }

            // If the spawn position is not occupied, spawn the invader
            if (!spawnPositionOccupied)
            {
                invader inv = Instantiate(prefabs[Random.Range(0, prefabs.Length)], spawnPosition, Quaternion.identity, transform);
                inVader.Add(inv.transform);
            }
        }
    }
}
