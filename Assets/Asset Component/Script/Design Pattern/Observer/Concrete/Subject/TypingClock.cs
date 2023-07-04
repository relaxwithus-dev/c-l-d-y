using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class TypingClock : TypingBase
{
    #region Variable

    [Header("Clock Component")]
    // Ref typing masi belum dipake
    // [SerializeField] private TypingFirstItem firstItem;
    // [SerializeField] private TypingSecondItem secondItem;
    
    [SerializeField] private char[] clockWordLetters;
    private char[] keyClockLetters;
    private string[] keyCodeNumber;
    private string keyCodeInput;
    
    public bool GotItem {get; private set;}
    public NotifyComponent notifyComponent;
    
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
                    ItemFlow();
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
    
    private void ItemFlow()
    {
        // if (firstItem.GotItem & secondItem.GotItem)
        // {
        //     NotifyObservers(ItemAction.ItemThree);
        //     GotItem = true;
        // }
        // else
        // {
        //     StartCoroutine(SetDefaultTyping());
        // }
        
        Debug.Log("Check item ya bang");
        GotItem = true;
    }

    private IEnumerator SetDefaultTyping()
    {
        notifyComponent.notifyObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        
        notifyComponent.notifyObject.GetComponent<Animator>().SetTrigger("Close");
        IsCorrect = false;
        letterIndex = 0;
        StartTextColors();
        yield return new WaitForSeconds(0.2f);
        
        notifyComponent.notifyObject.SetActive(false);
    }

    #endregion
}