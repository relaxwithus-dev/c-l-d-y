using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Require Component

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerTyping))]

#endregion

public class PlayerController : SingletonMonobehaviour<PlayerController>
{
    [Header("Scriptable Object Component")]
    [SerializeField] private PlayerData playerDataSO;

    [Header("Movement Component")]
    [SerializeField] private Vector2 playerDirection;
    private bool isRight = true;
    private bool isPlayerMovementDisabled = false;

    [Header("Reference")]
    private Rigidbody2D myRb;
    private Animator myAnim;
    private BoxCollider2D myCollider;
    private Vector2 normalColSize;
    private Vector2 shrinkColSize;
    private Vector2 normalColOffset;
    private Vector2 shrinkColOffset;
    private bool isShrink = false;
    public bool IsShrink { get => isShrink; set => isShrink = value; }


    [Header("Shrink")]
    [SerializeField] private RuntimeAnimatorController animNormal;
    [SerializeField] private RuntimeAnimatorController animShrink;


    #region MonoBehaviour Callbacks

    protected override void Awake()
    {
        base.Awake();
        myRb = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        myCollider = GetComponent<BoxCollider2D>();
    }

    void OnEnable()
    {
        EventHandler.ChangeBodyEvent += ChangeBody;
        EventHandler.ShrinkEvent += Shrink;
    }

    void OnDisable()
    {
        EventHandler.ChangeBodyEvent -= ChangeBody;
        EventHandler.ShrinkEvent -= Shrink;
    }

    private void Start()
    {
        gameObject.name = playerDataSO.playerName;
        normalColSize = new Vector2(0.67f, 0.2f); //the value determined by the sprite size
        shrinkColSize = new Vector2(0.67f, 0.08f); //the value determined by the sprite size
        normalColOffset = new Vector2(myCollider.offset.x, 0.085f); //the value determined by the sprite size
        shrinkColOffset = new Vector2(myCollider.offset.x, 0.025f); //the value determined by the sprite size

        myAnim.runtimeAnimatorController = animNormal;
    }

    // Physics Update
    private void FixedUpdate()
    {
        // if (!DialogueManager.Instance.dialogueIsPlaying)
        // {
        //     PlayerMovement();
        //     PlayerDirection();
        //     PlayerAnimation();
        // }

        if (isPlayerMovementDisabled == false)
        {
            PlayerMovement();
            PlayerDirection();
            PlayerAnimation();
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
            myAnim.SetBool("isWalk", true);
        }
        else
        {
            myAnim.SetBool("isWalk", false);
        }
    }

    public void PlayerFlip()
    {
        isRight = !isRight;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
    }

    #endregion

    private void ChangeBody()
    {
        if (isShrink == false)
        {
            myAnim.runtimeAnimatorController = animShrink;
            myCollider.size = shrinkColSize;
            myCollider.offset = shrinkColOffset;

            StartCoroutine(Shrinked());
        }
        else
        {
            myAnim.runtimeAnimatorController = animNormal;
            myCollider.size = normalColSize;
            myCollider.offset = normalColOffset;

            StartCoroutine(Shrinked());
        }
    }

    private void Shrink(Vector3 target, float moveDuration)
    {
        StartCoroutine(NormalAndSmall(target, moveDuration));
    }

    IEnumerator NormalAndSmall(Vector3 target, float moveDuration)
    {
        Vector3 startPosition = transform.position;
        float timeElapsed = 0f;

        while (timeElapsed < 1)
        {
            transform.position = Vector3.Lerp(startPosition, target, timeElapsed);
            timeElapsed += Time.deltaTime / moveDuration;

            yield return new WaitForEndOfFrame();
        }

        transform.position = target;
    }

    IEnumerator Shrinked()
    {
        yield return new WaitForSeconds(2f);
        if (!isShrink)
        {
            isShrink = true;
        }
        else
        {
            isShrink = false;
        }
    }

}
