using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyAnimController : MonoBehaviour {
    public Transform spriteChild;
    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 lastMoveDirection = Vector2.zero;

    void Start() {
        animator = spriteChild.GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        if(animator == null || rb == null) return;

        Vector2 velocity = rb.velocity;
        Vector2 normalizedVelocity = velocity.normalized;
        float moveMagnitude = velocity.magnitude;

        animator.SetFloat("MoveX", normalizedVelocity.x);
        animator.SetFloat("MoveY", normalizedVelocity.y);
        animator.SetFloat("MoveMagnitude", moveMagnitude);
        if(moveMagnitude > 0.1f)
        {
            lastMoveDirection = normalizedVelocity;
        }
        if(moveMagnitude < 0.1f && lastMoveDirection != Vector2.zero)
        {
            animator.SetFloat("LastMoveX", lastMoveDirection.x);
            animator.SetFloat("LastMoveY", lastMoveDirection.y);
        }

        if(velocity.x < 0) {
            FlipSprite(false);
        } else if(velocity.x > 0) {
            FlipSprite(true);
        }
    }

    void FlipSprite(bool flip) {
        if(spriteChild != null) {
            spriteChild.localScale = new Vector3(flip ? -1 : 1, 1, 1);
        }
    }
}
