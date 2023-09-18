using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerMovement : MonoBehaviour
{

    public Animator anime;
    public CharacterController cController;
    public PlayerActions pAction;

    [SerializeField] private float playerMovementSpeed = 5f;
    [SerializeField] private float gravity = -9.8f; 
    [SerializeField] private float rotationSpeed = 5f;
    
    public void OnEnable()
    {
        cController = transform.GetComponent<CharacterController>(); // assigns the transform to the controller value.
        TakeControlOfCannons.SetAnimationToIdle += ChangeAnimationToIdle;
        pAction = new PlayerActions(); 
        pAction.Enable();
    }
   
    public void OnDisable()
    {
        pAction.Disable();
        TakeControlOfCannons.SetAnimationToIdle -= ChangeAnimationToIdle;
    }

    private void FixedUpdate() { Movement(); }

    private void Movement()
    {
        Vector3 movementDirection = Vector3.zero;

         movementDirection += Vector3.ProjectOnPlane(Camera.main.transform.right, transform.up).normalized * // project on a plane so the player doesn't move upwards in the sky with the camera
            pAction.PlatformerAction.Movement.ReadValue<Vector2>().x;

        movementDirection += Vector3.ProjectOnPlane(Camera.main.transform.forward, transform.up).normalized *
           pAction.PlatformerAction.Movement.ReadValue<Vector2>().y;

        //If necessary, clamp the direction of this movement vector to magnitude of 1f;
        if (movementDirection.magnitude > 1f)
            movementDirection.Normalize();

       //Make sure to increase gravity in the Inspector. This is due to the value getting decreased from playerMovementSpeed in order to adjust for the camera direction vector
        movementDirection.y = gravity * Time.deltaTime;



        cController.Move(movementDirection * playerMovementSpeed); //using the move function of the CharacterController class in order to move it with regard to our movement actions.


        //Rotation
        if (movementDirection.x != 0 || movementDirection.z !=0)
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(movementDirection.x, 0, movementDirection.z)),
            Time.deltaTime * rotationSpeed);

        //Animation
        anime.SetFloat("speed", new Vector3(movementDirection.x, 0, movementDirection.z).magnitude);
    }

    private void ChangeAnimationToIdle() { anime.SetFloat("speed", new Vector3(0, 0, 0).magnitude); }

}

