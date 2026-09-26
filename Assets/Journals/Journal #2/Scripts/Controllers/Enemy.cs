using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    [Header("Task 2")]
    public Transform playerTransform;
    public Vector3 direction;
    public Vector3 velocity;
    public float maxSpeed;
    public float minDistance;
    [Header("Acceleration")]
    public float acceleration;
    public float accelerationTime;
    [Header("Decceleration")]
    public float decceleration;
    public float deccelerationTime;

    private void Start()
    {
        acceleration = maxSpeed / accelerationTime;
        decceleration = maxSpeed / deccelerationTime;
    }

    private void Update()
    {
        EnemyMovement();
    }

    private void EnemyMovement()
    {
        float distance = Vector3.Distance(transform.position, playerTransform.position);

        if (distance < minDistance)
        {
            direction = (playerTransform.position - transform.position).normalized;

            velocity += direction * acceleration * Time.deltaTime;

            transform.position += Time.deltaTime * velocity;
        }
        else
        {
            velocity -= velocity.normalized * decceleration * Time.deltaTime;
            transform.position += Time.deltaTime * velocity;
        }
    }
}
