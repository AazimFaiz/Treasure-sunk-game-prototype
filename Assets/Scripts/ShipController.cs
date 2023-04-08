using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public List<float> shipSpeedList = new List<float>();
    public int shipSpeedPointer = 1;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //rb.MovePosition((Vector2)transform.position + shipSpeed * Time.deltaTime * (Vector2)transform.right);

        
    }

    private void LateUpdate()
    {
        transform.position = (Vector2)transform.position + shipSpeedList[shipSpeedPointer]
                * Time.deltaTime * (Vector2)transform.right;
    }
}
