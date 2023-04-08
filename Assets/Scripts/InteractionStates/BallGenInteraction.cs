using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BallGenInteraction : MonoBehaviour
{
    bool shouldSpawn = true;

    public float spawnDelayMin;
    public float spawnDelayMax;

    [HideInInspector]
    public GameObject CannonballPrefab;
    public GameObject Ship;
    public GameObject Player;

    GameObject currentCannonball;
    HoldBehaviour cannonballBehaviour;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(currentCannonball == null && shouldSpawn)
        {
            currentCannonball = Instantiate(CannonballPrefab, transform.position,
                CannonballPrefab.transform.rotation, transform.parent);
            cannonballBehaviour = currentCannonball.GetComponent<HoldBehaviour>();

            cannonballBehaviour.Ship = Ship;
            cannonballBehaviour.Player = Player;
            cannonballBehaviour.PlayerController = Player.GetComponent<PlayerStateController>();
            cannonballBehaviour.rb = currentCannonball.GetComponent<Rigidbody2D>();
            cannonballBehaviour.boxCollider = currentCannonball.GetComponent<BoxCollider2D>();
            cannonballBehaviour.TogglePhysics(false);
        }
        else if(currentCannonball != null && !cannonballBehaviour.inGen)
        {
            currentCannonball = null;
            cannonballBehaviour = null;
            StartCoroutine(SpawnDelay(Random.Range(spawnDelayMin, spawnDelayMax)));
        }
    }

    IEnumerator SpawnDelay(float delay)
    {
        shouldSpawn = false;
        yield return new WaitForSeconds(delay);
        shouldSpawn = true;
    }
}
