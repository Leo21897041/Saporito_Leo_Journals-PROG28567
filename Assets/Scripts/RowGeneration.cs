using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class RowGeneration : MonoBehaviour
{
    public TMP_InputField inputField;

    public float spacing = 0.5f;
    public bool isGenerating = false;

    private void Update()
    {
        if (!isGenerating) { return; }

        bool isValidInput = int.TryParse(inputField.text, out int x);
        if (!isValidInput) { return; }
        
        int numberOfSquares = int.Parse(inputField.text);

        for (int i = 0; i < numberOfSquares; i++)
        {
            //Up && Down
            Debug.DrawLine(new Vector2(-0.5f + i * spacing, 0.5f), new Vector2(-0.5f + i * spacing, -0.5f), Color.white);
            Debug.DrawLine(new Vector2(0.5f + i * spacing, 0.5f), new Vector2(0.5f + i * spacing, -0.5f), Color.white);

            //Left && Right
            Debug.DrawLine(new Vector2(-0.5f + i * spacing, 0.5f), new Vector2(0.5f + i * spacing, 0.5f), Color.white);
            Debug.DrawLine(new Vector2(-0.5f + i * spacing, -0.5f), new Vector2(0.5f + i * spacing, -0.5f), Color.white);
        }
                
    }
    public void GenerateSquares()
    {
        isGenerating = !isGenerating;
    }
}
