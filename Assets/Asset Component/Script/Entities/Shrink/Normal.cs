using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Normal : MonoBehaviour
{
    public Transform targetLeftPos;
    public float moveDuration = 0.5f;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (PlayerController.Instance.IsShrink == true)
            {
                EventHandler.CallShrinkEvent(targetLeftPos.position, moveDuration);
            }
        }
    }
}
