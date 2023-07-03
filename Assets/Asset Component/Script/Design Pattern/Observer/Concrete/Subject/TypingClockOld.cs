using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypingClockOld : ObserverSubject
{
    #region Variable

    [Header("Clock Component")] 
    [SerializeField] private string[] clockWords;
    [SerializeField] private TextMeshProUGUI[] clockTexts;
    
    [Space]
    [SerializeField] private int clockWordsIndex;
    [SerializeField] private bool isCorrect;
    private bool isClockArea;
    
    [Header("Reference")]
    private PlayerTyping playerTyping;

    #endregion

    #region MonoBehaviour Callbacks

    private void Awake()
    {
        playerTyping = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerTyping>();
    }

    private void Start()
    {
        isCorrect = false;
        isClockArea = false;
    }

    private void Update()
    {
        if (isClockArea)
        {
            ClockTypingMechanic();
        }
        
    }

    #endregion
  
    #region Tsukuyomi Methods
    
    // Typing Clock Mechanics
    private void ClockTypingMechanic()
    {
        if (isCorrect)
        {
            return;
        }

        if (clockWordsIndex < clockWords.Length)
        {
            // Bug Here
            if (NumericWords(playerTyping.CodeText))
            {
                clockWords[clockWordsIndex] = playerTyping.CodeText;
                clockTexts[clockWordsIndex].text = clockWords[clockWordsIndex];
                clockWordsIndex++;
            }
        }
        else
        {
            isCorrect = true;
            Debug.Log("Correct Lorem Ipsum");
        }
    }

    private bool NumericWords(string words)
    {
        // Bug Here
        foreach (var numeric in words)
        {
            if (!(numeric >= '0' && numeric <= '9'))
            {
                Debug.Log("Not Numeric");
                return false;
            }
        }

        Debug.Log("Numeric");
        return true;
    }
    
    #endregion

    #region Collider Callbacks

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isClockArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isClockArea = false;
        }
    }

    #endregion
}
