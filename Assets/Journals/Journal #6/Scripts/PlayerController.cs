using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public BoxCollider2D bc;

    public Vector2 directionalInput;
    public float moveSpeed;
    public bool isMoving;
    public bool isGrounded;
    public enum FacingDirection
    {
        left, right
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
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
        rb.AddForce(playerInput * moveSpeed, ForceMode2D.Force);
    }

    public bool IsWalking()
    {
        isMoving = directionalInput.x != 0f;
        return isMoving;
    }
    public bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.5f);

        if (hit.collider != null)
        {
            print(hit.collider);
            print(hit.distance);
            Debug.DrawRay(transform.position, Vector2.down * 1.5f, Color.magenta);
        }
        
        isGrounded = hit.distance <= hit.distance;        
        
        return isGrounded;
    }
    public FacingDirection GetFacingDirection()
    {
        if (directionalInput.x > 0f) 
        {
            return FacingDirection.right;
        }

        return FacingDirection.left;
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        directionalInput = context.ReadValue<Vector2>();
    }
}
