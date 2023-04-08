using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class HoldBehaviour : MonoBehaviour
{
    public bool isHeld = false;
    bool canToggle = true;

    public string holdableName;
    public float holdRadius;
    public float toggleDelay;
    public float friction;
    public Vector3 holdPosition;
    public Vector3 holdRotation;
    public bool inGen = true;

    public int holdingSortingOrder = 10;
    public int droppedSortingOrder = 0;

    [HideInInspector]
    public PlayerStateController PlayerController;
    public GameObject Player;
    public GameObject Ship;

    [HideInInspector]
    public Rigidbody2D rb;
    [HideInInspector]
    public BoxCollider2D boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        PlayerController = Player.GetComponent<PlayerStateController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isHeld)
        {
            float distance = Vector3.Distance(transform.position, PlayerController.transform.position);
            if (distance < holdRadius && PlayerController.wantToHold && !PlayerController.isHolding && canToggle)
                PickupHoldable();
            else
                rb.velocity *= friction;
        }
        else
        {
            if(holdableName == "fish" && PlayerController.wantToInteract)
            {
            }
            if (PlayerController.wantToDrop && canToggle)
                DropHoldable(false);
        }
    }

    public void PickupHoldable()
    {
        isHeld = true;
        inGen = false;
        PlayerController.isHolding = true;
        gameObject.transform.SetParent(Player.transform);

        Debug.Log("BleepPick");
        rb.isKinematic = true;
        boxCollider.enabled = false;

        transform.localPosition = holdPosition;
        transform.rotation = PlayerController.transform.rotation;

        StartCoroutine(ToggleHold(toggleDelay));
    }

    public void DropHoldable(bool deleteHoldable)
    {
        isHeld = false;
        PlayerController.isHolding = false;
        gameObject.transform.SetParent(Ship.transform);

        
        rb.isKinematic = false;
        boxCollider.enabled = true;
        Debug.Log("BleepDrop, kinematic: " +  rb.isKinematic + ", boxCollider: " + boxCollider.enabled);

        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;

        if (deleteHoldable)
            Destroy(gameObject);

        StartCoroutine(ToggleHold(toggleDelay));
    }

    public void TogglePhysics(bool toggleOn)
    {
        if (toggleOn)
        {
            rb.isKinematic = false;
            boxCollider.enabled = true;
        }
        else
        {
            rb.isKinematic = true;
            boxCollider.enabled = false;
        }
    }

    private IEnumerator ToggleHold(float sec)
    {
        canToggle = false;
        yield return new WaitForSeconds(sec);
        canToggle = true;
    }
}
