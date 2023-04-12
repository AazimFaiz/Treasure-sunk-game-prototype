using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Unity.IO.LowLevel.Unsafe;
using System;

public class PlayerStateController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float hungerIncrDelay;
    public int maxHunger = 10;
    public int hungerIncr = 1;
    public int eatGain = 5;

    Coroutine hungerCoroutine;
    int hunger = 10;

    [HideInInspector]
    public bool isHolding = false;
    [HideInInspector]
    public bool isInteracting = false;
    [HideInInspector]
    public bool wantToInteract = false;
    [HideInInspector]
    public bool wantToLeave = false;
    [HideInInspector]
    public bool wantToHoldToggle = false;
    [HideInInspector]
    public bool wantToHold = false;
    [HideInInspector]
    public bool wantToDrop = false;
    public bool hasWon = true;

    [HideInInspector]
    public float horizontalInput;
    [HideInInspector]
    public float verticalInput;
    float spaceInput;
    float shiftInput;
    float leftMouseInput;
    float rightMouseInput;

    public GameObject Ship;
    public GameObject HungerBar;
    public GameObject LogicManager;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        hungerCoroutine = StartCoroutine(IncreaseHunger());
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        spaceInput = Input.GetAxisRaw("Interact");
        shiftInput = Input.GetAxisRaw("Leave");
        leftMouseInput = Input.GetAxisRaw("Left Mouse");
        rightMouseInput = Input.GetAxisRaw("Right Mouse");

        wantToInteract = spaceInput != 0;
        wantToLeave = shiftInput != 0;
        wantToHoldToggle = leftMouseInput != 0;
        wantToHold = leftMouseInput != 0;
        wantToDrop = rightMouseInput != 0;
    }

    void LateUpdate()
    {
        if (!isInteracting && !hasWon)
        {
            Vector2 direction = new Vector2(horizontalInput, verticalInput).normalized;
            //direction = Ship.transform.TransformDirection(direction);
            rb.velocity = 
                Ship.GetComponent<ShipController>().rb.velocity + 
                moveSpeed * direction;

            Vector2 mousePos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.up = (Vector3)(mousePos - new Vector2(transform.position.x, transform.position.y));
        }
    }

    public void EatFish()
    {
        hunger += eatGain;
        if(hunger > maxHunger)
            hunger = maxHunger;
        HungerBar.GetComponent<SliderBar>().setHealth(hunger);

        StopCoroutine(hungerCoroutine);
        StartCoroutine(IncreaseHunger());
    }

    IEnumerator IncreaseHunger()
    {
        yield return new WaitForSeconds(hungerIncrDelay);
        hunger -= hungerIncr;

        if(hunger > - 1)
            HungerBar.GetComponent<SliderBar>().setHealth(hunger);

        if (hunger > 0)
            hungerCoroutine = StartCoroutine(IncreaseHunger());
        else
            LogicManager.GetComponent<LogicManager>().EndGameLoss();

    }
}

