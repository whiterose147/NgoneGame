using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class LevelUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI singleLevelTimer, cannonControlText, cannonPlaceText, victoryScreenText;
    [SerializeField] private TextMeshProUGUI toolTipText;
    [SerializeField] private GameObject toolTipPanel, gameExitMenu;

    [SerializeField] private AudioSource source;     // Each time a level is completed PostTime is called once; this is to sync with  audio events.
    [SerializeField] private AudioClip levelCompleteAudio;
  

    private List<String> levelNames = new List<string>(11)
    {
        "Tutorial: ", "Level 1: ", "Level 2: ", "Level 3: ", "Level 4: ", "Level 5: ", "Level 6: ", "Level 7: ",
            "Level 8: ", "Level 9: ", "Level 10:"
    };

    private List<String> textToolTips = new List<string>(3)
    {
        "Buttons with a cyan outline only need to be activated once.", "Buttons with a blue outline are activated only when nothing is touching them.", 
        "Buttons with golden outlines stay activated much longer."
    };

    private List<float> levelTimers = new List<float>(11);


    private int levelNum = 0;
    private float masterTimer = 0;
    private float totalTime;
   
    
    void Start()
    {
        source = gameObject.AddComponent<AudioSource>();
        source.volume = .7f;
        cannonControlText.enabled = false;
        cannonPlaceText.enabled = false;
        victoryScreenText.enabled = false;
        gameExitMenu.SetActive(false);
        toolTipPanel.SetActive(false);
    }


    private void Update()
    {
        masterTimer += Time.deltaTime;
        if (levelNum >= 11)
        {
            levelNum = 11;
        }
        if (levelNum <= 10)
            singleLevelTimer.text = levelNames[levelNum] + masterTimer.ToString("0.00");
    }

    public void UnlockLevel10SoundEvent()
    {
        source.PlayOneShot(levelCompleteAudio); // level 10 is only played once
    }

    
   public void PostTime() // this is called once each time after a level has been completed
    {
        source.PlayOneShot(levelCompleteAudio);
        levelTimers.Add(masterTimer); // record the individual timer into the TimerList
        levelNum++; // Add to the level counter since the player completed the level

        if (levelNum == 11)
        {
            singleLevelTimer.text = "";

            for (int i = 0; i < 11; i++)
            {
                singleLevelTimer.text += levelNames[i] + " " + levelTimers[i].ToString("0.00") + " \n";
                totalTime += levelTimers[i];
            }

            singleLevelTimer.text += "\n" + "Total Time: " + totalTime.ToString("0.00");
            victoryScreenText.enabled = true;

        }
        masterTimer = 0;

        //Tool tips pop up after certain rooms, with the exception of the first one being activated after a set amount of time.
        switch (levelNum)
        {
            case 1:
                
                toolTipText.text = textToolTips[0];
                toolTipPanel.SetActive(true);
                StartCoroutine(ToolTipPauseTime());
                break;
            case 5:
                
                toolTipText.text = textToolTips[1];
                toolTipPanel.SetActive(true);
                StartCoroutine(ToolTipPauseTime());
                break;

            case 9:
                
                toolTipText.text = textToolTips[2];
                toolTipPanel.SetActive(true);
                StartCoroutine(ToolTipPauseTime());
                break;


            default:
                break;
        }



    }

    IEnumerator ToolTipPauseTime()
    {
        yield return new WaitForSeconds(10);
        toolTipPanel.SetActive(false);
    }


    private void OnEnable()
   {
       GridMovement.Place_CannonTextAction += PlaceCannonDownText;
       TakeControlOfCannons.TakeControlOfCannonText += TakeControlCannonText;
       QuitGameMenu.GameMenuAction += GameExitMenu;

   }

   private void OnDisable()
   {
       GridMovement.Place_CannonTextAction -= PlaceCannonDownText;
       TakeControlOfCannons.TakeControlOfCannonText -= TakeControlCannonText;
       QuitGameMenu.GameMenuAction -= GameExitMenu;


    }


   private void TakeControlCannonText(bool playerInRange)
   {
       if(playerInRange == true)
         cannonControlText.enabled = true;
       else
       {
           cannonControlText.enabled = false;
       }
   }

   private void PlaceCannonDownText(bool placeCannon)
   {
       if (placeCannon == true)
           cannonPlaceText.enabled = true;
       else
       {
           cannonPlaceText.enabled = false;
       }
   }


   private void GameExitMenu(bool incoming)
   {
       if (incoming == true)
           gameExitMenu.SetActive(true);
       else
           gameExitMenu.SetActive(false);
   }
  
}
