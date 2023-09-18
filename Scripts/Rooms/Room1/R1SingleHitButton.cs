using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class R1SingleHitButton : MonoBehaviour
//These buttons only require to be activated once to trigger the door, unlike the other normal button which requires a constant source.
{


    private bool bActivateOnce = true;
    public Room1KeyCheckerObject room1Condition;


    private Material newMaterial;
    [SerializeField] private GameObject button;
    // these buttons are local; therefore will not turn green as a result of the entire system being activated. They will turn green on individual action.
    private void Start()
    {
        newMaterial = button.GetComponent<Renderer>().material;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("YellowCBall") && bActivateOnce == true)
        {
            bActivateOnce = false;
            newMaterial.color = Color.green;
            room1Condition.Trigger(1);
           


        }

    }

    
}
