using System;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class TypingGetItem : TypingObjectBase
{
    [Header("Get Component")]
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private GameObject itemPrefabs;
    [field: SerializeField] public bool IsGetItem { get; private set; }

    #region MonoBehaviour Callbacks
    
    private void OnEnable()
    {
        OnInteractDone += InteractDone;
    }
    
    private void OnDisable()
    {
        OnInteractDone -= InteractDone;
    }
    
    private void Start()
    {
        InteractStart();
    }

    private void Update()
    {
        if (!isTypingArea)
        {
            return;
        }
        
        TypingMechanic();
    }

    #endregion

    #region Another Methods
    
    public override void InteractStart()
    {
        base.InteractStart();
        // Some Logic
        
        IsGetItem = false;
    }

    public override void InteractDone()
    {
        base.InteractDone();
        // Some Logic
        
        IsGetItem = true;
        Instantiate(itemPrefabs, spawnPosition.position, Quaternion.identity);
    }

    #endregion

    #region Trigger Methods

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isTypingArea = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isTypingArea = false;
        }
    }

    #endregion

    
}