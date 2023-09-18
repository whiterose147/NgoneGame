using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowConstButtonTutorial : MonoBehaviour
{
    public TutorialKeyCheckerObject condition;

    private float localTimer = 0f;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("YellowCBall") && localTimer <= 4)
        {
            condition.Trigger(1); 
            localTimer = 0;
            

        }
        
    }
  
    private void Update()
    {
        localTimer += Time.deltaTime;
        localTimer = Mathf.Clamp(localTimer, 0, 4);


        if (localTimer == 4)
        {
            condition.Trigger(-1);
            localTimer = 0;
        }
            

    }
    
   
}
