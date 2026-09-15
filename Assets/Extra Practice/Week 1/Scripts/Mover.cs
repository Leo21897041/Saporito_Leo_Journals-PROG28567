using UnityEngine;
using UnityEngine.InputSystem;

public class Mover : MonoBehaviour
{
    private Vector2 mousePosition;
    public float _percentDistance;
    public float _velocity;
    private void Update()
    {
        if(Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        }

        MoveTowardsTarget(mousePosition, _percentDistance / 100, _velocity);
    }

    private void MoveTowardsTarget(Vector3 target, float percentDistance, float velocity)
    {        
        transform.position = Vector2.Lerp(transform.position, target, percentDistance);
    }
}
