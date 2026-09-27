using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars : MonoBehaviour
{
    public List<Transform> starTransforms;
    public float drawingTime;
    private Vector3 currentPosition;
    private Vector3 startPosition;
    private Vector3 endPosition;

    void Update()
    {
        DrawConstellation();
    }

    private void DrawConstellation()
    {
        
    }
}
