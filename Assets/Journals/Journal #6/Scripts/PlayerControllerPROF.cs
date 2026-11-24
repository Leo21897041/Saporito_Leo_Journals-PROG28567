using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerController;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerControllerPROF : MonoBehaviour
{
    //Debug.DrawRay(transform.position, Vector3.down * 1f, Color.red);
    public enum FacingDirection
    {
        left, right
    }    
    [Header("References")]
    public Rigidbody2D body2D;

    [Header("Horizontal")]
    public float maxSpeed;
    public float accelerationTime;
    public float decelerationTime;

    [Header("Vertical")]
    public float apexHeight = 3f;
    public float apexTime = 0.5f;

    [Header("Ground Check")]
    public float groundCheckOffset;
    public Vector2 groundCheckSize;
    public LayerMask groundCheckMask;

    private float acceleration;
    private float deceleration;
    private Vector2 velocity;

    private float gravity;
    private float jumpVelocity;
    private float terminalVelocity;

    private FacingDirection facingDirection = FacingDirection.left;
    private bool isGrounded;
    private bool isJumping;

    private Keyboard keyboard;

    void Start()
    {
        body2D.gravityScale = 0f;

        acceleration = maxSpeed / accelerationTime;
        deceleration = maxSpeed / decelerationTime;

        gravity = -2 * apexHeight / (Mathf.Pow(apexTime,2));
        jumpVelocity = 2 * apexHeight / apexTime;
        terminalVelocity = -2 * jumpVelocity;

        keyboard = Keyboard.current;
    }

    void Update()
    {
        CheckForGround();

        Vector2 playerInput = new ();

        if (keyboard.aKey.isPressed)
        {
            playerInput.x += -1;
            facingDirection = FacingDirection.left;
        }
        if (keyboard.dKey.isPressed)
        {
            playerInput.x += 1;
            facingDirection = FacingDirection.right;
        }

        if (keyboard.spaceKey.isPressed && isGrounded)
        {
            isJumping = true;
        }

        MovementUpdate(playerInput);
    }

    private void MovementUpdate(Vector2 playerInput)
    {
        if (playerInput.x != 0)
        {
            velocity.x += acceleration * playerInput.x * Time.deltaTime;
            velocity.x = Mathf.Clamp(velocity.x, -maxSpeed, maxSpeed);
        }
        else
        {
            velocity.x = Mathf.MoveTowards(velocity.x, 0f, deceleration * Time.deltaTime);
        }
    }
    private void FixedUpdate()
    {
        if (!isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
            velocity.y = Mathf.Clamp(velocity.y, terminalVelocity, velocity.y);
        }
        else 
        {
            velocity.y = 0f;
        }

        if (isJumping)
        {
            isJumping = false;
            velocity.y = jumpVelocity;
        }

        body2D.linearVelocity = velocity;
    }
    private void CheckForGround()
    {
        isGrounded = Physics2D.OverlapBox(
            transform.position + Vector3.down * groundCheckOffset,
            groundCheckSize,
            0,
            groundCheckMask);
    }
    public bool IsWalking()
    {
        return velocity.x != 0f;
    }
    public bool IsGrounded()
    {
        return isGrounded;
    }
    public FacingDirection GetFacingDirection()
    {
        return facingDirection;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position + Vector3.down * groundCheckOffset, groundCheckSize);
    }
}
