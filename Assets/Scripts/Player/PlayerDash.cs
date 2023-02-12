using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    #region Variables
    // Dash Params
    public float dashingPower = 24f,
                 dashingAccel = 1f,
                 dashingTime = 0.2f,
                 dashingCooldown = 1f;

    // Others
    TrailRenderer tr;
    #endregion

    #region LifeCycle
    void Start()
    {
        tr = GetComponent<TrailRenderer>();
        if (!tr) Player.player.canDash = false;
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
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.LeftShift)) && Player.player.canDash) StartCoroutine(Dash());
    }
    #endregion

    #region Corrutines
    IEnumerator Dash()
    {
        // Setting player state
        Player.player.playerAnim.PlayDash();
        Player.player.canDash = false;
        Player.player.canAttack = false;
        Player.player.isDashing = true;
        Player.player.isinvincible = true;
        

        // Dash
        Player.player.rb.velocity = Player.player.lookingDir * dashingPower;
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);

        // CoolDown
        Player.player.playerAnim.EndDash();
        tr.emitting = false;
        Player.player.isDashing = false;
        Player.player.isinvincible = false;
        Player.player.canAttack = true;
        yield return new WaitForSeconds(dashingCooldown);

        // Reset
        Player.player.canDash = true;
    }
    #endregion
}
