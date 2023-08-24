using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrink : MonoBehaviour
{
    public Transform targetRightPos;
    public float moveDuration = 0.5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (PlayerController.Instance.IsShrink == false)
            {
                EventHandler.CallShrinkEvent(targetRightPos.position, moveDuration);
            }
        }
    }
}
