using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    #region variables
    public float maxShield = 3,
                 shield,
                 repairRate = 0.3f,
                 repairTimeDelay = 0.1f,
                 invicibleTime = 2.0f,
                 invincibleWarning = 1.5f;

    public Animator shieldAnim;

    bool isRecovering = false;

    public Image shieldBar;
    #endregion

    #region lifeCycle
    void Start()
    {
        shield = maxShield;
    }

    void Update()
    {
        if (!Player.player.isInvincible && shield < maxShield && !isRecovering) StartCoroutine(RecoverShield());
        UpdateShieldStatus();
    }
    #endregion

    #region Methods
    void Hit()
    {
        if (isRecovering)
        {
            StopCoroutine(RecoverShield());
            isRecovering = false;
        }

        // Dead check
        if ( shield < 1 && Player.player.isExhausted ) { Death(); return; }

        StartCoroutine(Invincible());
        shield = Mathf.Floor(shield);
        if( shield >= 1 ) shield -= 1;
    }
    
    void Death()
    {
        Player.player.canAttack = false;
        Player.player.canDash = false;
        Player.player.canMove = false;
        Player.player.rb.velocity = Vector3.zero;
    }

    void UpdateShieldStatus()
    {
        shieldBar.fillAmount = shield / maxShield;
        if (shield == 0) Player.player.isExhausted = true;
    }

    public void ReduceShield(float _amount)
    {
        shield -= _amount;
    }
    #endregion

    #region Corrutines

    IEnumerator RecoverShield()
    {
        isRecovering = true;
        while (isRecovering)
        {
            shield = Mathf.Clamp(shield + repairRate, 0, maxShield);
            if (Player.player.isExhausted && shield >= 1) Player.player.isExhausted = false;
            yield return new WaitForSeconds(repairTimeDelay);
        }
        isRecovering = false;
    }

    IEnumerator Invincible()
    {
        Player.player.isInvincible = true;
        shieldAnim.enabled = true;

        yield return new WaitForSeconds(invincibleWarning);

        shieldAnim.SetTrigger("fadeOut");

        yield return new WaitForSeconds(invicibleTime - invincibleWarning);

        Player.player.isInvincible = false;
        shieldAnim.enabled = false;
    }
    #endregion

    #region colliders

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Update with monster dmg
        if (collision.gameObject.CompareTag("Enemy") && !Player.player.isInvincible) Hit();
    }

    #endregion
}
