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

        ShipController shipControl = sc.ship.GetComponent<ShipController>();

        Vector2 direction = new Vector2(horizontalInput, verticalInput).normalized ;
        direction = sc.ship.transform.TransformDirection(direction);
        //rb.MovePosition((Vector2)sc.transform.position + sc.moveSpeed * Time.deltaTime * direction);
        rb.velocity = sc.moveSpeed  * direction;

        Vector2 mousePos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        sc.transform.up = (Vector3)(mousePos - new Vector2(sc.transform.position.x, sc.transform.position.y));

        if (spaceInput == 1)
            sc.wantToInteract = true;
        else
            sc.wantToInteract = false;

        /*for(int i = 0;i < sc.interactableList.Count; ++i)
        {
            GameObject interactableObj = sc.interactableList[i];
            Interactable interactable = interactableObj.GetComponent<Interactable>();

            float distance = Vector3.Distance(sc.transform.position, interactableObj.transform.position);
            if (distance < interactable.interactRadius)
            {
                // Debug.Log("Player is close to steering!: " + sc.transform.localPosition);
                if (spaceInput == 1)
                {
                    sc.isInteracting = true;
                    sc.ChangeState(sc.interactableStates[i + 1]);
                    break;
                }
            }
        }*/
    }

    public override void OnExit()
    {
    }
}
