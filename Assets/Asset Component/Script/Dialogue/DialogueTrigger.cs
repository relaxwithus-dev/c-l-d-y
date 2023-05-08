using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("InkJSON")]
    [SerializeField] private TextAsset inkJSON;

    private bool playerInRanged;

    // Start is called before the first frame update
    void Start()
    {
        playerInRanged = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRanged && !DialogueManager.Instance.dialogueIsPlaying)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                DialogueManager.Instance.EnterDialogueMode(inkJSON);
            }
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
