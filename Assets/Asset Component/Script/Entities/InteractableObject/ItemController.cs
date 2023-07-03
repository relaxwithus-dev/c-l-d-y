using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    [Header("Item Controller Component")]
    [SerializeField] private string itemName;
    [SerializeField] private float moveTime;
    // [SerializeField] private GameObject itemDestroyEffect;
    private Transform playerTransform;

    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Start()
    {
        gameObject.name = itemName;
    }

    private void Update()
    {
        StartCoroutine(ItemMove());
    }

    private IEnumerator ItemMove()
    {
        yield return new WaitForSeconds(1f);
        transform.position = Vector2.MoveTowards(transform.position, playerTransform.position,
            moveTime * Time.deltaTime);

        if (Vector2.Distance(transform.position, playerTransform.position) < 0.5f)
        {
            // Effect pas object mlebu nde player
            // Instantiate(itemDestroyEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
