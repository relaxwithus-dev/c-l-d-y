using System;
using UnityEngine;

public class TypingDummy : TypingObjectBase
{
    [Header("Got Item Reference")]
    public GameObject referencePanel;

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
        ColliderChecker();
        
        if (isTypingArea)
        {
            TypingMechanic();
        }
        
        // TypingMechanic();
    }
    
    #endregion

    #region Another Methods
    
    public override void InteractStart()
    {
        base.InteractStart();
        // Some Logic
        
        referencePanel.SetActive(false);
        Debug.Log("Typing Got Item Start");
    }

    public override void InteractDone()
    {
        base.InteractDone();
        // Some Logic
        
        referencePanel.SetActive(true);
        Debug.Log("Typing Got Item Done");
    }
    
    #endregion
    
}