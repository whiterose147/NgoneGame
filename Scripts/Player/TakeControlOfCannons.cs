using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TakeControlOfCannons : MonoBehaviour
{
    public static Action<Transform> sendCameraOver = delegate { };
    public static Action SetAnimationToIdle = delegate { };
    public static Action<bool> TakeControlOfCannonText = delegate { };


    private PlayerActions pInput;
    private Transform cannonCameraObject;


    private bool bPlayerControlText = false;
    private bool bEnabletext;
   
    // Start is called before the first frame update
    void OnEnable()
    {
        pInput = new PlayerActions();
        pInput.Enable();
        bEnabletext = true;
    }

    private void OnDisable()
    {
        pInput.Disable();
        bEnabletext = false;
    }

    private void OnTriggerStay(Collider other)
    {

        if (bEnabletext == false)
            return;

        if (other.gameObject.CompareTag("CannonTriggerArea")) // flag to control the text prompt
        {
            bPlayerControlText = true;
            TakeControlOfCannonText(bPlayerControlText);
        }


        if (other.gameObject.CompareTag("CannonTriggerArea") && pInput.PlatformerAction.TakeControlOfCannon.ReadValue<float>() == 1) // check to see if player is in cannon and E is pressed
        {
            bPlayerControlText = false;
            bEnabletext = false;
            TakeControlOfCannonText(bPlayerControlText);
            SetAnimationToIdle();


            other.gameObject.GetComponentInParent<GridMovement>().enabled = true; // enable the local cannon's movement
                gameObject.GetComponent<ControllerMovement>().enabled = false;  // disable the player character's controller

                cannonCameraObject = other.GetComponentInParent<GridMovement>().cannonTarget; // make sure the camera is sent over to the cannon
                 sendCameraOver(cannonCameraObject);

                 
                 gameObject.GetComponent<TakeControlOfCannons>().enabled = false; // disable this script so it does not conflict with the cannon.

        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("CannonTriggerArea"))
        {
            bPlayerControlText = false;
            TakeControlOfCannonText(bPlayerControlText);
        }
    }

}
