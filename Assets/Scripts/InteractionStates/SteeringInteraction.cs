using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SteeringInteraction : InteractionInterface
{
    bool canInteract = true;

    public float rotationSpeed = 20f; // The speed at which the object should rotate

    private InteractionBehaviour Interaction;

    void Start()
    {
        Interaction = GetComponent<InteractionBehaviour>();
    }

    override public void OnEnter()
    {
        Vector3 rot = Interaction.interactRotation;
        Quaternion shipRot = base.Ship.transform.rotation;

        Interaction.PlayerController.transform.rotation = Quaternion.Euler(rot.x, rot.y, rot.z + shipRot.eulerAngles.z);

        if (Interaction.PlayerController.isHolding)
        {
            Transform holdable = Interaction.PlayerController.transform.GetChild(0);
            HoldBehaviour holdBehaviour = holdable.GetComponent<HoldBehaviour>();

            if (holdBehaviour.holdableName == "fish")
            {
                Interaction.Leave = true;
                StartCoroutine(InteractDelay(0.2f));
            }
            else if (holdBehaviour.holdableName == "cannonball")
            {
                holdBehaviour.DropHoldable(false);
            }
        }
    }

    override public void OnUpdate()
    {
        if (canInteract)
        {
            // Change player's position to be next to the interactable
            Interaction.PlayerController.transform.localPosition = Interaction.transform.localPosition
                + Interaction.interactPosition;

            // Change ship's rotation using horizontal input
            base.Ship.transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed *
                -Interaction.PlayerController.horizontalInput);
        }
    }

    override public void OnExit()
    {

    }

    IEnumerator InteractDelay(float delay)
    {
        canInteract = false;
        yield return new WaitForSeconds(delay);
        canInteract = true;
    }
}
