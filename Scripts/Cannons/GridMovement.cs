using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  System;
using Debug = UnityEngine.Debug;

public class GridMovement : MonoBehaviour
{
    public static Action giveControlBackToPlayerBody = delegate { }; // deals with transferring control between main player and cannon object
    public static Action<bool> Place_CannonTextAction = delegate { };

    public Transform cannonTarget;

    private bool bPlacecannonText = false;

    //Grid movement

    public Transform startPosition;
    public PlayerActions pInput;

    public Vector2 gridSize;
    private Vector2 moveInput;
    private Vector3 maxPosition, desiredPosition;

    private bool bMove;

    public float moveSpeed = 5f;
    private float moveRate;
    
    public int moveDistance;

    //Rotation Variables
    private Quaternion desiredRotation;

    private float rotationInputDelayTimer = 0f;
    private float rotationDelayTime = .1f;

    private bool bRotateDelay = false;

    /// Ray casting variables
    public LayerMask cannonLayer;

    private void Awake()
    {
        // Set the rotation intervals differently depending on cannon type
        if (gameObject.tag == "YellowCannon")
            desiredRotation = Quaternion.Euler(0, 90, 0);

        else if (gameObject.tag == "BlueCannon")
            desiredRotation = Quaternion.Euler(0, 20, 0);


        pInput = new PlayerActions();
        pInput.Enable();

        maxPosition.x = startPosition.position.x + gridSize.x * moveDistance; // make sure to add start position 
        maxPosition.z = startPosition.position.z + gridSize.y * moveDistance;

    }

    private void OnEnable() // since the cannon is not moving by default, the text is enabled by default when the cannon becomes active
    {
        bPlacecannonText = true;
        Place_CannonTextAction(bPlacecannonText);
    }

    private void Update()
    {

        // Player cannot place cannon as it is moving; must be false
        if (pInput.PlatformerAction.PlaceDownCannon.ReadValue<float>() == 1 && bMove == false) // check if the F key is pressed or the North Button on the gamepad.
        {
            bPlacecannonText = false;
            Place_CannonTextAction(bPlacecannonText);

            giveControlBackToPlayerBody();
            gameObject.GetComponent<GridMovement>().enabled = false; // disable this script to give control back to player as well
            return;
        }



        if (bMove) // prevents the player from spamming the movement; can only move once it's done.
        {
            bPlacecannonText = false;
            Place_CannonTextAction(bPlacecannonText); // can ONLY place the cannon when it is not moving

            Move();
            return;
        }


        moveInput = pInput.PlatformerAction.CannonMovement.ReadValue<Vector2>();

        if (Physics.Raycast(transform.position, new Vector3(moveInput.x, 0, moveInput.y), moveDistance, cannonLayer)) // if there is another cannon, prevent it from moving there.
                 return;


        if (Mathf.Abs(moveInput.x) > 0 || Mathf.Abs(moveInput.y) > 0)
        {
         
            desiredPosition.y =
                transform.position.y; // make sure to copy the local object's Y, this will remain constant, so it does not change in height

            if (Mathf.Abs(moveInput.x) > 0) // This is to prevent diagonal movement and breaking off the grid.
            {
                if (moveInput.x > 0)
                    moveInput.x = 1;
                else if(moveInput.x < 0)
                  moveInput.x = -1;
                

                desiredPosition.x = transform.position.x + (moveInput.x * moveDistance);
                desiredPosition.x = Mathf.Clamp(desiredPosition.x, startPosition.position.x, maxPosition.x); // limit movement to the grid dimensions
                desiredPosition.z = transform.position.z;
            }
            else if (Mathf.Abs(moveInput.y) > 0)
            {
                if (moveInput.y > 0)
                    moveInput.y = 1;
                else if (moveInput.y < 0)
                    moveInput.y = -1;

                desiredPosition.z = transform.position.z + (moveInput.y * moveDistance);
                desiredPosition.z = Mathf.Clamp(desiredPosition.z, startPosition.position.z, maxPosition.z); // limit movement to the grid dimensions
                desiredPosition.x = transform.position.x;
            }

            bMove = true;
            moveRate = 0; // Reset the move rate so we just don't teleport to the location
        }



        if (bRotateDelay == true)                                                                                                                                                                                                                   // Celestia's ass it took me forever to figure this out xD
        {
            rotationInputDelayTimer += Time.deltaTime;
            if (rotationInputDelayTimer >= rotationDelayTime)
            {
                bRotateDelay = false;
                rotationInputDelayTimer = 0f;
            }
        }


        //Rotation
        if (pInput.PlatformerAction.CannonRotation.ReadValue<float>() == 1)
        {
            Rotate();
        }

    }


    private void Move()
    {
        moveRate += Time.deltaTime * moveSpeed;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, moveRate);
        if (moveRate >= 1)
        {
            bMove = false;
            bPlacecannonText = true; // cannon is done moving, player can place it now
            Place_CannonTextAction(bPlacecannonText);
        }
            
    }

    private void Rotate()
    {
        if (bRotateDelay == true) // if there is a delay going on, return and do nothing
            return;
        else
        {
            transform.rotation = desiredRotation;
        }


        if ((int)transform.rotation.eulerAngles.y == (int)desiredRotation.eulerAngles.y) // we have to (int) the values so we don't get floating point number errors
        {
            if(gameObject.tag == "YellowCannon")
                desiredRotation *= Quaternion.Euler(0, 90, 0);

            else if (gameObject.tag == "BlueCannon")
                desiredRotation *= Quaternion.Euler(0, 20, 0);

            bRotateDelay = true;

        }

    }
}