using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R2SingleHitButtonY : MonoBehaviour
{
    private bool bActivateOnce = true;
    public Room2KeyCheckerObject room2Condition;


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
            room2Condition.Trigger(1);



        }

    }
}
