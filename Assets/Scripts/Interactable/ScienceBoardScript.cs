using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScienceBoardScript : MonoBehaviour
{
    public KeyCode interactKey;
    public bool isInRange = false;
    public DialogueManager dialogueManager;
    public Dialogue dialogue;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        BoardInteraction();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("HumanPlayer"))
        {
            isInRange = true;
            Debug.Log("Player is in range of scienceboard object");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("HumanPlayer"))
        {
            isInRange = false;
            Debug.Log("Player is NOT in range with scienceboard");
        }
    }

    private void BoardInteraction()
    {
        if (isInRange && (Input.GetButtonDown("interact") || Input.GetButtonDown("interactAlt")))
        {
            Debug.Log("player is interacting with scienceboard");
            //play ui element 
            dialogueManager.StartDialogue(dialogue);
        }
    }


}
