using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class SteerState : PlayerBaseState
{
    PlayerStateController sc;

    private Rigidbody2D rb;
    private GameObject SteeringWheel;

    public SteerState(PlayerStateController stateMachine, GameObject steering) : base("Steer", stateMachine)
    {
        sc = stateMachine;

        rb = sc.GetComponent<Rigidbody2D>();
        SteeringWheel = steering;
        //steeringWheelInteractable = SteeringWheel.GetComponent<Interactable>();
    }

    public override void OnEnter()
    {
        /*Vector3 rot = steeringWheelInteractable.interactRotation;
        Quaternion shipRot = sc.ship.transform.rotation;

        Debug.Log("Rotation " + rot + " " + shipRot.eulerAngles.z);

        sc.transform.rotation = Quaternion.Euler(rot.x, rot.y, rot.z + shipRot.eulerAngles.z);*/
    }

    public override void Update()
    {
        /*float horizontalInput = -Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        float shiftInput = Input.GetAxisRaw("Leave");

        // If shift is pressed, change state back to regular player state
        if (shiftInput == 1)
            sc.ChangeState(sc.interactableStates[0]);

        // Change player's position to be next to the interactable
        sc.transform.localPosition = SteeringWheel.transform.localPosition
            + steeringWheelInteractable.interactPosition;

        // Change ship's rotation using horizontal input
        sc.ship.transform.Rotate(Vector3.forward * Time.deltaTime * sc.rotationSpeed * horizontalInput);*/
    }
    public override void OnExit()
    {

    }
}
