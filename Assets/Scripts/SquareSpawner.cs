using UnityEngine;
using UnityEngine.InputSystem;

public class SquareSpawner : MonoBehaviour
{
    public Vector2 mousePosition;
    public Vector2 worldMousePosition;

    public GameObject transparentSquare;
    private void Update()
    {
        mousePosition = Mouse.current.position.ReadValue();
        worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // a)
        if (Mouse.current.leftButton.isPressed)
        {
            //Up && Down
            Debug.DrawLine(worldMousePosition - new Vector2(0.5f, 0.5f), worldMousePosition + new Vector2(-0.5f, 0.5f), Color.white);
            Debug.DrawLine(worldMousePosition - new Vector2(-0.5f, 0.5f), worldMousePosition + new Vector2(0.5f, 0.5f), Color.white);

            //Left && Right
            Debug.DrawLine(worldMousePosition - new Vector2(0.5f, -0.5f), worldMousePosition + new Vector2(0.5f, 0.5f), Color.white);
            Debug.DrawLine(worldMousePosition - new Vector2(0.5f, 0.5f), worldMousePosition + new Vector2(0.5f, -0.5f), Color.white);
        }

        // b)
        transparentSquare.transform.position = worldMousePosition;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            GameObject squareObj = Instantiate(transparentSquare, worldMousePosition, Quaternion.identity);

            squareObj.GetComponent<SpriteRenderer>().color = Color.white;
        }

        //c)
        Vector2 scale = transparentSquare.transform.localScale;
        scale += Mouse.current.scroll.ReadValue() * new Vector2(0.5f, 0.5f);
        transparentSquare.transform.localScale = new Vector2(scale.y, scale.y);
    }
}
