using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R9Door : Room9Component
{
    [SerializeField] private GameObject door;
    private bool bOverrideRoom9 = false;

    private void OnEnable()
    {
        R9DoorGREEN.OpenAllDoors += DeactivateRoom9Door;
    }


    private void OnDisable()
    {
        R9DoorGREEN.OpenAllDoors -= DeactivateRoom9Door;
    }
    public override void ActivateAction()
    {
        if (bOverrideRoom9 == false)
        {
            door.GetComponent<BoxCollider>().enabled = false;
            door.GetComponent<MeshRenderer>().enabled = false;

        }
        
    }

    // ROOM 9 works different, in that once the door is open, it is OPEN. It will only deactivate once level 10 is enabled.
    private void DeactivateRoom9Door()
    {
        bOverrideRoom9 = true;
        door.GetComponent<BoxCollider>().enabled = true;
        door.GetComponent<MeshRenderer>().enabled = true;
    }
}
