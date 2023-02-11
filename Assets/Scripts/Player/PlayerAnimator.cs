using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

    // Animations
    Animator animator;
    PlayerSound playerS;
    public ParticleSystem dashParticle;

    private void Awake()
    {
        animator= GetComponent<Animator>();
        playerS = GetComponent<PlayerSound>();
    }

    // Update is called once per frame
    void Update()
    {
        MovementAnim();
        DashAnim();
    }

    void MovementAnim()
    {
        animator.SetBool("isMoving", Player.player.isMoving);
    }

    void DashAnim()
    {
        if (!animator.GetBool("isDashing") && Player.player.isDashing)
        {
            playerS.PlayDash();
            dashParticle.Play();
            Debug.Log("Dash effects triggered");
        }
        animator.SetBool("isDashing", Player.player.isDashing);
    }
}
