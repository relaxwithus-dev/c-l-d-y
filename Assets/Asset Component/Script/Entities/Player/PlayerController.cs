using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Require Component

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Animator))]

#endregion
public class PlayerController : MonoBehaviour
{
    [Header("Scriptable Object Component")]
    [SerializeField] private PlayerData playerDataSO;

    [Header("Movement Component")]
    [SerializeField] private Vector2 playerDirection;
    [SerializeField] private bool isRight;

    [Header("Reference")]
    private Rigidbody2D myRb;
    private Animator myAnim;

    #region MonoBehaviour Callbacks

    private void Awake()
    {
        myRb = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
    }

    private void Start()
    {
        gameObject.name = playerDataSO.playerName;
    }

    // Physics Update
    private void FixedUpdate()
    {
        if (!DialogueManager.Instance.dialogueIsPlaying)
        {
            PlayerMovement();
            PlayerDirection();
            // PlayerAnimation();
        }

    }

    // Logic Update
    private void Update()
    {

    }

    #endregion

    #region Movement Method

    private void PlayerMovement()
    {
        float moveX, moveY;
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");

        playerDirection = new Vector2(moveX, moveY);
        playerDirection.Normalize();
        myRb.velocity = playerDirection * playerDataSO.playerSpeed;
    }

    private void PlayerDirection()
    {
        if (playerDirection.x > 0 && !isRight)
        {
            PlayerFlip();
        }

        if (playerDirection.x < 0 && isRight)
        {
            PlayerFlip();
        }
    }

    private void PlayerAnimation()
    {
        if (playerDirection != Vector2.zero)
        {
            myAnim.SetFloat("Horizontal", playerDirection.x);
            myAnim.SetFloat("Vertical", playerDirection.y);
            myAnim.SetBool("isWalk", true);
        }
        else
        {
            myAnim.SetBool("isWalk", false);
        }
    }

    private void PlayerFlip()
    {
        isRight = !isRight;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    #endregion

}
