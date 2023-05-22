using System;
using UnityEngine;
using TMPro;

public class TypingFirstItem : TypingBase
{
    #region First Item Component

    public bool GotItem {get; private set;}

    #endregion

    #region MonoBehaviour Callbacks

    private void Start()
    {
        InteractStart();
        GotItem = false;
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

    // Typing Mechanics Without Same Character Detector
    private void TypingMechanic()
    {
        var letter = anyWords.ToCharArray();
        
        if (IsCorrect)
        {
            return;
        }
        
        if (letterIndex < letter.Length)
        {
            if (letter[letterIndex].ToString() == playerTyping.CodeText)
            {
                characterColors[letterIndex] = Color.blue;
                letterIndex++;
            }
            UpdateTextColors();
        }
        else
        {
            IsCorrect = true;
            NotifyObservers(ItemAction.ItemOne);
            GotItem = true;
        }
    }

    #endregion

    #region Collider2D Callbacks

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