using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using System;
using UnityEngine.Serialization;

public class DialogueManager : SingletonMonobehaviour<DialogueManager>
{
    #region Variable

    [Header("GameObject")]
    [SerializeField] private GameObject player;
    private GameObject playerBubbleTransform;
    [SerializeField] private GameObject npc01;
    private GameObject npc01BubbleTransform;

    [Header("DialogueUI")]
    [SerializeField] private GameObject dialogueBubble;
    [SerializeField] private TextMeshProUGUI dialogueTextUI;

    [Header("Parameter")]
    [SerializeField] private float typingSpeed;

    private Coroutine displayLineCoroutine;
    private bool canContinueToNextLine;
    private bool isRichTextTag;

    private Story currentStory;
    public bool dialogueIsPlaying { get; private set; }
    
    // Constant
    private const string SPEAKER_TAG = "speaker";

    #endregion

    #region MonoBehaviour Callbacks

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        // get gameobject in child of the object with idex 0
        playerBubbleTransform = player.transform.GetChild(0).gameObject;
        npc01BubbleTransform = npc01.transform.GetChild(0).gameObject;

        dialogueIsPlaying = false;
        dialogueBubble.SetActive(false);
        
        typingSpeed = 0.02f;
        canContinueToNextLine = false;
        isRichTextTag = false;
    }

    private void Update()
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

    #endregion

    #region Relax With Us Callbacks
    
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

            string nextLine = currentStory.Continue();
            // handle case where the last line is an external function
            if (nextLine.Equals("") && !currentStory.canContinue)
            {
                StartCoroutine(ExitDialogueMode());
            }
            // otherwise, handle the normal case for continuing the story
            else
            {
                // handle tags
                HandleTags(currentStory.currentTags);
                displayLineCoroutine = StartCoroutine(DisplayLine(nextLine));
            }
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
        dialogueTextUI.text = "";
    }

    IEnumerator DisplayLine(string line)
    {
        //empty the dialogue text
        dialogueTextUI.text = "";

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
                dialogueTextUI.text += letter;

                if (letter == '>')
                {
                    isRichTextTag = false;
                }
            }
            // if not
            else
            {
                dialogueTextUI.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }
        }

        // user can continue after all of the character has been displayed
        canContinueToNextLine = true;
    }

    private void HandleTags(List<string> currentTags)
    {
        // loop through each tag and handle it accordingly
        foreach (string tag in currentTags)
        {
            // parse the tag
            string[] splitTag = tag.Split(':');
            if (splitTag.Length != 2)
            {
                Debug.LogError("Tag could not be appropriately parsed: " + tag);
            }
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            // handle the tag
            switch (tagKey)
            {
                case SPEAKER_TAG:
                    if (tagValue == "Player")
                    {
                        dialogueBubble.transform.position = new Vector2(playerBubbleTransform.transform.position.x, playerBubbleTransform.transform.position.y);
                    }
                    else if (tagValue == "Npc01")
                    {
                        dialogueBubble.transform.position = new Vector2(npc01BubbleTransform.transform.position.x, npc01BubbleTransform.transform.position.y);
                    }
                    break;

                // case: another tags (portrat, layout, etc)
                // break;
                default:
                    Debug.LogWarning("Tag came in but is not currently being handled: " + tag);
                    break;
            }
        }
    }

    #endregion

    

}
