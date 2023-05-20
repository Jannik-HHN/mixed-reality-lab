using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float torqueForce;
    public float initialAngle;
    private float currentGravity;


    // Start is called before the first frame update
    void Start()
    {
        currentGravity = Physics.gravity.magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Throw(Transform target)
    {

        var rigid = GetComponent<Rigidbody>();

        Vector3 p = target.position;

        // Selected angle in radians
        float angle = initialAngle * Mathf.Deg2Rad;

        // Positions of this object and the target on the same plane
        Vector3 planarTarget = new Vector3(p.x, 0, p.z);
        Vector3 planarPostion = new Vector3(transform.position.x, 0, transform.position.z);

        // Planar distance between objects
        float distance = Vector3.Distance(planarTarget, planarPostion);
        // Distance along the y axis between objects
        float yOffset = transform.position.y - p.y;

        float initialVelocity = (1 / Mathf.Cos(angle)) * Mathf.Sqrt((0.5f * currentGravity * Mathf.Pow(distance, 2)) / (distance * Mathf.Tan(angle) + yOffset));

        Vector3 velocity = new Vector3(0, initialVelocity * Mathf.Sin(angle), initialVelocity * Mathf.Cos(angle));

        // Rotate our velocity to match the direction between the two objects
        float angleBetweenObjects = Vector3.Angle(Vector3.forward, planarTarget - planarPostion) * (p.x > transform.position.x ? 1 : -1);
        Vector3 finalVelocity = Quaternion.AngleAxis(angleBetweenObjects, Vector3.up) * velocity;

        // Fire!
        rigid.velocity = finalVelocity;
        rigid.AddTorque(new Vector3(1, 1, 1) * torqueForce, ForceMode.Impulse);

        // Alternative way:
        // rigid.AddForce(finalVelocity * rigid.mass, ForceMode.Impulse);
    }

}
