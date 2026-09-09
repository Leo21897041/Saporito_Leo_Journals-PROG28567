using UnityEngine;
using UnityEngine.InputSystem;

public class Pipeline : MonoBehaviour
{
    public Vector2 oldMousePosition;
    public Vector2 newMousePosition;
    public float progress;
    public float duration;

    public float totalMagnatude;

    private void Update()
    {
        if (Mouse.current.leftButton.isPressed)
        {
            progress += Time.deltaTime;

            if (progress > duration)
            {
                newMousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                Debug.DrawLine(oldMousePosition, newMousePosition, Color.white);

                //Total Magnatude
                //a^2 + b^2 = c^2

                float a = newMousePosition.x - oldMousePosition.x;
                float b = newMousePosition.y - oldMousePosition.y;

                float c = Mathf.Sqrt((a * a) + (b * b));

                totalMagnatude += c;

                oldMousePosition = newMousePosition;

                progress = 0f;
            }
        }

        if(Mouse.current.leftButton.wasReleasedThisFrame)
        {
            print(totalMagnatude);

            totalMagnatude = 0f;
        }
    }
}
