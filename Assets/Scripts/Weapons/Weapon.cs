using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Weapon : MonoBehaviour
{

    public float coolDownTime = 0.5f,
                 attackCost = 0.2f,
                 dmg = 1.0f;
    float coolDownCounter;
    public static Weapon weapon;
    public Collider2D attackCollider;
    WeaponAnimator weaponAnim;
    SpriteRenderer weaponSprite;

    private void Awake()
    {
        weapon = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        weaponAnim = GetComponent<WeaponAnimator>();
        weaponSprite = GetComponent<SpriteRenderer>();
        coolDownCounter = coolDownTime;
    }

    // Update is called once per frame
    void Update()
    {
        coolDownCounter += Time.deltaTime;

        CheckExhaustion();

        if (Player.player.canAttack && !Player.player.isExhausted && !Player.player.isAttacking && !Player.player.isDashing && coolDownCounter >= coolDownTime)
            AttackInput();
    }



    void CheckExhaustion()
    {
        if(Player.player.isExhausted)
            weaponSprite.enabled = false;
        else
            weaponSprite.enabled = true;
    }

    void AttackInput()
    {
        if (Input.GetMouseButton(0))
        {
            weaponAnim.StartAttackAnim();
            Player.player.health.ReduceShield(attackCost);
        }
    }

    public void EnableAttackCollider()
    {
        attackCollider.enabled = true;
    }

    public void DisableAttackCollider()
    {
        attackCollider.enabled = false;
        Player.player.isAttacking = false;
        coolDownCounter = 0;
    }
}
