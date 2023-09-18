using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenAllDoors : MonoBehaviour
{
    public List<GameObject> doors;
    private void OnEnable()
    {
        R9DoorGREEN.OpenAllDoors += OpenTHEEDoors;
    }


    private void OnDisable()
    {
        R9DoorGREEN.OpenAllDoors -= OpenTHEEDoors;
    }


    private void OpenTHEEDoors()
    {
        foreach (GameObject item in doors)
        {
            item.GetComponent<BoxCollider>().enabled = false;
            item.GetComponent<MeshRenderer>().enabled = false;
        }

    }


   

}
