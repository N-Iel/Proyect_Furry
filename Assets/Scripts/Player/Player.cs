using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Variables
    [Header("General")]
    public bool isDead = false;
    public bool isInvincible = false;
    public bool isExhausted = false;

    [Header("Movement")]
    public bool canMove = true;
    public bool isMoving = false;
    public Vector2 lookingDir;
    Vector2 storedLookingDir;

    [Header("Dash")]
    public bool canDash = true;
    public bool isDashing = false;

    [Header("Attack")]
    public bool canAttack = true;
    public bool isAttacking = false;

    [Header("Model")]
    [SerializeField] GameObject playerModel;
    public PlayerAnimator playerAnim;

    public Rigidbody2D rb { get; private set; }
    public PlayerHealth health { get; private set; }
    public Collider2D playerCollider;
    public static Player player;
    #endregion

    #region LifeCycle
    void Awake()
    {
        player = this;
    }

    void Start()
    {
        // Initialization
        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<PlayerHealth>();
        lookingDir = Vector2.zero;
        storedLookingDir = Vector2.zero;

        // Prevent player from moving without rb
        if(!rb)
        {
            canMove = false;
            canDash = false;
        }
    }

    void Update()
    {
        if (storedLookingDir != lookingDir && Mathf.Sign(storedLookingDir.x) == Mathf.Sign(lookingDir.x * -1)) Flip(); 
    }
    #endregion

    #region Events
    // Flip the player model in order to follow the movement direcction
    public void Flip()
    {
        // Update storedDir
        storedLookingDir = lookingDir;

        // Flip Model
        Vector3 buffer = playerModel.transform.localScale;
        buffer.x = Mathf.Sign(storedLookingDir.x);
        playerModel.transform.localScale = buffer;
    }
    #endregion
}
