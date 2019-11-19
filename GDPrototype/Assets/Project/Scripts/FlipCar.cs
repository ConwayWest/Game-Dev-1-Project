using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Kyle Conway
// Last Updated 10/23/2019
// FlipCar.cs
// Checks to see if the rigidbody of the car is beyond a certain threshold on the 
// y axis over a certain period of time, if it is the rigidbody is transformed right 
// side up in the same forward direction

public class FlipCar : MonoBehaviour
{
    // Car rigid body
    Rigidbody rb;

    // Stores time to check
    float lastTimeChecked;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    void RightCar()
    {
        // Adjust position to right side up
        this.transform.position += Vector3.up;

        // Keeps rotation
        this.transform.rotation = Quaternion.LookRotation(this.transform.forward);
    }

    // Update is called once per frame
    void Update()
    {
        // If car is right side up, resets time
        if(transform.up.y > 0.5f || rb.velocity.magnitude > 1)
        {
            lastTimeChecked = Time.time;
        }

        // If car is not right side up and time has elapsed over a certain period
        if(Time.time > lastTimeChecked + 3)
        {
            RightCar();
        }
    }
}
