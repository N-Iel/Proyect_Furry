using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

    // Animations
    public Animator animator;

    // Update is called once per frame
    void Update()
    {
        MovementAnim();
    }

    void MovementAnim()
    {
        animator.SetBool("isMoving", Player.player.isMoving);
    }
}
