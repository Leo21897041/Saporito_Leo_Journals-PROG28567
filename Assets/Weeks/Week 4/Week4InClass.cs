using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
public class Week4InClass : MonoBehaviour
{
    public List<float> angles = new List<float>();
    public int currentIndex;
    public float radius;
    public Vector3 startPoint = Vector3.zero;
    public float progress = 0;
    public float duration = 1;

    private void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            float newAngle = Random.Range(0f, 360f);
            angles.Add(newAngle);
        }
    }
    private void Update()
    {
        progress += Time.deltaTime;

        if (progress > duration)
        {
            currentIndex = (currentIndex + 1) % angles.Count;
            
            progress = 0f;
        }

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            currentIndex = (currentIndex + 1) % angles.Count;
        }

        float angle = angles[currentIndex];
        float angleInRads = angle * Mathf.Deg2Rad;

        float xPos = Mathf.Cos(angleInRads);
        float yPos = Mathf.Sin(angleInRads);

        Vector3 offset = new Vector3(xPos, yPos, 0);

        Debug.DrawLine(startPoint, startPoint + offset);
    }
}
