using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerTyping : MonoBehaviour
{
     [SerializeField] public string anyWords;
    private string codeText;
    public TextMeshProUGUI anyWordsText;
    
    [SerializeField] private int letterIndex;
    public bool isCorrect;
    
    protected List<KeyCode> activeInputs = new List<KeyCode>();

    private void Start()
    {
        letterIndex = 0;
        isCorrect = false;
        anyWordsText.text = anyWords;
    }

    private void Update()
    {
        CheckAnyKey();
        OldTypingMechanics();
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

    private void OldTypingMechanics()
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
}