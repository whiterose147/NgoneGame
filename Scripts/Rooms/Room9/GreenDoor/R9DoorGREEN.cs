using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class R9DoorGREEN : Room9GreenComponent
{
    public static Action OpenAllDoors = delegate { };

    
    [SerializeField] private GameObject door;
    public override void ActivateAction()
    {
        door.GetComponent<BoxCollider>().enabled = false;
        door.GetComponent<MeshRenderer>().enabled = false;
        OpenAllDoors();
    }

    public override void DeActivateAction()
    {
        door.GetComponent<BoxCollider>().enabled = true;
        door.GetComponent<MeshRenderer>().enabled = true;
    }



}
