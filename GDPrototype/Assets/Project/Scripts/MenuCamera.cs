using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCamera : MonoBehaviour
{
    public GameObject target;
    private float speedMod = 5.0f;
    private Vector3 point;
    // Start is called before the first frame update
    void Start()
    {
        point = target.transform.position;
        transform.LookAt(point);
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(target.transform.position, Vector3.up, speedMod * Time.deltaTime);
    }
}
