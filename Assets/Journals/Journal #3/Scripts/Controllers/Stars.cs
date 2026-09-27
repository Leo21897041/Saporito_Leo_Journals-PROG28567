using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Stars : MonoBehaviour
{
    public List<Transform> starTransforms;
    public float drawingTime;
    public Vector3 currentPosition;
    public Vector3 startPosition;
    public Vector3 endPosition;
    public int index;
    public float progress;

    void Update()
    {
        DrawConstellation();
    }

    private void DrawConstellation()
    {
        if (progress < drawingTime)
        {
            progress += Time.deltaTime;
        }
        else if(progress >= drawingTime)
        {
            progress = 0f;

            startPosition = starTransforms[index + 1].position;
            currentPosition = startPosition;
        }

        foreach (Transform star in starTransforms)
        {
            startPosition = star.position;
            endPosition = starTransforms[index + 1].position;

            if (startPosition == starTransforms[index].position)
            {
                currentPosition = Vector3.Lerp(startPosition, endPosition, progress / drawingTime);
                Debug.DrawLine(startPosition, currentPosition);
                
                if (currentPosition == endPosition)
                {
                    Debug.DrawLine(startPosition, endPosition, Color.white, 5f - index);
                }
            }
        }

        if (currentPosition == endPosition)
        {
            index++;
        }

        if (currentPosition == starTransforms[starTransforms.Count - 1].position)
        {
            index = 0;
        }
    }
}
