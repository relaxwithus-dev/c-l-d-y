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
        if (Input.GetKeyDown(KeyCode.Space))
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
            dialogueText.text = currentStory.Continue();
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
}
