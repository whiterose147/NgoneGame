using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room9KeyCheckerGREEN : MonoBehaviour
{
    public List<Room9GreenComponent> components;
    public LevelUI unlockLevel10SoundEvent;

    public int roomGoal = 1;
    private int counter;

    public void Trigger(int value)
    {

        counter += value;
        counter = Mathf.Clamp(counter, 0, 1); // have to clamp it otherwise it will go over threshold

        if (counter == roomGoal)
        {

            Activate(); // activate all the objects in the room, such as: unlocking door, changing button color, etc

           // Unlike the other door checker scripts, this one does not POST the time to the UI, because this door is still part of room 9 and 10. It is part of Room 10's total time
           // it will only play the sound.
           unlockLevel10SoundEvent.UnlockLevel10SoundEvent();
        }

        else
            DeActivate();
    }



    // this is the equivalent of the player completing the room's challenge
    private void Activate()
    {
        foreach (Room9GreenComponent item in components)
            item.ActivateAction();
    }

    private void DeActivate()
    {
        foreach (Room9GreenComponent item in components)
        {
            item.DeActivateAction();
        }
    }
}
