using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextToolTips : MonoBehaviour
{
    //This script acivates the first tool tip, the rest of the tips are located in LevelUI.cs
    [SerializeField] private TextMeshProUGUI toolTip;
    [SerializeField] private GameObject toolTipPanel;

    private float localTimer;
    private float holdTime;

    private bool bTurnOffLocalTimer = false;
    void Start(){ toolTipPanel.SetActive(false); }

    // Update is called once per frame
    void Update()
    {
        localTimer += Time.deltaTime;
        localTimer = Mathf.Clamp(localTimer, 0, 5);

        if(localTimer == 5 && bTurnOffLocalTimer == false)
        {
            toolTipPanel.SetActive(true);
            holdTime += Time.deltaTime;
            holdTime = Mathf.Clamp(holdTime, 0, 7);
        }

        if(holdTime == 7)
        {
            bTurnOffLocalTimer = true;
            toolTipPanel.SetActive(false);
            gameObject.GetComponent<TextToolTips>().enabled = false;  // turn off this script once done to save on memory 
        }
    }
}
