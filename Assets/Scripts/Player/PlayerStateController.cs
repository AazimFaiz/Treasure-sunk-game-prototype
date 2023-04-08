using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Unity.IO.LowLevel.Unsafe;
using System;

public class PlayerStateController : MonoBehaviour
{
    PlayerBaseState currentState;

    public float moveSpeed = 5f;

    public bool isHolding = false;
    public bool isInteracting = false;
    public bool wantToInteract = false;
    public bool wantToLeave = false;
    public bool wantToHoldToggle = false;
    public bool wantToHold = false;
    public bool wantToDrop = false;

    public float horizontalInput;
    public float verticalInput;
    float spaceInput;
    float shiftInput;
    float leftMouseInput;
    float rightMouseInput;

    public GameObject ship;

    private Rigidbody2D rb; 

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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

        if (!isInteracting)
        {
            ShipController shipControl = ship.GetComponent<ShipController>();

            Vector2 direction = new Vector2(horizontalInput, verticalInput).normalized;
            direction = ship.transform.TransformDirection(direction);
            rb.velocity = moveSpeed * direction;

            Vector2 mousePos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.up = (Vector3)(mousePos - new Vector2(transform.position.x, transform.position.y));
        }
    }
}

