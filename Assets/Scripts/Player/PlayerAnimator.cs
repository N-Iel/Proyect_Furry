using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

    // Animations
    Animator animator;
    PlayerSound playerS;
    Shadow shadow;
    public ParticleSystem dashParticle;

    private void Awake()
    {
        animator= GetComponent<Animator>();
        playerS = GetComponent<PlayerSound>();
        shadow = GetComponent<Shadow>();
    }

    // Update is called once per frame
    void Update()
    {
        MovementAnim();
    }

    void MovementAnim()
    {
        animator.SetBool("isMoving", Player.player.isMoving);
    }

    public void PlayDash()
    {
        Debug.Log("dashAnim");
        shadow.enabled = true;
        playerS.PlayDash(); // Sound
        if (dashParticle) dashParticle.Play(); // Particle
        animator.SetBool("isDashing", true);
    }

    public void EndDash()
    {
        shadow.enabled = false;
        animator.SetBool("isDashing", false);
    }
}
