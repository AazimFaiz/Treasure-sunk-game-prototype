using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SharkBehaviour : MonoBehaviour
{
    AIPath aiPath;
    AIDestinationSetter destinationSetter;
    Rigidbody2D rb;

    public float targettingRadius;
    public float attackForce;
    public float waitDelay;
    public float afterAttackDelay;
    public GameObject Ship;
    public GameObject TargetPrefab;

    bool reachedShip = false;
    bool isTargetting = false;

    // Start is called before the first frame update
    void Start()
    {
        aiPath = GetComponent<AIPath>();
        destinationSetter = GetComponent<AIDestinationSetter>();    
        rb = GetComponent<Rigidbody2D>();

        aiPath.enabled = false;
        rb.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, Ship.transform.position);
        if (!isTargetting && distance < targettingRadius)
        {
            isTargetting = true;
            aiPath.enabled = true;

            GameObject Target = Ship.GetComponent<ShipController>().SpawnAndGetTarget(TargetPrefab);
            destinationSetter.target = Target.transform;
        }
            

        if (isTargetting && aiPath.remainingDistance > 1 && !reachedShip)
        {
            reachedShip = true;
            StartCoroutine(WaitAndAttack(waitDelay, afterAttackDelay));
        }
    }

    IEnumerator WaitAndAttack(float waitTime, float attackDelayTime)
    {
        yield return new WaitForSeconds(waitTime);

        aiPath.enabled = false;
        destinationSetter.enabled = false;
        Vector2 attackDirection = (Ship.transform.position - transform.position).normalized;
        Debug.Log("Is attacking");
        rb.isKinematic = false;
        rb.AddForce(attackDirection * attackForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(1.5f);
        rb.isKinematic = true;
        aiPath.enabled = true;
        destinationSetter.enabled = true;

        yield return new WaitForSeconds(attackDelayTime);
        GameObject Target = Ship.GetComponent<ShipController>().SpawnAndGetTarget(TargetPrefab);
        destinationSetter.target = Target.transform;
        reachedShip = false;
    }
}
