using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTyping : MonoBehaviour
{
    [Header("Input Character Component")]
    protected List<KeyCode> activeInputs = new List<KeyCode>();
    public string CodeText { get; private set; }

    [Header("Reference")] private TypingDummy typingObjectBase;

    private void Awake()
    {
        typingObjectBase = GameObject.Find("Object").GetComponent<TypingDummy>();
    }

    private void Update()
    {
        if (typingObjectBase.isTypingArea)
        {
            CheckAnyKey();
        }
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
                    CodeText = code.ToString();
                }
            }
        }
    }
}