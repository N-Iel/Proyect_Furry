using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Weapon : MonoBehaviour
{

    public float coolDownTime = 0.5f,
                 dmg = 1.0f;
    float coolDownCounter;
    public static Weapon weapon;
    public Collider2D attackCollider;
    WeaponAnimator weaponAnim;

    private void Awake()
    {
        weapon = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        weaponAnim = GetComponent<WeaponAnimator>();
        coolDownCounter = coolDownTime;
    }

    // Update is called once per frame
    void Update()
    {
        coolDownCounter += Time.deltaTime;

        if(!Player.player.isAttacking && Player.player.canAttack && coolDownCounter >= coolDownTime)
            AttackInput();
    }

    void AttackInput()
    {
        if (Input.GetMouseButton(0))
            weaponAnim.StartAttackAnim();
    }

    public void EnableAttackCollider()
    {
        attackCollider.enabled = true;
    }

    public void DisableAttackCollider()
    {
        attackCollider.enabled = false;
        Player.player.isAttacking = false;
        Player.player.canAttack = true;
        coolDownCounter = 0;
    }
}
