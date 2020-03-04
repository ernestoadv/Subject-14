using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueInteraction : MonoBehaviour
{
    public KeyCode interactKey;

    private bool isInRange, isShowing;
    private int numberSentences, count;

    public DialogueManager dialogueManager;
    public Dialogue dialogue;

    // Start is called before the first frame update
    void Start()
    {
        isInRange = false;
        isShowing = false;
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
        if (collision.gameObject.CompareTag("HumanPlayer"))
        {
            isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("HumanPlayer"))
        {
            isInRange = false;
            isShowing = false;
            count = 0;
            dialogueManager.EndDialogue();
        }
    }

    private void Interact()
    {
        // You press the key
        if (isInRange && Input.GetButtonDown("interact"))
        {
            // If the dialogue wasn't showing, it's displayed.
            if (!isShowing)
            {
                count = 0;
                isShowing = true;
                dialogueManager.StartDialogue(dialogue);
                count++;
            }
            // If it's already appearing in the screen.
            else
            {
                // If there are more sentences, it will change.
                if(count <= numberSentences)
                {
                    count++;
                    dialogueManager.DisplayNextSentence();
                }
                // Else, close the dialogue.
                else
                {
                    isShowing = false;
                    count = 0;
                    dialogueManager.EndDialogue();
                }
            }
        }
    }
}
