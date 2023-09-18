using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TutorialKeyCheckerObject : MonoBehaviour
{
    public List<TutorialComponent> components;
    public LevelUI postTime;

    public int roomGoal = 1;
    private int counter;
    private bool bSendTimeOnce = true;


    // keeps track of total room value. This is the tutorial, so only 1 condition needs to be met counter == 1 & threshhold == 1
    public void Trigger(int value) 
    {
       
        counter += value;
        counter = Mathf.Clamp(counter, 0, 1); // have to clamp it otherwise it will go over threshold

        if (counter == roomGoal)
        {
           
            Activate(); // activate all the objects in the room, such as: unlocking door, changing button color, etc

            if (bSendTimeOnce == true)
            {
                postTime.PostTime();
                bSendTimeOnce = false;

            }
        }
            
        else
            DeActivate();
    }



    // this is the equivalent of the player completing the room's challenge
    private void Activate() 
    {
        foreach (TutorialComponent item in components)
            item.ActivateAction();
    }

    private void DeActivate()
    {
        foreach (TutorialComponent item in components)
        {
            item.DeActivateAction();
        }
    }
}
