using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraControls : MonoBehaviour
{
    private PlayerActions pInput;
    Vector3 desiredRotation;
    Quaternion caculatedRotation;
    [SerializeField] private float cameraRotationSpeed = 5f;

    private void OnEnable()
    {
        pInput = new PlayerActions();
        pInput.Enable();
        pInput.PlatformerAction.CameraMouse.performed += ButtonPressed;
        pInput.PlatformerAction.CameraMouse.canceled += ButtonReleased;

    }
    private void OnDisable()
    {
        pInput.Disable();
        pInput.PlatformerAction.CameraMouse.performed -= ButtonPressed;
        pInput.PlatformerAction.CameraMouse.canceled -= ButtonReleased;
    }
    private void ButtonPressed(InputAction.CallbackContext c) { }
    private void ButtonReleased(InputAction.CallbackContext c) {  }
    private void Update(){ MoveCamera(); }
    private void MoveCamera() // Move camera based on read in values from input system
    {
        desiredRotation.x = transform.localRotation.eulerAngles.x + pInput.PlatformerAction.CameraMouse.ReadValue<Vector2>().y * -1;
        desiredRotation.y = transform.localRotation.eulerAngles.y + pInput.PlatformerAction.CameraMouse.ReadValue<Vector2>().x;
        desiredRotation.z = 0;

        caculatedRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(desiredRotation), Time.deltaTime * cameraRotationSpeed);

        transform.localRotation = Quaternion.Euler(new Vector3(caculatedRotation.eulerAngles.x, caculatedRotation.eulerAngles.y,0));
    }
}
