using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public float turnSpeed;
    public float expForce;
    private bool applyExpForce;
    public float relativeForce;
    private bool applyRelativeForce;
    public Transform expPosition;
    private Rigidbody2D rb;
    private Vector2 directionalInput;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        Constraints();
        Type();        
        ForceAtPosition();
        ApplyRelativeForce();
    }
    void FixedUpdate()
    {
/*        rb.linearVelocity = new Vector2(
            moveSpeed * Time.fixedDeltaTime * directionalInput.x,
            rb.linearVelocity.y);*/

        transform.Rotate(new Vector3(0, 0, directionalInput.x * -turnSpeed * Time.fixedDeltaTime));
        
        if (applyExpForce)
        {
            rb.AddForceAtPosition(Vector2.one * expForce, expPosition.position, ForceMode2D.Impulse);            
            applyExpForce = false;
        }
        if (applyRelativeForce)
        {
            Vector2 forceDirection = transform.up;

            rb.AddForce(forceDirection * relativeForce, ForceMode2D.Force);
        }
    }
    private void Constraints()
    {
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        }
        else if (Keyboard.current.oKey.wasPressedThisFrame)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        }
        else if (Keyboard.current.iKey.wasPressedThisFrame)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        else if (Keyboard.current.uKey.wasPressedThisFrame)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else if (Keyboard.current.yKey.wasPressedThisFrame)
        {
            rb.constraints = RigidbodyConstraints2D.None;
        }
    }
    private void Type()
    {
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
        if (Keyboard.current.kKey.wasPressedThisFrame)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
        if (Keyboard.current.jKey.wasPressedThisFrame)
        {
            rb.bodyType = RigidbodyType2D.Static;
        }
    }
    private void ForceAtPosition()
    {
        if (Keyboard.current.mKey.wasPressedThisFrame)
        {
            applyExpForce = true;
        }
    }
    private void ApplyRelativeForce()
    {
        if (Keyboard.current.nKey.isPressed)
        {
            applyRelativeForce = true;
        }
        else
        {
            applyRelativeForce = false;
        }

        /*if (Keyboard.current.zKey.isPressed)
        {
            transform.Rotate(new Vector3(0, 0, -turnSpeed * Time.fixedDeltaTime));
        }
        if (Keyboard.current.cKey.isPressed)
        {
            transform.Rotate(new Vector3(0, 0, turnSpeed * Time.fixedDeltaTime));
        }*/
    }
    
    public void OnMove(InputAction.CallbackContext context)
    {
        directionalInput = context.ReadValue<Vector2>();
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}
