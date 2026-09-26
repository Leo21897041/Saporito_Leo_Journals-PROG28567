using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float moveSpeed;
    public float arrivalDistance;
    public float maxFloatDistance;
    public Vector3 randomPosition;
    public float moveProgress;
    public float moveDuration;

    void Start()
    {
        randomPosition = new Vector3(Random.Range(-maxFloatDistance, maxFloatDistance), Random.Range(-maxFloatDistance, maxFloatDistance), 0);        
    }

    void Update()
    {
        AsteroidMovement();
    }

    public void AsteroidMovement()
    {
        Vector3 direction = (randomPosition - transform.position).normalized;

        transform.position += moveSpeed * direction * Time.deltaTime;

        float distance = Vector3.Distance(transform.position, randomPosition);

        if (distance <= arrivalDistance)
        {
            randomPosition = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height), 0));
        }
    }
}
