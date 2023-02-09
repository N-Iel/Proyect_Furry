using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMelee : MonoBehaviour
{
    public float dmg = 1.0f,
                 duration = 0.1f,
                 cooldownTime = 0.5f,
                 range = 1.0f;
    public Transform attackPoint;
    public LayerMask enemyLayer;
    float cooldownTimer;
    bool isAttacking = false; 

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Timer", 1.0f, 1.0f);
        cooldownTimer = cooldownTime;
    }

    // Update is called once per frame
    void Update()
    {
        AttackInputs();

        cooldownTimer += Time.deltaTime;

        if (isAttacking || cooldownTimer > cooldownTime)
            EnableAttackArea();
    }

    void Timer()
    {
        Debug.Log(cooldownTimer);
    }

    void AttackInputs()
    {
        isAttacking = Input.GetMouseButton(0);
    }

    void EnableAttackArea()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, range, enemyLayer);

        foreach(Collider2D enemy in hitEnemies)
        {
            Debug.Log("enemy hitted");
        }

        cooldownTimer = 0;
    }

    private void OnDrawGizmos()
    {

        Gizmos.DrawWireSphere(attackPoint.position, range);
    }
}
