using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    #region Variables
    // Dash Params
    public float dashingPower = 24f,
                 dashingTime = 0.2f,
                 dashingCooldown = 1f,
                 dashingCost = 0.8f;

    // Others
    #endregion

    #region LifeCycle
    void Start()
    {
        Player.player.canDash = true;
    }

    // Update is called once per frame
    void Update()
    {
        DashInput();
    }
    #endregion

    #region Methods
    void DashInput()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.LeftShift)) && Player.player.canDash && !Player.player.isExhausted) StartCoroutine(Dash());
    }
    #endregion

    #region Corrutines
    IEnumerator Dash()
    {
        // Update Player state
        Player.player.playerAnim.PlayDash();

        Player.player.canDash = false;
        Player.player.isDashing = true;

        Player.player.playerCollider.enabled = false;
        Player.player.isInvincible = true;

        Player.player.health.ReduceShield(dashingCost);


        // Dash
        Player.player.rb.velocity = Player.player.lookingDir * dashingPower;
        yield return new WaitForSeconds(dashingTime);

        // CoolDown
        Player.player.playerAnim.EndDash();
        Player.player.isDashing = false;
        Player.player.isInvincible = false;
        Player.player.playerCollider.enabled = true;
        yield return new WaitForSeconds(dashingCooldown);

        // Reset
        Player.player.canDash = true;
    }
    #endregion
}
