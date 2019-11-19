using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Kyle Conway
// Last Updated 10/23/2019
// AntiRollBar.cs
// Applies significant force to wheel colliders to avoid 
// vehicles tipping over

public class AntiRollBar : MonoBehaviour
{
    // Antiroll float designed to heavily weight rigibody to ground
    private float antiRoll = 15000.0f;

    // Wheel collider of each wheel
    public WheelCollider wheelLFront;
    public WheelCollider wheelRFront;
    public WheelCollider wheelLBack;
    public WheelCollider wheelRBack;

    // Center of Mass, 3D primitive cube positioned for best weight allocation
    public GameObject COM;

    // Rigidbody of car
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();

        // Use 3D primitive cube position for rigidoby's center of mass
        rb.centerOfMass = COM.transform.localPosition;
    }

    void GroundWheels(WheelCollider WL, WheelCollider WR)
    {
        WheelHit hit;
        float travelLeft = 1.0f;
        float travelRight = 1.0f;

        bool groundedL = WL.GetGroundHit(out hit);
        if (groundedL)
            travelLeft = (-WL.transform.InverseTransformPoint(hit.point).y - WL.radius) / WL.suspensionDistance;

        bool groundedR = WR.GetGroundHit(out hit);
        if (groundedR)
            travelRight = (-WR.transform.InverseTransformPoint(hit.point).y - WR.radius) / WR.suspensionDistance;

        float antiRollForce = (travelLeft - travelRight) * antiRoll;

        if (groundedL)
            rb.AddForceAtPosition(WL.transform.up * -antiRollForce, WL.transform.position);

        if (groundedR)
            rb.AddForceAtPosition(WR.transform.up * antiRollForce, WR.transform.position);
    }

    // No reason to check every single frame
    void FixedUpdate()
    {
        // Checks front wheels
        GroundWheels(wheelLFront, wheelRFront);

        // Checks back wheels
        GroundWheels(wheelLBack, wheelRBack);
    }
}
