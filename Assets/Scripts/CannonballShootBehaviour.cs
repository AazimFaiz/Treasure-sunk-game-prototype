using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonballShootBehaviour : MonoBehaviour
{
    public float moveSpeed;
    public float maxDistance;

    float travelledDistance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = transform.up;

        // Move the GameObject in the direction it is facing
        travelledDistance += moveSpeed * Time.deltaTime;
        transform.position += direction * moveSpeed * Time.deltaTime;

        if(travelledDistance > maxDistance)
            Destroy(gameObject);
    }
}
