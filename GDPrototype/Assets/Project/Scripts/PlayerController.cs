using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Kyle Conway
// Last Updated 10/23/2019
// PlayerController.cs
// Accepts player input and uses it in conjunction with scripting 
// to produce desired game output

public class PlayerController : MonoBehaviour
{
    // Be able to access Drive.cs script
    Drive ds;

    public GameObject playerCar;

    // Start is called before the first frame update
    void Start()
    {
        ds = this.GetComponent<Drive>();

        if(playerCar.name == "RedCar")
        {
            playerCar.transform.position = new Vector3(-213.7322f, 2.337418f, -348.9078f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!RaceMonitor.racing) return;
        // holds various input values
        float a = Input.GetAxis("Vertical");
        float s = Input.GetAxis("Horizontal");
        float b = Input.GetAxis("Jump");

        // Controls locomotion/braking
        // Rotates wheel meshes with wheel colliders
        ds.Go(a, s, b);

        // Checks wheelhit slippage for skidsound and skid smoke particles 
        ds.CheckForSkid();

        // Controls pitch of engine audio based off speed
        // and where that fits within a certain gear criteria
        ds.CalculateEngineSound();
    }
}
