using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Kyle Conway
// Last Updated 10/23/2019
// AIController.cs
// Handles driver AI behavior
// 

public class AIController : MonoBehaviour
{
    public GameObject robotCar;
    public Circuit circuit;
    Drive ds;
    public float steeringSensitivity = 0.01f;
    public float brakingSensitivity = 3.0f;
    public float accelSensitivity = 0.3f;
    Vector3 target;
    Vector3 nextTarget;
    int currentWP = 0;
    float totalDistanceToTarget;

    // Instantiating tracker with waypoint system
    GameObject tracker;
    int currentTrackerWP = 0;
    private float lookAhead = 100.0f;

    // For re-positioning stuck car
    float lastTimeMoving = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (robotCar.name == "GreenCar")
        {
            robotCar.transform.position = new Vector3(-135.8818f, 2.337418f, -349.2012f);
        }

        if (robotCar.name == "WhiteCar")
        {
            robotCar.transform.position = new Vector3(-95.72f, 2.337418f, -376.66f);
        }

        if (robotCar.name == "OrangeCar")
        {
            robotCar.transform.position = new Vector3(-173.7602f, 2.337418f, -377.0724f);
        }

        ds = this.GetComponent<Drive>();
        target = circuit.waypoints[currentWP].transform.position;
        nextTarget = circuit.waypoints[currentWP + 1].transform.position;
        totalDistanceToTarget = Vector3.Distance(target, ds.rb.gameObject.transform.position);

        // create tracker
        tracker = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        DestroyImmediate(tracker.GetComponent<Collider>());
        tracker.GetComponent<MeshRenderer>().enabled = false;

        // tracker mirrors rigidbody's position/rotation
        tracker.transform.position = ds.rb.gameObject.transform.position;
        tracker.transform.rotation = ds.rb.gameObject.transform.rotation;
    }

    void ProgressTracker()
    {
        Debug.DrawLine(ds.rb.gameObject.transform.position, tracker.transform.position);
        if(Vector3.Distance(ds.rb.gameObject.transform.position, tracker.transform.position) > lookAhead)
        {
            return;
        }

        tracker.transform.LookAt(circuit.waypoints[currentTrackerWP].transform.position);
        tracker.transform.Translate(0, 0, 1.0f);  // tracker speed

        // Checks to see if tracker has hit one of my waypoints
        if(Vector3.Distance(tracker.transform.position, circuit.waypoints[currentTrackerWP].transform.position) < 1)
        {
            // increment tracker to chase next waypoint
            currentTrackerWP++;

            // if tracker has reached last waypoint, set it to chase after starting waypoint
            if(currentTrackerWP >= circuit.waypoints.Length)
            {
                currentTrackerWP = 0;
            }
        }
    }

    void ResetLayer()
    {
        ds.rb.gameObject.layer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!RaceMonitor.racing)
        {
            lastTimeMoving = Time.time;
            return;
        }

        ProgressTracker();
        Vector3 localTarget;
        float targetAngle;

        if(ds.rb.velocity.magnitude > 1)
        {
            lastTimeMoving = Time.time;
        }

        if(Time.time > lastTimeMoving + 4)
        {
            // resets position and rotation
            ds.rb.gameObject.transform.position = 
                circuit.waypoints[currentTrackerWP].transform.position + Vector3.up * 3 + 
                new Vector3(Random.Range(-1,1),0,Random.Range(-1,1));

            // repositions tracker
            tracker.transform.position = ds.rb.gameObject.transform.position;

            ds.rb.gameObject.layer = 8;
            Invoke("ResetLayer", 3);
        }

        if(Time.time < ds.rb.GetComponent<AvoidDetector>().avoidTime)
        {
            localTarget = tracker.transform.right * ds.rb.GetComponent<AvoidDetector>().avoidPath;
        }
        else
        {
            localTarget = ds.rb.gameObject.transform.InverseTransformPoint(tracker.transform.position);
        }
        targetAngle = Mathf.Atan2(localTarget.x, localTarget.z) * Mathf.Rad2Deg;

        float steer = Mathf.Clamp(targetAngle * steeringSensitivity, -1, 1) * Mathf.Sign(ds.currentSpeed);

        float speedFactor = ds.currentSpeed / ds.maxSpeed;

        float corner = Mathf.Clamp(Mathf.Abs(targetAngle), 0, 90);
        float cornerFactor = corner / 90.0f;

        float brake = 0;
        if(corner > 20 && speedFactor > 0.1f)
        {
            brake = Mathf.Lerp(0, 1 + speedFactor * brakingSensitivity, cornerFactor);
        }
        float accel = 1f;
        if(corner > 35 && speedFactor > 0.2f)
        {
            accel = Mathf.Lerp(0, 1 * accelSensitivity, 1 - cornerFactor);
        }

        ds.Go(accel, steer, brake);

        // ds.CheckForSkid();
        // ds.CalculateEngineSound();
    }
}
