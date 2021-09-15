using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour {

    private Animator animator;
    private Vector2 swingTarget;
    private bool swinging;
    private bool right;

    void Start() {
        animator = GetComponent<Animator>();
    }

    void Update() {
        if (swinging) {
            transform.up = (swingTarget - (Vector2) transform.position).normalized;
            transform.Rotate(new Vector3(0, 0, right ? 50 :-50));
        } else {
            transform.up = Vector3.up;
        }
    }

    public void UpdateVelocity(float x, float y) {
        animator.SetFloat("xVel", Mathf.Abs(x));
        animator.SetFloat("yVel", y);

        if (x > 0 && !swinging) {
            animator.SetLayerWeight(1, 1);
            animator.SetLayerWeight(2, 0);
            right = true;
        } else if (x < 0 && !swinging) {
            animator.SetLayerWeight(1, 0);
            animator.SetLayerWeight(2, 1);
            right = false;
        }
    } 

    public void UpdateSliding(bool s) {
        animator.SetBool("sliding", s);
    }

    public void UpdateGroundedState(bool g) {
        animator.SetBool("grounded", g);
    } 

    public void PlayJump() {
        animator.SetTrigger("jump");
    }

    public void PlayDoubleJump() {
        animator.SetTrigger("doubleJump");
    }

    public void PlayStartSwing(Vector2 target) {
        animator.SetTrigger("startSwing");
        swingTarget = target;
        swinging = true;
    }

    public void PlayStopSwing() {
        animator.SetTrigger("stopSwing");
        swinging = false;
    }
}
