using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public List<float> shipSpeedList = new();
    public int shipSpeedPointer = 1;
    public float collisionPushbackForce;
    public float decayRate;
    public float shipMaxHealth;
    public float shipRegain;
    public float holeSpawnChance;
    public float healthDrain;
    public float collisionDamage;
    public bool inTreasureBoundary = false;

    [HideInInspector]
    public Rigidbody2D rb;
    public GameObject Hole;
    public GameObject Player;
    public GameObject HealthBar;
    public GameObject SharkTarget;
    public GameObject LogicManager;
    public List<Vector3> holeSpawns;
    List<Vector3> availableHoleSpawns;
    List<GameObject> currentHoles = new();
    

    public bool isPushedBack = false;
    public int currentHoleCount = 0;
    public float shipHealth;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        shipHealth = shipMaxHealth;
        availableHoleSpawns = holeSpawns;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isPushedBack)
            rb.velocity = shipSpeedList[shipSpeedPointer] * (Vector2)transform.right;

        if (currentHoleCount == 0 && shipHealth < shipMaxHealth)
        {
            shipHealth += shipRegain;
            HealthBar.GetComponent<SliderBar>().setHealth(shipHealth);
        }
        else if(currentHoleCount > 0)
        {
            shipHealth -= healthDrain;
            HealthBar.GetComponent<SliderBar>().setHealth(shipHealth);

            if (shipHealth < 1)
                LogicManager.GetComponent<LogicManager>().EndGameLoss();
        }

        foreach(GameObject hole in currentHoles.ToList())
        {
            if (hole.GetComponent<HoleBehaviour>().isRepaired)
            {
                availableHoleSpawns.Add(hole.transform.position);
                Destroy(hole);
                currentHoles.Remove(hole);
                currentHoleCount--;
            }
        }

        GetRandomPointOnEdge();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        string cName = collision.gameObject.name;
        bool collisionCheck = cName == "Shark" || cName == "Rock" || cName == "Statue";
        Debug.Log(cName);

        if (collisionCheck)
        {
            isPushedBack = true;

            //Vector3 forceDirection = collision.relativeVelocity.normalized;
            Vector3 forceDirection = (transform.position - collision.transform.position).normalized;
            float randVal = Random.Range(0.0f, 1.0f);
            Debug.Log("Random: " + randVal + " " + currentHoleCount + " " + holeSpawns.Count);

            if (randVal < holeSpawnChance && currentHoleCount <= holeSpawns.Count)
            {
                currentHoleCount++;

                int pickedHole = Random.Range(0, availableHoleSpawns.Count - 1);
                Vector3 spawnLocation = availableHoleSpawns[pickedHole];
                availableHoleSpawns.RemoveAt(pickedHole);

                GameObject newHole = Instantiate(Hole, spawnLocation, Hole.transform.rotation, transform);
                newHole.transform.localPosition = spawnLocation;
                newHole.GetComponent<HoleBehaviour>().Player = Player;
                currentHoles.Add(newHole);
            }

            shipHealth -= collisionDamage;
            HealthBar.GetComponent<SliderBar>().setHealth(shipHealth);

            if (shipHealth < 1)
                LogicManager.GetComponent<LogicManager>().EndGameLoss();

            // Apply the force to the rigidbody
            rb.AddForce(forceDirection * collisionPushbackForce, ForceMode2D.Impulse);
            StartCoroutine(ReturnToSteadySpeed());
        }
    }

    private IEnumerator ReturnToSteadySpeed()
    {
        // Wait for a short amount of time before gradually adjusting the velocity
        yield return new WaitForSeconds(0.3f);


         // Change the value to adjust the rate of decrease
        while (rb.velocity.magnitude > shipSpeedList[shipSpeedPointer])
        {
            rb.velocity *= decayRate * shipSpeedList[shipSpeedPointer]/shipSpeedList.Max();
            yield return new WaitForFixedUpdate();
        }

        // Set the velocity to the steady speed
        rb.velocity = shipSpeedList[shipSpeedPointer] * (Vector2)transform.right;
        rb.angularVelocity = 0;
        isPushedBack = false;
    }

    Vector2 GetRandomPointOnEdge()
    {
        BoxCollider2D boxCollider = transform.Find("ShipAttackBoundary").GetComponent<BoxCollider2D>();
        Vector2[] edgePoints = new Vector2[4];

        // Get the edge points of the collider
        edgePoints[0] = boxCollider.bounds.min; // Left bottom
        edgePoints[1] = boxCollider.bounds.max; // Right top
        edgePoints[2] = new Vector2(boxCollider.bounds.min.x, boxCollider.bounds.max.y); // Left top
        edgePoints[3] = new Vector2(boxCollider.bounds.max.x, boxCollider.bounds.min.y); // Right bottom

        // Choose a random edge
        int randomEdge = Random.Range(0, 1);
        int otherEdge = randomEdge == 0 ? 3 : 2;

        // Get a random point on the chosen edge
        Vector2 randomPointOnEdge = Vector2.Lerp(edgePoints[randomEdge], edgePoints[otherEdge], Random.Range(0f, 1f));

        return randomPointOnEdge;
    }

    public GameObject SpawnAndGetTarget(GameObject Target)
    {
        SharkTarget = Instantiate(Target, GetRandomPointOnEdge(), Quaternion.identity, transform);
        return SharkTarget;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Treasure Boundary")
            inTreasureBoundary = true;

    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Treasure Boundary")
            inTreasureBoundary = false;
    }
}
