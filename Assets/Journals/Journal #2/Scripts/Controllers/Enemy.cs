using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    [Header("Task 2")]
    public float minDistance;
    public Transform playerTransform;
    public Vector3 direction;
    public float acceleration;
    public Vector3 velocity;
    public float accelerationTime;
    public float maxSpeed;

    private void Start()
    {
        acceleration = maxSpeed / accelerationTime;
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

            transform.position = Vector3.Lerp(transform.position, playerTransform.position, distance * Time.deltaTime / distance);
        }
        else
        {
            velocity = Vector3.zero;
        }
    }
}
