using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    #region Variable

    [Header("Visual")]
    [SerializeField] private GameObject visualQue;

    [Header("InkJSON")]
    [SerializeField] private TextAsset inkJSON;

    private bool playerInRanged;

    #endregion

    #region MonoBehaviour Callbacks

    private void Start()
    {
        playerInRanged = false;
        visualQue.SetActive(false);
    }
    
    private void Update()
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

    #endregion

    #region Collider Callbacks

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            playerInRanged = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            playerInRanged = false;
        }
    }

    #endregion

    
}
