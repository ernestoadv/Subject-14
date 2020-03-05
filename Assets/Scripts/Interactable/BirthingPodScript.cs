using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BirthingPodScript : MonoBehaviour
{
    private Dialogue dialogue;
    public KeyCode interactKey;
    public bool isInRange = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PodInteraction();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("HumanPlayer"))
        {
            isInRange = true;
            Debug.Log("Player is in range of birthingPod object");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("HumanPlayer"))
        {
            isInRange = false;
            Debug.Log("Player is NOT in range with birthingPod");
            FindObjectOfType<DialogueManager>().EndDialogue();
        }
    }

    private void PodInteraction()
    {
        if (isInRange && (Input.GetButtonDown("interact") || Input.GetButtonDown("interactAlt")))
        {
            Debug.Log("player is interacting with Pod");
            //play ui element 
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            FindObjectOfType<DialogueTrigger>().TriggerDialogue();

        }
    }
}
