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
        if (!Player.player.isinvincible && shield < maxShield && !isRecovering) StartCoroutine(RecoverShield());
        UpdateShieldBar();
    }
    #endregion

    #region Methods
    void Hit(float _dmg)
    {
        if (isRecovering)
        {
            StopCoroutine(RecoverShield());
            isRecovering = false;
        }

        // Dead check
        if ( shield < 1 ) { Death(); return; }

        StartCoroutine(Invincible());
        shield = Mathf.Floor(shield);
        shield -= _dmg;
        Debug.Log(shield);
        // CanAttack check
        Player.player.canAttack = shield < 1 ? false : true;
    }
    
    void Death()
    {
        Player.player.canAttack = false;
        Player.player.canDash = false;
        Player.player.canMove = false;
        Player.player.rb.velocity = Vector3.zero;
        Debug.Log("Game over");
    }

    void UpdateShieldBar()
    {
        shieldBar.fillAmount = shield / maxShield;
    }
    #endregion

    #region Corrutines

    IEnumerator RecoverShield()
    {
        isRecovering = true;
        while (isRecovering)
        {
            Debug.Log("recovering");
            shield = Mathf.Clamp(shield + repairRate, 0, maxShield);
            if (Player.player.canAttack = shield < 1 ? false : true);
            yield return new WaitForSeconds(repairTimeDelay);
        }
        isRecovering = false;
    }

    IEnumerator Invincible()
    {
        Player.player.isinvincible = true;
        shieldAnim.enabled = true;

        yield return new WaitForSeconds(invincibleWarning);

        shieldAnim.SetTrigger("fadeOut");

        yield return new WaitForSeconds(invicibleTime - invincibleWarning);

        Player.player.isinvincible = false;
        shieldAnim.enabled = false;
    }
    #endregion

    #region colliders

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Update with monster dmg
        if (collision.gameObject.CompareTag("Enemy") && !Player.player.isinvincible) Hit(1.0f);
    }

    #endregion
}
