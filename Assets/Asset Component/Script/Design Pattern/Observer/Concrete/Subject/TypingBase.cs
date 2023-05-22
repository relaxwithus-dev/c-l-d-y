using System;
using UnityEngine;
using TMPro;

public class TypingBase : ObserverSubject
{
    #region Main Component

    [Header("Typing Component")]
    public string anyWords;
    public int letterIndex { get; set; }
    public bool isTypingArea { get; set; }
    [field: SerializeField] public bool IsCorrect { get; set; }

    #endregion

    #region UI Component

    [Header("UI Component")]
    public TextMeshProUGUI anyWordsText;
    public Color[] characterColors { get; private set; }

    #endregion

    #region Reference

    public PlayerTyping playerTyping {get; private set;}

    #endregion
    
    private void Awake()
    {
        playerTyping = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerTyping>();
    }

    public void InteractStart()
    {
        letterIndex = 0;
        IsCorrect = false;
        anyWordsText.text = anyWords;
        
        StartTextColors();
    }

    public void StartTextColors()
    {
        characterColors = new Color[anyWordsText.text.Length];
        for (int i = 0; i < characterColors.Length; i++)
        {
            characterColors[i] = Color.black;
        }
    }
    
    public void UpdateTextColors()
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