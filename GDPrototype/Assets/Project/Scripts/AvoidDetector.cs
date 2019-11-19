using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidDetector : MonoBehaviour
{
    // offset
    public float avoidPath = 0;

    // time where AI focuses on avoidance rather than waypoints
    public float avoidTime = 0;

    // distance between you and other car
    public float wanderDistance = 2;

    // length of time for avoidance (in seconds)
    public float avoidLength = 1;

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag != "car") return;
        avoidTime = 0;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag != "car") return;

        Rigidbody otherCar = collision.rigidbody;
        avoidTime = Time.time + avoidLength;

        // get other cars position relative to cars position
        Vector3 otherCarLocalTarget = transform.InverseTransformPoint(otherCar.gameObject.transform.position);

        float otherCarLocalAngle = Mathf.Atan2(otherCarLocalTarget.x, otherCarLocalTarget.z);

        avoidPath = wanderDistance * -Mathf.Sign(otherCarLocalAngle);
    }
}