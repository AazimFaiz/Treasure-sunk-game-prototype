using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoleBehaviour : MonoBehaviour
{
    [HideInInspector]
    public GameObject Player;
    [HideInInspector]
    public bool isRepaired = false;
    public float repairTime;
    public float interactRadius;

    float totalDownTime = 0;
    bool isInteracting = false;

    SpriteRenderer SpriteRenderer;
    PlayerStateController PlayerController;

    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController == null)
            PlayerController = Player.GetComponent<PlayerStateController>();

        float distance = Vector3.Distance(transform.position, PlayerController.transform.position);
        if (PlayerController.wantToInteract && !isInteracting && distance < interactRadius)
        {
            totalDownTime = 0;
            isInteracting = true;
        }
        else if(!PlayerController.wantToInteract)
            isInteracting = false;

        if (isInteracting)
        {
            totalDownTime += 0.016f;

            Color old = SpriteRenderer.color;
            SpriteRenderer.color = new Color(old.r, old.g, old.b, totalDownTime/repairTime);

            if(totalDownTime >= repairTime)
             isRepaired = true;
        }
    }
}
