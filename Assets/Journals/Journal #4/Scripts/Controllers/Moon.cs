using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Moon : MonoBehaviour
{
    public Transform planetTransform;
    public float angle;
    public float _speed;
    public float _radius;
    public Vector3 pointOnCircle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OrbitalMotion(_radius, _speed, planetTransform);
    }
    private void OrbitalMotion(float radius, float speed, Transform target)
    {
        angle += Time.deltaTime * speed;

        pointOnCircle = new Vector3((Mathf.Cos(angle) * radius) * Mathf.Deg2Rad, (Mathf.Sin(angle) * radius) * Mathf.Deg2Rad, 0f);

        transform.position = target.position + pointOnCircle;
    }
}
