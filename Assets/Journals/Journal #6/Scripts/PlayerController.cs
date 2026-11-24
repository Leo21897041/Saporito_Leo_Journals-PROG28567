using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody2D rb;
    public BoxCollider2D bc;

    [Header("Input")]
    public Vector2 directionalInput;

    [Header("Speed")]
    public float moveSpeed;
/*    public float maxSpeed;
    public Vector2 velocity;

    [Header("Acceleration")]
    public float acceleration;
    public float accelerationTime;

    [Header("Deceleration")]
    public float deceleration;
    public float decelerationTime;*/

    [Header("Player State")]
    public LayerMask layer;
    public float lookDirection;
    public float belowPlayer;
    private bool isMoving;
    private bool isGrounded;
    public enum FacingDirection
    {
        left, right
    }

    [Header("Jumping")]
    public float apexHeight;
    public float apexTime;
    public float jumpVelocity;
    public float gravity;
    public bool doubleJump;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();

        gravity = -2 * apexHeight / (apexTime * Mathf.Exp(2));
        jumpVelocity = 2 * apexHeight / apexTime;
    }

    void FixedUpdate()
    {

        Vector2 playerInput = new Vector2(directionalInput.x, 0f);

        // The input from the player needs to be determined and
        // then passed in the to the MovementUpdate which should
        // manage the actual movement of the character.

        MovementUpdate(playerInput);
    }

    private void MovementUpdate(Vector2 playerInput)
    {
/*        if (playerInput.x == 0f)
        {
            velocity.x = 0f;
        }
        else
        {
            velocity.x += acceleration * Time.fixedDeltaTime * playerInput.x;
        }

        velocity.x = Mathf.Clamp(velocity.x, -maxSpeed, maxSpeed);

        rb.linearVelocity = new Vector2(velocity.x, rb.linearVelocity.y);*/
        
        rb.linearVelocity = new Vector2(playerInput.x * moveSpeed, rb.linearVelocity.y);
    }

    public bool IsWalking()
    {
        isMoving = directionalInput.x != 0f;
        return isMoving;
    }

    public FacingDirection GetFacingDirection()
    {
        if (directionalInput.x != 0f)
        {
            lookDirection = Mathf.Sign(directionalInput.x);
        }

        if (lookDirection > 0f) 
        {
            return FacingDirection.right;
        }

        return FacingDirection.left;
    }

    public bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, bc.bounds.extents.y + belowPlayer, layer);

        /*        print(hit.collider);
        print(bc.bounds.extents.y + belowPlayer);

        Debug.DrawRay(transform.position, Vector2.down * (bc.bounds.extents.y + belowPlayer), Color.magenta);*/
        
        isGrounded = hit.collider != null;

        return isGrounded;
    }
/*    public bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position - new Vector3(0f, bc.bounds.size.y, 0f), Vector2.down);
        
        if (hit.collider != null)
        {
            print(hit.collider);
            print(hit.distance);
            Debug.DrawRay(transform.position - new Vector3(0f, bc.bounds.size.y, 0f), Vector2.down * 1.5f, Color.magenta);
            
            isGrounded = hit.distance <= 0f;        
        }

        return isGrounded;
    }*/
    public void OnMove(InputAction.CallbackContext context)
    {
        directionalInput = context.ReadValue<Vector2>();
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded || context.performed && doubleJump)
        {
            JumpMechanic();
        }
    }
    public void JumpMechanic()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y - jumpVelocity * gravity);
    }
}
