using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InteractionBehaviour : MonoBehaviour
{
    public Vector3 interactPosition;
    public Vector3 interactRotation;
    public float interactRadius;
    public float interactDelay;

    [HideInInspector]
    public bool isInteracting = false;
    public bool Leave = false;
    bool canInteract = true;

    [HideInInspector]
    public PlayerStateController PlayerController;
    public GameObject player;

    InteractionInterface Behaviour;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        PlayerController = player.GetComponent<PlayerStateController>();
        Behaviour = GetComponent<InteractionInterface>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isInteracting)
        {
            float distance = Vector3.Distance(transform.position, PlayerController.transform.position);
            if (distance < interactRadius && PlayerController.wantToInteract && canInteract)
            {
                // Debug.Log("Player is close to steering!: " + sc.transform.localPosition);
                PlayerController.isInteracting = true;
                isInteracting = true;
                Behaviour.OnEnter();
            }
        }
        else
        {
            if (PlayerController.wantToLeave || Leave)
            {
                PlayerController.isInteracting = false;
                isInteracting = false;
                Leave = false;
                Behaviour.OnExit();

                StartCoroutine(InteractDelay(interactDelay));
            }
            else
            {
                Behaviour.OnUpdate();
            }
        }
    }

    IEnumerator InteractDelay(float delay)
    {
        canInteract = false;
        yield return new WaitForSeconds(delay);
        canInteract = true;
    }
}
