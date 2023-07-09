using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class PlayerTypingPrototype : MonoBehaviour
{
    [SerializeField] public string anyWords;
    private string codeText;
    public TextMeshProUGUI anyWordsText;
    
    [SerializeField] private int letterIndex;
    public bool isCorrect;
    
    protected List<KeyCode> activeInputs = new List<KeyCode>();
    private Color[] characterColors;

    private void Start()
    {
        letterIndex = 0;
        isCorrect = false;
        anyWordsText.text = anyWords;
        
        characterColors = new Color[anyWordsText.text.Length];
        for (int i = 0; i < characterColors.Length; i++)
        {
            characterColors[i] = Color.white;
        }
    }

    private void Update()
    {
        CheckAnyKey();
        TypingMechanic();
    }

    private void CheckAnyKey()
    {
        List<KeyCode> pressedInput = new List<KeyCode>();

        if (Input.anyKeyDown)
        {
            foreach (KeyCode code in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(code))
                {
                    activeInputs.Remove(code);
                    activeInputs.Add(code);
                    pressedInput.Add(code);

                    Debug.Log(code + " was pressed");
                    codeText = code.ToString();
                }
            }
        }
    }
    
    // Typing Mechanics With Same Character Detector
    private void TypingMechanics()
    {
        var letter = anyWords.ToCharArray();

        if (letterIndex < letter.Length)
        {
            if (letter[letterIndex].ToString() == codeText)
            {
                for (int i = 0; i < letter.Length; i++)
                {
                    if (letter[letterIndex] == letter[i])
                    {
                        if (letterIndex == i)
                        {
                            continue;
                        }
                        
                        Debug.Log($"Letternya adalah {letter[i]} ada yang kembar");
                        Debug.Log($"d index ke {i}");
                    }
                    else
                    {
                        Debug.Log("Letter aman");
                    }
                }
                // anyWordsText.text = anyWordsText.text.Replace(letter[letterIndex].ToString(), 
                //     "<color=#0045FF>" + letter[letterIndex] + "</color>");
                letterIndex++;
            }
        }
        else
        {
            isCorrect = true;
        }
    }
    
    // Typing Mechanics Without Same Character Detector
    private void TypingMechanic()
    {
        var letter = anyWords.ToCharArray();

        if (letterIndex < letter.Length)
        {
            if (letter[letterIndex].ToString() == codeText)
            {
                characterColors[letterIndex] = Color.blue;
                letterIndex++;
            }
            UpdateTextColors();
        }
        else
        {
            isCorrect = true;
            Debug.Log("Correct Lur");
        }
    }
    
    private void AnyEnter()
    {
        var letter = anyWords.ToCharArray();
        for (int i = 0; i < letter.Length; i++)
        {
            if (letter[1] == letter[i])
            {
                Debug.Log("Letter ke-1 dan ke-" + i + " sama!");
                Debug.Log($"Letternya adalah {letter[i]}");
            }
            else
            {
                Debug.Log("Letter aman");
            }
        }
    }
    
    void UpdateTextColors()
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
