using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using TMPro;

public class TypingClock : TypingBase
{
    #region Variable

    [Header("Clock Component")]
    [SerializeField] private char[] clockWordLetters;
    private char[] keyClockLetters;
    private string[] keyCodeNumber;
    private string keyCodeInput;
    
    [SerializeField] private string notifyText;
    [SerializeField] private GameObject notifyObject;
    [SerializeField] private TextMeshProUGUI notifyTextUI;
    
    #endregion

    #region MonoBehaviour Callbacks

    private void Start()
    {
        IsCorrect = false;
        letterIndex = 0;

        clockWordLetters = keyClockLetters = anyWords.ToCharArray();
        keyCodeNumber = new string[10]
        {
            "Alpha0", "Alpha1", 
            "Alpha2", "Alpha3", 
            "Alpha4", "Alpha5", 
            "Alpha6", "Alpha7", 
            "Alpha8", "Alpha9"
        };
        
    }

    private void Update()
    {
        keyCodeInput = playerTyping.CodeText;
        
        if (!isTypingArea)
        {
            return;
        }
        
        TypingClockMechanic();
    }

    #endregion

    #region Tsukuyomi Callbacks
    
    private void TypingClockMechanic()
    {
        if (IsCorrect)
        {
            return;
        }
        
        if (letterIndex < clockWordLetters.Length)
        {
            if (clockWordLetters[letterIndex] == ':')
            {
                //Skip Letter
                Debug.Log("Letter Skip");
                letterIndex++;
            }
            
            if (NumericsInputChecker(keyCodeInput))
            {
                var keyCodeChar = keyCodeInput.ToCharArray()[keyCodeInput.Length - 1];

                clockWordLetters[letterIndex] = keyCodeChar;
                anyWordsText.text = anyWordsText.text.Replace(clockWordLetters[letterIndex].ToString(), 
                    keyCodeChar.ToString());
                letterIndex++;
                playerTyping.SetCodeTextNull();
            }
        }
        else
        {
            for (int i = 0; i < clockWordLetters.Length; i++)
            {
                if (clockWordLetters[i] == keyClockLetters[i])
                {
                    IsCorrect = true;
                    Debug.Log("Betul Joss");
                }
                else
                {
                    IsCorrect = false;
                    Debug.Log("Apalah");
                    letterIndex = 0;
                }
            }
        }
    }

    private bool NumericsInputChecker(string keycode)
    {
        for (int i = 0; i < keyCodeNumber.Length; i++)
        {
            if (keycode == keyCodeNumber[i])
            {
                return true;
            }
        }

        return false;
    }

    #endregion
}