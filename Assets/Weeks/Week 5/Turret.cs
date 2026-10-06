using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Turret : MonoBehaviour
{
    [Tooltip("Measured in Degrees per second.")]
    public float angularSpeed;
    public Transform target;
    public bool printMine;
    void Update()
    {
        // <<----------------------- MINE ----------------------->> //
        if (printMine)
        {
            Debug.DrawLine(transform.position, transform.position + transform.up, Color.magenta);

            transform.Rotate(0, 0, angularSpeed * Time.deltaTime);

            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                if (ComputeDotProduct() > 0)
                {
                    print("In Front: " + ComputeDotProduct());
                }
                else
                {
                    print("Behind: " + ComputeDotProduct());
                }
            }
        }

        // <<----------------------- PROF ----------------------->> //
        else
        {
            Vector3 directionToTarget = (target.position - transform.position).normalized;

/*
            #region
            transform.Rotate(0, 0, angularSpeed * Time.deltaTime);

            Debug.DrawLine(transform.position, transform.position + transform.up, Color.magenta);

            float dot = Vector3.Dot(transform.up, directionToTarget);

            if (dot >= 0)
            {
                print("In Front");
            }
            else
            {
                print("Behind");
            }
            #endregion
*/

            #region
            float upAngle = Mathf.Atan2(transform.up.y, transform.up.x) * Mathf.Rad2Deg;
            float directionAngle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
            float deltaAngle = Mathf.DeltaAngle(upAngle, directionAngle);

            //Debug.Log(deltaAngle);

            float dot = Vector3.Dot(transform.up, directionToTarget);

            if (dot < 0.98f)
            {
                switch (Mathf.Sign(deltaAngle))
                {
                    case 1:
                        transform.Rotate(0, 0, angularSpeed * Time.deltaTime);
                        break;
                    case -1:
                        transform.Rotate(0, 0, -angularSpeed * Time.deltaTime);
                        break;
                }
            }
            #endregion
        }
    }
    


    // <<----------------------- MINE ----------------------->> //
    private float ComputeDotProduct()
    {
        float dotProduct = Vector3.Dot(transform.position.normalized + transform.up.normalized, target.position.normalized);

        return dotProduct;
    }
}
