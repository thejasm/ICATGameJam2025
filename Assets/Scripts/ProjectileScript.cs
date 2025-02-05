using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public Vector2 direction;
    public float speed;
    private float maxDistance = 50f;
    private Vector3 startPosition;

    void Start() {
        startPosition = transform.position;
    }


    void Update() {
        transform.Translate(direction * speed * Time.deltaTime);

        if(Vector3.Distance(startPosition, transform.position) > maxDistance) {
            Destroy(gameObject);
        }
    }
}
