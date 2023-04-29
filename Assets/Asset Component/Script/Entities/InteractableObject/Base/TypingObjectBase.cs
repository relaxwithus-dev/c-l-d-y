using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypingObjectBase : MonoBehaviour
{
    [Header("Base Typing Component")]
    public string anyWords;
    [SerializeField] private int letterIndex;
    [field: SerializeField] public bool isCorrect { get; private set; }
    public event Action OnInteractDone;

    [Header("Base Typing UI Component")]
    public TextMeshProUGUI anyWordsText;
    private Color[] characterColors;
    
    [Header("Base Typing Area")]
    [SerializeField] private Vector2 typingAreaRadius;
    public LayerMask typingAreaLayer;
    private Collider2D[] colliderArea;
    public bool isTypingArea { get; private set; }

    [Header("Script Reference")]
    private PlayerTyping playerTyping;
    
    private void Awake()
    {
        playerTyping = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerTyping>();
    }

    public virtual void InteractStart()
    {
        // Some logic
        letterIndex = 0;
        isCorrect = false;
        anyWordsText.text = anyWords;
        
        characterColors = new Color[anyWordsText.text.Length];
        for (int i = 0; i < characterColors.Length; i++)
        {
            characterColors[i] = Color.black;
        }
    }

    public virtual void InteractDone()
    {
        // Some logic
        
    }
    
    // Typing Mechanics Without Same Character Detector
    public void TypingMechanic()
    {
        var letter = anyWords.ToCharArray();

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
            isCorrect = true;
            OnInteractDone?.Invoke();
            Debug.Log("Correct Lur");
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

    public void ColliderChecker()
    {
        colliderArea = Physics2D.OverlapBoxAll(transform.position, typingAreaRadius, typingAreaLayer);
    
        foreach (var player in colliderArea)
        {
            isTypingArea = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, typingAreaRadius);
    }
}