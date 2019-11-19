using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Kyle Conway
// Last Updated 10/23/2019
// Circuit.cs
// Collects vector3 of various waypoints set across the game map in 3D
// puts them all into a array/list to define a racing circuit

public class Circuit : MonoBehaviour
{
    // Array/List to store various waypoints
    public GameObject[] waypoints;

    // Does not draw our line between waypoints if object not selected
    private void OnDrawGizmos()
    {
        DrawGizmos(false);
    }

    // Does draw line between waypoints when object selected
    private void OnDrawGizmosSelected()
    {
        DrawGizmos(true);
    }

    private void DrawGizmos(bool selected)
    {
        // Returns so line is no longer drawn
        if (selected == false) return;

        // Draws the line in order of waypoints
        if(waypoints.Length > 1)
        {
            Vector3 prev = waypoints[0].transform.position;
            for(int i = 1; i < waypoints.Length; i++)
            {
                Vector3 next = waypoints[i].transform.position;
                Gizmos.DrawLine(prev, next);
                prev = next;
            }
            // Connects the last waypoint and first to form a circuit
            Gizmos.DrawLine(prev, waypoints[0].transform.position);
        }
    }
}
