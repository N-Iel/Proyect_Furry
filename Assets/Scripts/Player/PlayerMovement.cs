using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Variables
    public float speed;
    Vector2 movement;
    #endregion

    #region LifeCycle
    void Start() { }

    void Update()
    {
        if (!Player.player.canMove && Player.player.isDashing) return;

        MovementInput();
        UpdateLookingDir();
    }

    void FixedUpdate()
    {
        if ((!Player.player.canMove || Player.player.isDashing))
        {
            Player.player.isMoving = false;
            return;
        }

        Move();
    }
    #endregion

    #region Movement
    void MovementInput()
    {
        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }

    void Move()
    {
        Player.player.rb.MovePosition( Player.player.rb.position + movement * speed * Time.deltaTime);
        Player.player.isMoving = movement != Vector2.zero ? true : false;
    }
    #endregion

    #region Utils
    void UpdateLookingDir()
    {
        if(movement != Vector2.zero) Player.player.lookingDir = movement;
    }
    #endregion
}
