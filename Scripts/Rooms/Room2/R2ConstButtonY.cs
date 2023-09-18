using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R2ConstButtonY : MonoBehaviour
{
    public Room2KeyCheckerObject condition;

    private float localTimer = 0f;

    [SerializeField] private GameObject buttonPad;


    private Material originalMaterial;
    private Color originalColor;


    private Material newMaterial;

    private bool bSendTriggerOneTime = true;

    private void Start()
    {
        newMaterial = buttonPad.GetComponent<Renderer>().material;

        originalMaterial = buttonPad.GetComponent<Renderer>().material;
        originalColor = originalMaterial.color;

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("YellowCBall") && localTimer <= 4)
        {
            if (bSendTriggerOneTime == true)
            {
                condition.Trigger(1);
                bSendTriggerOneTime = false;
            }

            newMaterial.color = Color.green;
            localTimer = 0;


        }

    }

    private void Update()
    {
        localTimer += Time.deltaTime;
        localTimer = Mathf.Clamp(localTimer, 0, 4);


        if (localTimer == 4)
        {

            if (bSendTriggerOneTime == false)
            {
                bSendTriggerOneTime = true;
                condition.Trigger(-1);
            }

            originalMaterial.color = originalColor;
            localTimer = 0;
        }


    }
}