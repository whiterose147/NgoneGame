using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R5YButtonInverse : MonoBehaviour
{
    public Room5KeyCheckerObject condition;

    private float localTimer = 0f;

    [SerializeField] private GameObject buttonPad;


    private Material originalMaterial;
    private Color originalColor;
    private Color newColor;
    private Material newMaterial;

    private bool bSendTriggerOneTime = true;

    private void Start()
    {
        newColor.r = 1;
        newColor.g = 0;
        newColor.b = .5f;
        newColor.a = 1;
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
                condition.Trigger(-1); // this button is inversed, it activates when there is NO trigger source.
                bSendTriggerOneTime = false;
            }


            newMaterial.color = newColor;
            localTimer = 0;


        }

    }

    private void Update()
    {
        localTimer += Time.deltaTime;
        localTimer = Mathf.Clamp(localTimer, 0, 5);


        if (localTimer == 5)
        {
            if (bSendTriggerOneTime == false)
            {
                bSendTriggerOneTime = true;
                condition.Trigger(1);
            }

            originalMaterial.color = originalColor;
            localTimer = 0;
        }


    }
}
