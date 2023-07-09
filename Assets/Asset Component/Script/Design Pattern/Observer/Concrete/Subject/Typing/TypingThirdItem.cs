using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class TypingThirdItem : TypingBase
{
    #region Third Item Component
    
    [Header("Third Item Component")]
    [SerializeField] private TypingFirstItem firstItem;
    [SerializeField] private TypingSecondItem secondItem;
    public bool GotItem {get; private set;}
    public NotifyComponent notifyComponent;

    #endregion


    #region MonoBehaviour Callbacks

    private void Start()
    {
        WordsUpperChecker();
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
        if (firstItem.GotItem & secondItem.GotItem)
        {
            NotifyObservers(ItemAction.ItemThree);
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