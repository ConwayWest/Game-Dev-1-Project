using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text lapDisplay;
    public Text placementDisplay;
    public Transform target;
    CheckpointManager cpManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void LateUpdate()
    {
        if (cpManager == null)
            cpManager = target.GetComponent<CheckpointManager>();

        lapDisplay.text = "Lap " + cpManager.lap + "/3";
        placementDisplay.text = "CP " + cpManager.checkPoint;
    }
}
