using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChangeSailInteraction : InteractionInterface
{
    bool canChangeSpeed = true; 
    public float changeSailDelay;

    InteractionBehaviour Interaction;

    void Start()
    {
        Interaction = GetComponent<InteractionBehaviour>();
    }

    // Start is called before the first frame update
    override public void OnEnter()
    {
        Vector3 rot = Interaction.interactRotation;
        Quaternion shipRot = base.Ship.transform.rotation;

        Interaction.PlayerController.transform.rotation = Quaternion.Euler(rot.x, rot.y, rot.z + shipRot.eulerAngles.z);

        if (Interaction.PlayerController.isHolding)
        {
            Transform holdable = Interaction.PlayerController.transform.GetChild(0);
            HoldBehaviour holdBehaviour = holdable.GetComponent<HoldBehaviour>();
            holdBehaviour.DropHoldable(false);
        }
    }

    override public void OnUpdate()
    {
        // Change player's position to be next to the interactable
        Interaction.PlayerController.transform.localPosition = Interaction.transform.localPosition
            + Interaction.interactPosition;

        ChangeSpeed((int) Interaction.PlayerController.horizontalInput);
    }

    override public void OnExit()
    {

    }

    private void ChangeSpeed(int change)
    {
        if (canChangeSpeed && change != 0)
        {
            ShipController shipController = base.Ship.GetComponent<ShipController>();

            int speedListCount = shipController.shipSpeedList.Count;
            int speedPointer = shipController.shipSpeedPointer;

            shipController.shipSpeedPointer = Math.Min(Math.Max(speedPointer + change, 0), speedListCount - 1);
            canChangeSpeed = false;

            StartCoroutine(DelayChange(changeSailDelay));
        }
    }

    private IEnumerator DelayChange(float sec)
    {
        yield return new WaitForSeconds(sec);
        canChangeSpeed = true;
    }
}
