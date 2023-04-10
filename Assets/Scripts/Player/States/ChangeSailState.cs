using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChangeSailState : PlayerBaseState
{
    private bool canInteract = true;

    private PlayerStateController sc;
    private Rigidbody2D rb;
    private GameObject ChangeSail;
    //private Interactable changeSailInteractable;
    private ShipController shipController;

    public ChangeSailState(PlayerStateController stateMachine, GameObject changeSail) : base("Regular", stateMachine)
    {
        sc = stateMachine;
        rb = sc.GetComponent<Rigidbody2D>();

        /*ChangeSail= changeSail;
        changeSailInteractable = changeSail.GetComponent<Interactable>();*/

        //shipController = sc.ship.GetComponent<ShipController>();
    }

    public override void OnEnter()
    {
        /*Vector3 rot = changeSailInteractable.interactRotation;
        Quaternion shipRot = sc.ship.transform.rotation;

        Debug.Log("Rotation " + rot + " " + shipRot.eulerAngles.z);

        sc.transform.rotation = Quaternion.Euler(rot.x, rot.y, rot.z + shipRot.eulerAngles.z);*/
    }

    public override void Update()
    {
        /*int horizontalInput = (int) Input.GetAxisRaw("Horizontal");
        int verticalInput = (int) Input.GetAxisRaw("Vertical");
        float shiftInput = Input.GetAxisRaw("Leave");

        // Change player's position to be next to the interactable
        sc.transform.localPosition = ChangeSail.transform.localPosition
            + changeSailInteractable.interactPosition;

        // If shift is pressed, change state back to regular player state
        *//*if (shiftInput == 1)
            sc.ChangeState(sc.interactableStates[0]);*//*

        ChangeSpeed(horizontalInput);*/
    }
                   
    public override void OnExit()
    {

    }

    private void ChangeSpeed(int change)
    {
        int speedListCount = shipController.shipSpeedList.Count;
        int speedPointer = shipController.shipSpeedPointer;

        if (canInteract && change != 0)
        {
            canInteract = false;
            shipController.shipSpeedPointer =  Math.Min(Math.Max(speedPointer + change, 0), speedListCount - 1);

            //IEnumerator coroutine = DelayChange(sc.changeSailDelay);
            //CoroutineManager.Start(coroutine);
        }
    }

    private IEnumerator DelayChange(float sec)
    {
        yield return new WaitForSeconds(sec);
        canInteract = true;
    }
}
