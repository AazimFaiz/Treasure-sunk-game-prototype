using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineManager : MonoBehaviour
{
    private static CoroutineManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        instance = this;
    }

    public static Coroutine Start(IEnumerator coroutine)
    {
        return instance.StartCoroutine(coroutine);
    }
}
