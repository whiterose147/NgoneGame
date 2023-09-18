using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class QuitGameMenu : MonoBehaviour
{
    public static Action<bool> GameMenuAction = delegate { };
    public PlayerActions pAction;

    private bool bMenuActive = false;
    public void Awake()
    {
        pAction = new PlayerActions();
        pAction.Enable();
        pAction.PlatformerAction.ExitMenu.performed += ToggleMenu;
        pAction.PlatformerAction.Exit.performed += QuitGame;
    }

    public void OnDisable()
    {
        pAction.PlatformerAction.ExitMenu.performed -= ToggleMenu;
        pAction.PlatformerAction.Exit.performed -= QuitGame;
        pAction.Disable();
       
    }
    private void ToggleMenu(InputAction.CallbackContext c)
    {
        if (bMenuActive)
        {
            bMenuActive = false;
            GameMenuAction(bMenuActive);
        }
        else
        {
            bMenuActive = true;
            GameMenuAction(bMenuActive);
        }
    }
    private void QuitGame(InputAction.CallbackContext c)
    {
        if(bMenuActive)
        {
            print("Game has exited"); // Print this so we can see if the game has successfully quit in Unity Editor
            Application.Quit();
        }
    }
}
