using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageNumberTimer : MonoBehaviour
{
    public float DestroyTime = 1f;
    public Vector3 offset = new Vector3(0, 0.7f, 0);
    public Vector3 randomizedOffset = new Vector3();
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Display");
        transform.localPosition += offset;
        randomizedOffset.x = Random.Range(-0.5f, 0.5f);
        randomizedOffset.y = Random.Range(-0.5f, 0.5f);
        transform.localPosition += randomizedOffset;

        Destroy(gameObject, DestroyTime);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
