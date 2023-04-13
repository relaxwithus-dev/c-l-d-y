using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Scriptable Object Component")]
    [SerializeField] private PlayerData playerDataSO;
    
    [Header("Movement Component")]
    [SerializeField] private Vector2 playerDirection;

    [Header("Reference")] 
    private Rigidbody2D myRb;
    private Animator myAnim;

    #region MonoBehaviour Callbacks

    private void Awake()
    {
        myRb = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
    }
    
    void Start()
    {
        gameObject.name = playerDataSO.playerName;
    }
    
    // Physics Update
    private void FixedUpdate()
    {
        PlayerMovement();
       
    }
    
    // Update is called once per frame
    void Update()
    {
        PlayerAnimation();
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
        myRb.velocity =  playerDirection * playerDataSO.playerSpeed;
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

    #endregion

}
