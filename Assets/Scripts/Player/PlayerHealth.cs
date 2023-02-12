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
                 invicibleTime = 1.0f;

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
        StartCoroutine(Invincible());
        shield -= _dmg;
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
            yield return new WaitForSeconds(repairTimeDelay);
        }
        isRecovering = false;
    }

    IEnumerator Invincible()
    {
        Player.player.isinvincible = true;
        Debug.Log("Is invincible");
        yield return new WaitForSeconds(invicibleTime);
        Player.player.isinvincible = false;
        Debug.Log("No longer invincible");
    }
    #endregion

    #region colliders

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !Player.player.isinvincible) Hit(1.0f);
    }

    #endregion
}
