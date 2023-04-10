using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CannonInteraction : InteractionInterface
{
    bool canInteract = false;
    bool canShoot = false;
    int loadedCannonballs = 0;
    float currentRotation = 0;

    public float maxRotation;
    public float minRotation;
    public float rotationSpeed;
    public float shootDelay;
    public bool flipped = false;

    public GameObject CannonballPrefab;

    InteractionBehaviour Interaction;

    void Start()
    {
        Interaction = GetComponent<InteractionBehaviour>();
        if (flipped)
            currentRotation = 180;
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
                Interaction.Leave = true;
                holdBehaviour.DropHoldable(true);
                loadedCannonballs += 1;

                StartCoroutine(InteractDelay(0.2f));
            }
        }
        else
        {
            StartCoroutine(ShootDelay(0.2f));
            canInteract = true;
        }
            

    }

    override public void OnUpdate()
    {
        // Change player's position to be next to the interactable
        if (canInteract)
        {
            Interaction.PlayerController.transform.localPosition = Interaction.transform.localPosition
                + Interaction.interactPosition;
            

            float deltaRotation = Time.deltaTime * rotationSpeed * -Interaction.PlayerController.horizontalInput;
            currentRotation += deltaRotation;

            if (currentRotation > 180f && !flipped)
                currentRotation -= 360f;

            currentRotation = Mathf.Clamp(currentRotation, minRotation, maxRotation);

            // Apply the new rotation to the game object
            transform.rotation = Quaternion.Euler(0, 0, base.Ship.transform.rotation.eulerAngles.z + currentRotation);

            if(Interaction.PlayerController.wantToInteract && canShoot && loadedCannonballs > 0)
            {
                loadedCannonballs -= 1;
                Instantiate(CannonballPrefab, transform.position, transform.rotation);
                StartCoroutine(ShootDelay(shootDelay));
            }
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

    IEnumerator ShootDelay(float delay)
    {
        canShoot = false;
        yield return new WaitForSeconds(delay);
        canShoot = true;
    }
}


