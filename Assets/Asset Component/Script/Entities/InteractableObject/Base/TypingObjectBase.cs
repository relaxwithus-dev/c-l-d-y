using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypingObjectBase : MonoBehaviour
{
    [Header("Base Typing Component")]
    public string anyWords;
    private int letterIndex;
    [HideInInspector] public bool isTypingArea;
    public bool IsCorrect { get; private set; }
    public event Action OnInteractDone;

    [Header("Base Typing UI Component")]
    public TextMeshProUGUI anyWordsText;
    private Color[] characterColors;

    [Header("Script Reference")]
    private PlayerTyping playerTyping;
    
    private void Awake()
    {
        playerTyping = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerTyping>();
    }

    public virtual void InteractStart()
    {
        // Default Logic
        letterIndex = 0;
        IsCorrect = false;
        anyWordsText.text = anyWords;
        
        StartTextColors();

        // Some Logic
    }

    public virtual void InteractDone()
    {
        // Some logic
        
    }
    
    // Typing Mechanics Without Same Character Detector
    public void TypingMechanic()
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
            OnInteractDone?.Invoke();
        }
    }

    private void StartTextColors()
    {
        characterColors = new Color[anyWordsText.text.Length];
        for (int i = 0; i < characterColors.Length; i++)
        {
            characterColors[i] = Color.black;
        }
    }
    
    private void UpdateTextColors()
    {
        for (int i = 0; i < anyWordsText.text.Length; i++)
        {
            TMP_CharacterInfo charInfo = anyWordsText.textInfo.characterInfo[i];
            int materialIndex = charInfo.materialReferenceIndex;
            int vertexIndex = charInfo.vertexIndex;

            if (charInfo.isVisible)
            {
                anyWordsText.textInfo.meshInfo[materialIndex].colors32[vertexIndex + 0] = characterColors[i];
                anyWordsText.textInfo.meshInfo[materialIndex].colors32[vertexIndex + 1] = characterColors[i];
                anyWordsText.textInfo.meshInfo[materialIndex].colors32[vertexIndex + 2] = characterColors[i];
                anyWordsText.textInfo.meshInfo[materialIndex].colors32[vertexIndex + 3] = characterColors[i];
            }
        }

        // Memperbarui tampilan teks
        anyWordsText.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
    }
}