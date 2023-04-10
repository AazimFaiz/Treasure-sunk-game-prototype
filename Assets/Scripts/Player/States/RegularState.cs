using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
using System;


public class RegularState : PlayerBaseState
{
    PlayerStateController sc;

    private Rigidbody2D rb;

    public RegularState(PlayerStateController stateMachine) : base("Regular", stateMachine) 
    {
        sc = stateMachine;
        rb = sc.GetComponent<Rigidbody2D>();
    }

    public override void OnEnter()
    {

    }

    public override void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        float spaceInput = Input.GetAxisRaw("Interact");

       /* ShipController shipControl = sc.ship.GetComponent<ShipController>();

        Vector2 direction = new Vector2(horizontalInput, verticalInput).normalized ;
        direction = sc.ship.transform.TransformDirection(direction);*/
        //rb.MovePosition((Vector2)sc.transform.position + sc.moveSpeed * Time.deltaTime * direction);
        //rb.velocity = sc.ship.GetComponent<ShipController>().rb.velocity + sc.moveSpeed  * direction;

        Vector2 mousePos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        sc.transform.up = (Vector3)(mousePos - new Vector2(sc.transform.position.x, sc.transform.position.y));

        if (spaceInput == 1)
            sc.wantToInteract = true;
        else
            sc.wantToInteract = false;
    }

    public override void OnExit()
    {
    }
}
