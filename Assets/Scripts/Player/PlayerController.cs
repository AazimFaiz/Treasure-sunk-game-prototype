using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /*public float moveSpeed = 5f;
    public float detectionDistance = 2f;

    public GameObject steering;
    public GameObject ship;*/

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        /*float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        float spaceInput = Input.GetAxisRaw("Interact");
        float shiftInput = Input.GetAxisRaw("Leave");

        float distance = Vector3.Distance(transform.position, steering.transform.position);
        if (distance < detectionDistance)
        {
            Debug.Log("Player is close to steering!: " + transform.localPosition);
            if (spaceInput == 1)
            {
                isInteracting = true;
                Vector3 rot = interactable.interactRotation;

                transform.localPosition = steering.transform.localPosition + interactable.interactPosition;
                transform.rotation = Quaternion.Euler(rot.x, rot.y, rot.z);
            }
        }

        if (isInteracting)
        {
            if (shiftInput == 1)
                isInteracting = false;
        }
        else
        {
            Vector2 movement = new Vector2(horizontalInput, verticalInput).normalized;
            rb.velocity = movement * moveSpeed;
            rb.MovePosition((Vector2)transform.position + moveSpeed * Time.deltaTime * movement);

            Vector2 mousePos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.up = (Vector3)(mousePos - new Vector2(transform.position.x, transform.position.y));
        }*/
    }
}
