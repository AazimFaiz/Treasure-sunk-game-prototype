using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SteeringInteraction : InteractionInterface
{
    bool canInteract = true;

    public float rotationSpeed = 20f; // The speed at which the object should rotate
    public float leftPoint;
    public Vector3 rotatePivot;
    public bool spawnPivot = false;

    public GameObject pivotPrefab;
    GameObject rock;

    private InteractionBehaviour Interaction;

    void Start()
    {
        Interaction = GetComponent<InteractionBehaviour>();
        if (spawnPivot)
        {
            rock = Instantiate(pivotPrefab, (base.Ship.transform.position - base.Ship.transform.right * leftPoint), 
                pivotPrefab.transform.rotation, base.Ship.transform);
        }
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
        if(spawnPivot)
            rock.transform.position = base.Ship.transform.position - base.Ship.transform.right * leftPoint;
        if (canInteract)
        {
            // Change player's position to be next to the interactable
            Interaction.PlayerController.transform.localPosition = Interaction.transform.localPosition
                + Interaction.interactPosition;

            // Change ship's rotation using horizontal input
            //base.Ship.transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed *
            //    -Interaction.PlayerController.horizontalInput);

            float rotationAngle = Time.deltaTime * rotationSpeed * 
                -Interaction.PlayerController.horizontalInput;

            Vector3 rotateAroundPoint = base.Ship.transform.position - base.Ship.transform.right * leftPoint;
            base.Ship.transform.RotateAround(rotateAroundPoint, Vector3.forward, rotationAngle);
                
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
