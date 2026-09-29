using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Timeline;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class Player : MonoBehaviour
{
    [Header("Default")]
    public Transform enemyTransform;
    public GameObject bombPrefab;
    public List<Transform> asteroidTransforms;

    // <<--------------------- Journal #2 --------------------->> //
    [Header ("Journal #2")]
    [Header("Task 1")]
    public float delay = 3f;
    private Coroutine spawnBombCoroutine;
    public float bombTrailSpacing;
    public int numOfTrailBombs;

    [Header("Task 2")]
    public float cornerBombSpacing;

    [Header("Task 3")]
    public float ratioValue;

    [Header("Task 4")]
    public float maxRange = 2.5f;

    // <<--------------------- Journal #3 --------------------->> //
    [Header ("Journal #3")]
    [Header("Task 1")]
    //a)
    public Vector3 direction;
    public Vector3 velocity;
    //b)
    [Header ("Acceleration")]
    public float acceleration;
    public float accelerationTime;
    public float maxSpeed;
    //c)
    [Header ("Decceleration")]
    public float decceleration;
    public float deccelerationTime;
    public float minSpeed;

    public float angle;
    public int currentIndex;
    public float _radius;
    public int _numberOfSides;
    public float radarSpeed;


    private void Start()
    {
        acceleration = maxSpeed / accelerationTime;
        decceleration = maxSpeed / deccelerationTime;
    }
    // UPDATE FUNCTION
    void Update()
    {
        // <<--------------------- Journal #2 --------------------->> //
        if (Keyboard.current.bKey.wasPressedThisFrame && spawnBombCoroutine == null)
        {
            spawnBombCoroutine = StartCoroutine(SpawnBombAtOffsetUpdate());
        }
        else if (Keyboard.current.tKey.wasPressedThisFrame)
        {
            SpawnBombTrail(bombTrailSpacing, numOfTrailBombs);
        }
        else if (Keyboard.current.gKey.wasPressedThisFrame)
        {
            WarpPlayer(enemyTransform, ratioValue);
        }
        else if (Keyboard.current.hKey.wasPressedThisFrame)
        {
            SpawnBombOnRandomCorner(cornerBombSpacing);
        }
        DetectAsteroids(maxRange, asteroidTransforms);

        // <<--------------------- Journal #3 --------------------->> //
        PlayerMovement();

        PlayerRadar();
        DrawRadar(_radius, _numberOfSides);
    }




    // <<--------------------- Journal #2 --------------------->> //
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




    // <<--------------------- Journal #3 --------------------->> //
    private void PlayerMovement()
    {
        
        direction = Vector3.zero;            
        
        if (Keyboard.current.upArrowKey.isPressed || Keyboard.current.wKey.isPressed)
        {
            direction += Vector3.up;
        }
        if (Keyboard.current.downArrowKey.isPressed || Keyboard.current.sKey.isPressed)
        {
            direction += Vector3.down;
        }
        if (Keyboard.current.leftArrowKey.isPressed || Keyboard.current.aKey.isPressed)
        {
            direction += Vector3.left;
        }
        if (Keyboard.current.rightArrowKey.isPressed || Keyboard.current.dKey.isPressed)
        {
            direction += Vector3.right;
        }

        if (!Keyboard.current.anyKey.isPressed)
        {
            velocity -= velocity.normalized * decceleration * Time.deltaTime;
        }
        else
        {
            velocity += direction * acceleration * Time.deltaTime;
        }

        velocity.x = Mathf.Clamp(velocity.x, -maxSpeed, maxSpeed);
        velocity.y = Mathf.Clamp(velocity.y, -maxSpeed, maxSpeed);

        transform.position += velocity * Time.deltaTime;
    }
    //Own

    private void PlayerRadar()
    {
        if (angle < 360)
        {
            angle += Time.deltaTime * radarSpeed;
            angle = angle % 360;
        }

/*        float yPos = Mathf.Sin(angleInRads);
        float xPos = Mathf.Cos(angleInRads);
        Vector3 offset = new Vector3(xPos * radius, yPos * radius, 0);*/
        float angleInRads = angle * Mathf.Deg2Rad;

        Vector3 pointOnCircle = new Vector3(Mathf.Sin(angleInRads), Mathf.Cos(angleInRads), 0) * _radius;

        float distanceToEnemy = Vector3.Distance(transform.position, enemyTransform.position);

        if (distanceToEnemy < _radius)
        {
            Debug.DrawLine(transform.position, transform.position + pointOnCircle, Color.red);
        }
        else
        {
            Debug.DrawLine(transform.position, transform.position + pointOnCircle, Color.green);
        }

    }

    //Prof

    private void DrawRadar(float radius, int numberOfSides)
    {
        float stepAngle = 360f / numberOfSides;
        List<Vector3> points = new();

        stepAngle *= Mathf.Deg2Rad;
        float currentAngle = stepAngle;

        for (int i = 0; i < numberOfSides; i++)
        {
            float xPos = Mathf.Cos(currentAngle) * radius;
            float yPos = Mathf.Sin(currentAngle) * radius;            

            Vector3 newPoint = new Vector3(xPos, yPos);
            points.Add(newPoint);

            currentAngle += stepAngle;
        }

        for (int i = 0; i < numberOfSides - 1; i++)
        {
            Vector3 startPoint = points[i];
            Vector3 endPoint = points[i + 1];

            float distanceToEnemy = Vector3.Distance(transform.position, enemyTransform.position);

            if (distanceToEnemy < radius)
            {
                Debug.DrawLine(transform.position + startPoint, transform.position + endPoint, Color.red);
            }
            else
            {
                Debug.DrawLine(transform.position + startPoint, transform.position + endPoint, Color.green);
            }

            if (i == numberOfSides - 2)
            {
                endPoint = points[i + 1];
                startPoint = points[0];

                if (distanceToEnemy < radius)
                {
                    Debug.DrawLine(transform.position + startPoint, transform.position + endPoint, Color.red);
                }
                else
                {
                    Debug.DrawLine(transform.position + startPoint, transform.position + endPoint, Color.green);
                }

            }
        }
    }  
}