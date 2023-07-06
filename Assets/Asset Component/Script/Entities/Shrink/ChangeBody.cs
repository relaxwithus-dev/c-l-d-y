using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBody : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            EventHandler.CallChangeBodyEvent();
        }
    }
}
