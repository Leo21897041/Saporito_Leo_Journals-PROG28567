using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class Player : MonoBehaviour
{
    [Header("Default")]
    public Transform enemyTransform;
    public GameObject bombPrefab;
    public List<Transform> asteroidTransforms;

    [Header("Task 1")]
    //a)
    public float delay = 3f;
    private Coroutine spawnBombCoroutine;
    //b)
    public float bombTrailSpacing;
    public int numOfTrailBombs;

    [Header("Task 2")]
    public float cornerBombSpacing;

    [Header("Task 3")]
    public float ratioValue;

    [Header("Task 4")]
    public float maxRange = 2.5f;

    void Update()
    {
        if (Keyboard.current.bKey.wasPressedThisFrame && spawnBombCoroutine == null)
        {
            spawnBombCoroutine = StartCoroutine(SpawnBombAtOffsetUpdate());
        }
        else if (Keyboard.current.tKey.wasPressedThisFrame)
        {
            SpawnBombTrail(bombTrailSpacing, numOfTrailBombs);
        }
        else if (Keyboard.current.aKey.wasPressedThisFrame)
        {
            WarpPlayer(enemyTransform, ratioValue);
        }
        else if (Keyboard.current.dKey.wasPressedThisFrame)
        {
            SpawnBombOnRandomCorner(cornerBombSpacing);
        }

        DetectAsteroids(maxRange, asteroidTransforms);
    }
    private void SpawnBombAtOffset(Vector3 inOffset)
    {
        Instantiate(bombPrefab, transform.position + inOffset, Quaternion.identity);
    }
    private IEnumerator SpawnBombAtOffsetUpdate()
    {
        yield return new WaitForSeconds(delay);

        SpawnBombAtOffset(Vector3.up);
        spawnBombCoroutine = null;

        yield return null;
    }
    private void SpawnBombTrail(float inBombSpacing, int inNumberOfBombs)
    {
        for (int i = 0; i < inNumberOfBombs; i++)
        {
            SpawnBombAtOffset(new Vector3(0, -1 - i * inBombSpacing, 1));
        }
    }
    private void SpawnBombOnRandomCorner(float inDistance)
    {
        int randomPosition = Random.Range(0, 4);

        switch (randomPosition)
        {
            case 0: SpawnBombAtOffset(new Vector3(-1, 1, 0) * inDistance); break; //Top Left
            case 1: SpawnBombAtOffset(new Vector3(1, 1, 0) * inDistance); break; //Top Right
            case 2: SpawnBombAtOffset(new Vector3(-1, -1, 0) * inDistance); break; //Bottom Left
            case 3: SpawnBombAtOffset(new Vector3(1, -1, 0) * inDistance); break; //Bottom Right
        }
    }
    private void WarpPlayer(Transform target, float ratio)
    {
        transform.position = Vector2.Lerp(transform.position, target.position, ratio);
    }
    private void DetectAsteroids(float inMaxRange, List<Transform> inAestroids)
    {
        foreach (Transform asteroid in asteroidTransforms)
        {
            float distance = Vector2.Distance(transform.position, asteroid.position);

            if (distance < inMaxRange)
            {
                Vector2 direction = (asteroid.position - transform.position).normalized;                

                Debug.DrawLine(transform.position, (Vector2)transform.position + direction * inMaxRange);
            }
        }
    }
}
