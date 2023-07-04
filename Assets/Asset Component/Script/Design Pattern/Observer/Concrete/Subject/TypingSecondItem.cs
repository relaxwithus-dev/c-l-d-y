using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class TypingSecondItem : TypingBase
{
    #region Second Item Component
    
    [Header("Second Item Component")]
    [SerializeField] private TypingFirstItem firstItem;
    public bool GotItem {get; private set;}
    public NotifyComponent notifyComponent;
    
    #endregion

    #region MonoBehaviour Callbacks

    private void Start()
    {
        InteractStart();
        GotItem = false;
        notifyComponent.notifyTextUI.text = notifyComponent.notifyText;
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
            ItemFlow();
        }
    }

    private void ItemFlow()
    {
        if (firstItem.GotItem)
        {
            NotifyObservers(ItemAction.ItemTwo);
            GotItem = true;
        }
        else
        {
            StartCoroutine(SetDefaultTyping());
        }
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