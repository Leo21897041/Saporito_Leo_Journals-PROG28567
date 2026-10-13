using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAgain : MonoBehaviour
{
    public float accelerationTime = 1f;
    public float deccelerationTime = 1f;
    public float maxSpeed = 1f;
    [Tooltip("Measured in degrees per second")]
    public float angularSpeed;

    public float acceleration;
    public float decceleration;
    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        acceleration = maxSpeed / accelerationTime;
        decceleration = maxSpeed / deccelerationTime;
    }

    private void Update()
    {
        PlayerMovement(); 
        PlayerRotation();
    }

    private void PlayerMovement()
    {
        Vector3 playerInput = Vector3.zero;
        
        if (Keyboard.current.upArrowKey.isPressed || Keyboard.current.wKey.isPressed)
        {
            playerInput += Vector3.up;
        }
        if (Keyboard.current.downArrowKey.isPressed || Keyboard.current.sKey.isPressed)
        {
            playerInput += Vector3.left;
        }
        if (Keyboard.current.leftArrowKey.isPressed || Keyboard.current.aKey.isPressed)
        {
            playerInput += Vector3.left;
        }
        if (Keyboard.current.rightArrowKey.isPressed || Keyboard.current.dKey.isPressed)
        {
            playerInput += Vector3.right;
        }

        if (playerInput.sqrMagnitude > 0) // some input detected
        {
            velocity += acceleration * Time.deltaTime * playerInput.normalized; // accelerate relative to the input

            if (velocity.sqrMagnitude > maxSpeed * maxSpeed) // going too fast
            {
                velocity = Vector3.ClampMagnitude(velocity, maxSpeed); // enforce the speed limit
            }
            else // no imput detected
            {
                float deltaVelocity = decceleration * Time.deltaTime;

                if (velocity.magnitude > deltaVelocity) // if we need to slow down
                {
                    velocity -= decceleration * velocity.normalized; // deccelerate
                }
                else
                {
                    velocity = Vector3.zero; // complete stop
                }
            }
        }

        transform.position += Time.deltaTime * velocity;
    }
    private void PlayerRotation()
    {
        if (velocity.sqrMagnitude == 0) { return; } // not moving, do nothing

        float deltaAngle = ComputeShortestAngle(transform.up, velocity);

        if (deltaAngle == 0) { return; } // locked on, do nothing

        float rotationDirection = Mathf.Sign(deltaAngle);
        float rotationStep = angularSpeed * Time.deltaTime;

        if (rotationStep < Mathf.Abs(deltaAngle)) // if we still need to rotate
        {
            transform.Rotate(0, 0, rotationDirection * rotationStep); // rotate relative to angular speed
        }
        else
        {
            transform.Rotate(0, 0, deltaAngle); // lock on
        }
    }
    private float ComputeShortestAngle(Vector3 a, Vector3 b)
    {
        float angleA = Mathf.Atan2(a.y, a.x) * Mathf.Rad2Deg;
        float angleB = Mathf.Atan2(b.y, b.x) * Mathf.Rad2Deg;

        return Mathf.DeltaAngle(angleA, angleB);
    }
}
