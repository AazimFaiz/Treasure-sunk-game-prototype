using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    public GameObject target;
    public Vector3 posVelocity;
    public float rotVelocity;
    public float moveSmoothTime = 0.3f;
    public float rotSmoothTime = 0.3f;
    public float targetZ;
    //public bool isFollow = false;

    private Vector3 targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (isFollow)
        //{ 
        if (target != null)
        {
            targetPosition = new Vector3(target.transform.position.x, target.transform.position.y, targetZ);
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref posVelocity, moveSmoothTime);

            float newRotZ = Mathf.SmoothDampAngle(transform.rotation.eulerAngles.z,
                target.transform.rotation.eulerAngles.z, ref rotVelocity, rotSmoothTime);
            transform.rotation = Quaternion.Euler(0, 0, newRotZ);
        }
        //}
    }
}
