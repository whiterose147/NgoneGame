using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform localTrans;

    [SerializeField] private Transform target;
    [SerializeField] private Transform mainPlayer; // must always be on the "PlayerCameraObject"
    private GameObject mainPlayerGameObject;

    void Start()
    {
        localTrans = transform;
        mainPlayerGameObject = GameObject.FindGameObjectWithTag("MainPlayer");
    }

    void LateUpdate() { localTrans.position = target.position; }


    private void OnEnable()
    {
        TakeControlOfCannons.sendCameraOver += PlayerCameraUpdateObject;
        GridMovement.giveControlBackToPlayerBody += TransferControlBackToPlayer;
    }

    private  void OnDisable()
    {
        TakeControlOfCannons.sendCameraOver -= PlayerCameraUpdateObject;
        GridMovement.giveControlBackToPlayerBody -= TransferControlBackToPlayer;
    }

    private void PlayerCameraUpdateObject(Transform other) { target = other;  }

    private void TransferControlBackToPlayer()
    {
        target = mainPlayer;

        mainPlayerGameObject.GetComponentInChildren<ControllerMovement>().enabled = true; // gives control back to the main player body
        mainPlayerGameObject.GetComponentInChildren<TakeControlOfCannons>().enabled = true; // allows the player to resume taking control of cannons
    }

}
