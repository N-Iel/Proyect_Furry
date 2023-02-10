using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    #region Variables
    // Dash Params
    public float dashingPowerMax = 24f,
                 dashingAccel = 0.1f,
                 dashingDeccel = 0.1f,
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
        Player.player.canDash = false;
        Player.player.isDashing = true;
        Player.player.isinvincible = true;

        // Accelerate
        float dashingPower = 0.1f;
        while(dashingPower < dashingPowerMax)
        {
            dashingPower *= dashingAccel * Time.deltaTime;
        }
        Player.player.rb.velocity = Player.player.lookingDir * dashingPower;
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);

        while (dashingPower > 0)
        {
            dashingPower /= dashingDeccel * Time.deltaTime;
        }
        // CoolDown
        tr.emitting = false;
        Player.player.isDashing = false;
        Player.player.isinvincible = false;
        yield return new WaitForSeconds(dashingCooldown);

        // Reset
        Player.player.canDash = true;
    }
    #endregion
}
