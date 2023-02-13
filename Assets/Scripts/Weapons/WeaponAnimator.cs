using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimator : MonoBehaviour
{

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void StartAttackAnim()
    {
        animator.SetTrigger("attack");
        Player.player.isAttacking = true;
    }
}
