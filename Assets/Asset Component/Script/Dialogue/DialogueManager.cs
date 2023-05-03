using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using System;

public class DialogueManager : SingletonMonobehaviour<DialogueManager>
{
    [Header("DialogueUI")]
    [SerializeField] private GameObject dialogueBubble;
    [SerializeField] private TextMeshProUGUI dialogueText;

    [Header("Parameter")]
    [SerializeField] private float typingSpeed = 0.02f;

    private Coroutine displayLineCoroutine;
    private bool canContinueToNextLine = false;
    private bool isRichTextTag = false;

    private Story currentStory;
    public bool dialogueIsPlaying { get; private set; }

    protected override void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    void Start()
    {
        dialogueIsPlaying = false;
        dialogueBubble.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // return when dialogue isn't playing
        if (!dialogueIsPlaying)
        {
            return;
        }
        // continue to tthe next line when player input submit buton
        if (canContinueToNextLine && Input.GetKeyDown(KeyCode.Space))
        {
            ContinueStory();
        }
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialogueBubble.SetActive(true);

        ContinueStory();
    }

    void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            // if line not finished display
            if (displayLineCoroutine != null)
            {
                // stop the last line
                StopCoroutine(displayLineCoroutine);
            }
            // continue
            displayLineCoroutine = StartCoroutine(DisplayLine(currentStory.Continue()));
        }
        else
        {
            StartCoroutine(ExitDialogueMode());
        }
    }

    IEnumerator ExitDialogueMode()
    {
        yield return new WaitForSeconds(0.1f);

        dialogueIsPlaying = false;
        dialogueBubble.SetActive(false);
        dialogueText.text = "";
    }

    IEnumerator DisplayLine(string line)
    {
        //empty the dialogue text
        dialogueText.text = "";

        canContinueToNextLine = false;

        // display each letter one at a time
        foreach (char letter in line.ToCharArray())
        {
            // // if submit button pressed, finished the line
            // if (Input.GetKeyDown(KeyCode.Space))
            // {
            //     dialogueText.text = line;
            //     break;
            // }

            // if RichTextTag found, add without displaying each word
            if (letter == '<' || isRichTextTag)
            {
                isRichTextTag = true;
                dialogueText.text += letter;

                if (letter == '>')
                {
                    isRichTextTag = false;
                }
            }
            // if not
            else
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }
        }

        // user can continue after all of the character has been displayed
        canContinueToNextLine = true;
    }
}
