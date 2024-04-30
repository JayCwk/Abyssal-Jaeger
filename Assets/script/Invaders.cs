using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invaders : MonoBehaviour
{
    public Invader[] prefabs;
    public Transform[] spawnPoints;
    public Transform bottomBoundary;
    public Transform topBoundary;
    public float initialSpawnDelay = 2f;
    public float initialMoveSpeed = 1f;
    public float spawnInterval = 1f;
    public float moveSpeedIncreaseInterval = 30f;
    public float speedIncreaseAmount = 0.1f;

    private List<Invader> invaders = new List<Invader>();
    private bool[] spawnCompleted;
    private float currentSpawnDelay;
    private float currentMoveSpeed;
    private float lastSpeedIncreaseTime;

    private void Start()
    {
        currentSpawnDelay = initialSpawnDelay;
        currentMoveSpeed = initialMoveSpeed;
        spawnCompleted = new bool[spawnPoints.Length];

        // Start spawning invaders
        StartCoroutine(SpawnInvaders());
        // Start increasing spawn speed and movement speed
        StartCoroutine(IncreaseSpeedOverTime());
    }

    private IEnumerator SpawnInvaders()
    {
        yield return new WaitForSeconds(currentSpawnDelay);

        while (true)
        {
            for (int i = 0; i < spawnPoints.Length; i++)
            {
                if (!spawnCompleted[i])
                {
                    SpawnInvader(spawnPoints[i]);
                    yield return new WaitForSeconds(spawnInterval);
                }
            }
            yield return null;
        }
    }

    private void SpawnInvader(Transform spawnPoint)
    {
        Invader invader = Instantiate(prefabs[Random.Range(0, prefabs.Length)], spawnPoint.position, Quaternion.identity);
        invaders.Add(invader);

        invader.SetMoveSpeed(currentMoveSpeed);
        invader.SetBoundaries(bottomBoundary, topBoundary);

        
    }

    private void OnInvaderDie(Invader invader)
    {
        invaders.Remove(invader);
    }

    private IEnumerator IncreaseSpeedOverTime()
    {
        yield return new WaitForSeconds(moveSpeedIncreaseInterval);

        while (true)
        {
            currentMoveSpeed += speedIncreaseAmount;

            yield return new WaitForSeconds(moveSpeedIncreaseInterval);
        }
    }
}