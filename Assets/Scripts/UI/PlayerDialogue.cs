using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDialogue : MonoBehaviour
{
    public KeyCode interactKey;

    private bool isInRange, isShowed;
    private int numberSentences, count;

    public DialogueManager dialogueManager;
    public Dialogue dialogue;

    // Start is called before the first frame update
    void Start()
    {
        isInRange = false;
        isShowed = false;
        numberSentences = dialogue.sentences.Length;
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Interact();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("HumanPlayer") && !isShowed)
        {
            dialogueManager.StartDialogue(dialogue);
            isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("HumanPlayer"))
        {
            isInRange = false;
            isShowed = true;
            count = 0;
            dialogueManager.EndDialogue();
        }
    }

    private void Interact()
    {
        // You press the key
        if (isInRange && Input.GetKeyDown(interactKey))
        {
            // If there are more sentences, it will change.
            if (count <= numberSentences)
            {
                count++;
                dialogueManager.DisplayNextSentence();
            }
            // Else, close the dialogue.
            else
            {
                count = 0;
                dialogueManager.EndDialogue();
            }
        }
    }
}