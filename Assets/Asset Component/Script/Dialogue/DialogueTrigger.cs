using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Visual")]
    [SerializeField] private GameObject visualQue;

    [Header("InkJSON")]
    [SerializeField] private TextAsset inkJSON;

    private bool playerInRanged;

    // Start is called before the first frame update
    void Start()
    {
        playerInRanged = false;
        visualQue.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRanged && !DialogueManager.Instance.dialogueIsPlaying)
        {
            visualQue.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                DialogueManager.Instance.EnterDialogueMode(inkJSON);
            }
        }
        else
        {
            visualQue.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            playerInRanged = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            playerInRanged = false;
        }
    }
}
