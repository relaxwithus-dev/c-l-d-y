using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Serialization;

// Bisa direfactor lagi se iki
// Soon lor

public class TypingClock : TypingBase
{
    #region Variable

    [Header("Clock Component")]
    [SerializeField] private string temporaryWords;
    // Ref typing masi belum dipake
    // [SerializeField] private TypingFirstItem firstItem;
    // [SerializeField] private TypingSecondItem secondItem;
    
    private char[] clockWordLetters;
    private char[] keyClockLetters;
    private string[] keyCodeNumber;
    private string keyCodeInput;
    
    public bool GotItem {get; private set;}
    public NotifyComponent notifyComponent;
    
    #endregion

    #region MonoBehaviour Callbacks

    private void Start()
    {
        InteractStart();
        GotItem = false;
        anyWordsTextUI.text = temporaryWords;
        notifyComponent.notifyTextUI.text = notifyComponent.notifyText;
        
        clockWordLetters = temporaryWords.ToCharArray();
        keyClockLetters = anyWords.ToCharArray();
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
                // Skip the colon
                letterIndex++;
            }
            
            if (NumericsInputChecker(keyCodeInput))
            {
                var keyCodeChar = keyCodeInput.ToCharArray()[keyCodeInput.Length - 1];
                clockWordLetters[letterIndex] = keyCodeChar;
                
                var stringBuilder = new StringBuilder(anyWordsTextUI.text);
                stringBuilder[letterIndex] = keyCodeChar;
                anyWordsTextUI.text = stringBuilder.ToString();

                characterColors[letterIndex] = Color.blue;
                letterIndex++;
                playerTyping.SetCodeTextNull();
            }
           
        }
        else
        {
            if (LetterCorrectChecker())
            {
                IsCorrect = true;
                ItemFlow();
            }
            else
            {
                StartCoroutine(SetWrongText());
            }
        }
        
        UpdateTextColors();
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

    private bool LetterCorrectChecker()
    {
        for (int i = 0; i < clockWordLetters.Length; i++)
        {
            if (clockWordLetters[i] != keyClockLetters[i])
            {
                return false;
            }
        }
        
        return true;
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
        
        Debug.Log("DONN TEMPE GORENG NIKMATT");
        GotItem = true;
    }

    private IEnumerator SetDefaultText()
    {
        notifyComponent.notifyObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        
        notifyComponent.notifyObject.GetComponent<Animator>().SetTrigger("Close");
        IsCorrect = false;
        letterIndex = 0;
        // Some logic for reset UI number typing
        
        yield return new WaitForSeconds(0.2f);
        
        notifyComponent.notifyObject.SetActive(false);
    }

    private IEnumerator SetWrongText()
    {
        WrongTextColors();
        letterIndex = 0;
        yield return new WaitForSeconds(1f);
        
        StartTextColors();
        Debug.Log("Reset");
    }
    
    #endregion
}