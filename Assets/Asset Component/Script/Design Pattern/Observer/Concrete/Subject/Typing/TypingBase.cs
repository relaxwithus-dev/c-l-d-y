using System;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

public class TypingBase : ObserverSubject
{
    #region Variable
    
    [Header("Typing Component")]
    public string anyWords;
    public int letterIndex { get; set; }
    public bool isTypingArea { get; set; }
    public bool IsCorrect { get; set; }
    
    [Header("UI Component")]
    public TextMeshProUGUI anyWordsTextUI;
    public Color[] characterColors { get; private set; }
    
    // Reference
    public PlayerTyping playerTyping {get; private set;}

    #endregion

    #region Some Struct
    
    [Serializable]
    public struct NotifyComponent
    {
        public string notifyText;
        public GameObject notifyObject;
        public TextMeshProUGUI notifyTextUI;
    }
    
    #endregion
    
    #region MonoBehaviour Callbacks
    
    private void Awake()
    {
        playerTyping = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerTyping>();
    }
    
    #endregion

    #region Relax With Us Callbacks
    
    public void InteractStart()
    {
        letterIndex = 0;
        IsCorrect = false;
        anyWordsTextUI.text = anyWords;
        
        StartTextColors();
    }

    public void StartTextColors()
    {
        characterColors = new Color[anyWordsTextUI.text.Length];
        for (int i = 0; i < characterColors.Length; i++)
        {
            characterColors[i] = Color.black;
        }
    }

    public void WrongTextColors()
    {
        characterColors = new Color[anyWordsTextUI.text.Length];
        for (int i = 0; i < characterColors.Length; i++)
        {
            characterColors[i] = Color.red;
        }
    }
    
    public void UpdateTextColors()
    {
        for (int i = 0; i < anyWordsTextUI.text.Length; i++)
        {
            TMP_CharacterInfo charInfo = anyWordsTextUI.textInfo.characterInfo[i];
            int materialIndex = charInfo.materialReferenceIndex;
            int vertexIndex = charInfo.vertexIndex;

            if (charInfo.isVisible)
            {
                anyWordsTextUI.textInfo.meshInfo[materialIndex].colors32[vertexIndex + 0] = characterColors[i];
                anyWordsTextUI.textInfo.meshInfo[materialIndex].colors32[vertexIndex + 1] = characterColors[i];
                anyWordsTextUI.textInfo.meshInfo[materialIndex].colors32[vertexIndex + 2] = characterColors[i];
                anyWordsTextUI.textInfo.meshInfo[materialIndex].colors32[vertexIndex + 3] = characterColors[i];
            }
        }

        // Memperbarui tampilan teks
        anyWordsTextUI.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
    }

    public void WordsUpperChecker()
    {
        var tempWords = anyWords.ToCharArray();
        for (int i = 0; i < tempWords.Length; i++)
        {
            if (tempWords[i] >= 'A' && tempWords[i] <= 'Z')
            {
                return;
            }
        }
        
        anyWords = anyWords.ToUpper();
    }
    
    #endregion

    #region Collider Callbacks
    
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