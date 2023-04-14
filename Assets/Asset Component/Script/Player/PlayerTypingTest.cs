using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerTypingTest : MonoBehaviour
{
    [SerializeField] public string anyWords;
    private string codeText;
    public TextMeshProUGUI anyWordsText;
    [SerializeField] private int letterIndex;
    
    protected List<KeyCode> m_activeInputs = new List<KeyCode>();

    private void Start()
    {
        letterIndex = 0;
    }

    private void Update()
    {
        anyWordsText.text = anyWords;
        CheckAnyKey();
        TypingMechanics();
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
                    m_activeInputs.Remove(code);
                    m_activeInputs.Add(code);
                    pressedInput.Add(code);

                    Debug.Log(code + " was pressed");
                    codeText = code.ToString();
                }
            }
        }
    }

    private void TypingMechanics()
    {
        var letter = anyWords.ToCharArray();

        while (letterIndex < letter.Length) 
        {
            if (codeText == letter[letterIndex].ToString())
            {
                // anyWordsText.color = Color.green;
                // anyWordsText.text = anyWords.Remove(letterIndex, 1).
                //     Insert(letterIndex, "<color=green>" + letter[letterIndex] + "</color>");
                // anyWordsText.color += letter.Color.green;
                Debug.Log("Letter Correct");
                letterIndex++;
                Debug.Log(letter.Length);
            }
        }
    }
}
