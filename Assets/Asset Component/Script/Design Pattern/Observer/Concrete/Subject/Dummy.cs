using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dummy : MonoBehaviour
{
    [Header("Clock Component")] 
    [SerializeField] private string clockWords;
    private string clockWordsTemporary;
    [SerializeField] private TextMeshProUGUI clockWordsUI;

    [SerializeField] private int letterIndex;
    public bool IsCorrect { get; private set; }
    [SerializeField] private bool isClockArea;
    
    private string[] keyCodeNumber;
    [SerializeField] private string codeTextTemp;
    
    [Header("Input Character Component")]
    protected List<KeyCode> activeInputs = new List<KeyCode>();
    public string CodeText { get; private set; }

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
                    
                    CodeText = code.ToString();
                }
            }
        }
    }
}