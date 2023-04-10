using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingRodInteraction : InteractionInterface
{
    bool isFishing = false;
    bool canFish = true;
    bool hasFish = false;
    bool canInteract = true;
    bool canStartInteract = false;

    public float startDelay;
    public float fishDelay;
    public float fishingTimeMin;
    public float fishingTimeMax;
    public float treasureChance;

    InteractionBehaviour Interaction;
    SpriteRenderer SpriteRenderer;

    Coroutine Fishing;

    public GameObject FishPrefab;
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        Interaction = GetComponent<InteractionBehaviour>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
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
        else if(hasFish)
        {
            Interaction.Leave = true;
        }
        else
        {
            StartCoroutine(StartInteractDelay(startDelay));
        }
    }

    override public void OnUpdate()
    {
        if (canInteract)
        {
            // Change player's position to be next to the interactable
            Interaction.PlayerController.transform.localPosition = Interaction.transform.localPosition
                + Interaction.interactPosition;

            if (hasFish)
            {
                // Leave and attach fish in exit
                Interaction.Leave = true;
            }
            else if (Interaction.PlayerController.wantToInteract && canFish && canStartInteract)
            {
                isFishing = !isFishing;
                if (isFishing)
                {
                    SpriteRenderer.color = new Color(1f, 0.7737637f, 0f, 1f);
                    if(Random.Range(0.0f, 1.0f) < treasureChance)
                        Fishing = StartCoroutine(GetTreasureAfterDelay(Random.Range(fishingTimeMin, fishingTimeMax)));
                    else
                        Fishing = StartCoroutine(GetFishAfterDelay(Random.Range(fishingTimeMin, fishingTimeMax)));
                }
                else
                {
                    SpriteRenderer.color = new Color(0f, 1f, 0.4470588f, 1f);
                    if (Fishing != null)
                        StopCoroutine(Fishing);
                }

                StartCoroutine(FishDelay(fishDelay));
            }
        }
    }

    override public void OnExit()
    {
        if (hasFish)
        {
            GameObject fish = Instantiate(FishPrefab, transform.position,
                FishPrefab.transform.rotation, transform.parent);
            HoldBehaviour fishBehaviour = fish.GetComponent<HoldBehaviour>();

            fishBehaviour.Ship = Ship;
            fishBehaviour.Player = Player;
            fishBehaviour.PlayerController = Player.GetComponent<PlayerStateController>();
            fishBehaviour.rb = fish.GetComponent<Rigidbody2D>();
            fishBehaviour.boxCollider = fish.GetComponent<BoxCollider2D>();
            fishBehaviour.TogglePhysics(false);
            fishBehaviour.PickupHoldable();

            SpriteRenderer.color = new Color(0f, 1f, 0.4470588f, 1f);

            hasFish = false;
            StartCoroutine(InteractDelay(0.2f));
        }
    }

    IEnumerator StartInteractDelay(float delay)
    {
        canStartInteract = false;
        yield return new WaitForSeconds(delay);
        canStartInteract = true;
    }

    IEnumerator InteractDelay(float delay)
    {
        canInteract = false;
        yield return new WaitForSeconds(delay);
        canInteract = true;
    }

    IEnumerator FishDelay(float delay)
    {
        canFish = false;
        yield return new WaitForSeconds(delay);
        canFish = true;
    }

    IEnumerator GetFishAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Fishing = null;
        hasFish = true;
        SpriteRenderer.color = new Color(0.7773361f, 0f, 1f, 1f);
    }

    IEnumerator GetTreasureAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Fishing = null;
        SpriteRenderer.color = new Color(0.7773361f, 0f, 1f, 1f);
        Debug.Log("YOU WON");
    }
}
