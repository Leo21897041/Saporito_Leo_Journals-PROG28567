using UnityEngine;
using UnityEngine.InputSystem;

public class Teleporter : MonoBehaviour
{
    public Vector2 mousePosition;
    public float _percentDistance;

    void Update()
    {
        
        if(Mouse.current.leftButton.wasPressedThisFrame)
        {            
            mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            TeleportTowardsTarget(mousePosition, _percentDistance / 100);            
        }
    }
    private void TeleportTowardsTarget(Vector2 target, float percentDistance)
    {
        transform.position = Vector2.Lerp((Vector2)transform.position, target, percentDistance);
    }
}
