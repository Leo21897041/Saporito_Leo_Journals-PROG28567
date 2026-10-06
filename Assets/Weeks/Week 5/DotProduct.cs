using UnityEngine;
using UnityEngine.InputSystem;

public class DotProduct : MonoBehaviour
{
    public float redAngle;
    public float blueAngle;
    public float radius;
    public bool printMine;
    
    void Update()
    {
        // <<----------------------- MINE ----------------------->> //
        if (printMine)
        {
            Debug.DrawLine(transform.position, transform.position + NewVector(redAngle).normalized, Color.red);
            Debug.DrawLine(transform.position, transform.position + NewVector(blueAngle).normalized, Color.blue);

            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                CalculateDotProduct();
            }
        }

        // <<----------------------- PROF ----------------------->> //
        else
        {
            Debug.DrawLine(transform.position, transform.position + ComputeVectorFromAngle(redAngle), Color.red);
            Debug.DrawLine(transform.position, transform.position + ComputeVectorFromAngle(blueAngle), Color.blue);

            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                float dot = ComputeDotProduct(ComputeVectorFromAngle(redAngle), ComputeVectorFromAngle(blueAngle));
                print(dot);
            }
        }
    }
    


    // <<----------------------- MINE ----------------------->> //
    private Vector3 NewVector(float _angle)
    {
        Vector3 _vector = new Vector3((Mathf.Cos(_angle * Mathf.Deg2Rad)) * radius, (Mathf.Sin(_angle * Mathf.Deg2Rad)) * radius, 0);

        return _vector;
    }
    private void CalculateDotProduct()
    {
        float _dotProduct = Vector3.Dot(NewVector(redAngle).normalized, NewVector(blueAngle).normalized);

        print(_dotProduct);
    }


    
    // <<----------------------- PROF ----------------------->> //
    private Vector3 ComputeVectorFromAngle(float _angle)
    {
        float angleInRads = Mathf.Deg2Rad * _angle;

        float xCoord = Mathf.Cos(angleInRads);
        float yCoord = Mathf.Sin(angleInRads);

        return new Vector3(xCoord, yCoord);
    }
    private float ComputeDotProduct(Vector3 a, Vector3 b)
    {
        float _dot = a.x * b.x + a.y * b.y + a.z * a.z;
        return _dot;
    }
}
