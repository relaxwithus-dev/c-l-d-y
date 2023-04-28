using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ColorOnType : MonoBehaviour
{
    List <string> strList = new List<string>();
    string finalString;
    
    [SerializeField] public string anyWords;
    private string codeText;
    public TextMeshProUGUI anyWordsText;
    
    [SerializeField] private int letterIndex;
    public bool isCorrect;
    
    protected List<KeyCode> activeInputs = new List<KeyCode>();
    void Start()
    {
        Debug.Log(FormatText("testtext", "00100100"));
    }

    private void Update()
    {
        CheckAnyKey();
        
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
    
    private void TypingMechanics()
    {


        var letter = anyWords.ToCharArray();

        if (letterIndex < letter.Length)
        {
            if (letter[letterIndex].ToString() == codeText)
            {
                anyWordsText.text = anyWordsText.text.Replace(letter[letterIndex].ToString(), 
                    "<color=#0045FF>" + letter[letterIndex] + "</color>");
                letterIndex++;
            }
        }
        else
        {
            isCorrect = true;
        }
    }
    
    private string FormatText(string normalWord, string positions)
    {    
        finalString = null;
        strList.Clear();
        string positionString = positions.ToString();
        for(int i=0; i<normalWord.Length; i++)
        {    
            if(int.Parse(positions[i].ToString())==1)
            {
                strList.Add(normalWord[i]+"");
            }
            else
            {
                strList.Add("<color=yellow>"+normalWord[i]+"</color>");
            }
        }
        for(int i=0; i<strList.Count;i++)
        {
            finalString+=strList[i];
        }
        return finalString;
    }
}
