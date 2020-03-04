using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CloseUp : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public Dialogue dialogue;
    public GameObject longFade;
    public GameObject blackFade;
    private int numberSentences, count;
    private bool dialogueShowed;

    // Start is called before the first frame update
    void Start()
    {
        dialogueManager.StartDialogue(dialogue);
        numberSentences = dialogue.sentences.Length;
        count = 2;
        dialogueShowed = false;
        Debug.Log("Number of sentences " + numberSentences);
    }

    // Update is called once per frame
    void Update()
    {
        Interact();
    }

    private void Interact()
    {
        // You press the key
        if (!dialogueShowed && Input.GetButtonDown("interact"))
        {
            // If there are more sentences, it will change.
            if (count <= numberSentences)
            {
                Debug.Log("El número " + count);
                FindObjectOfType<AudioManager>().PlaySound("Bubbles");
                count++;
                dialogueManager.DisplayNextSentence();
                longFade.SetActive(false);
                longFade.SetActive(true);
            }
            else
            {
                Debug.Log("Ha terminado");
                count = 0;
                dialogueShowed = true;
                dialogueManager.EndDialogue();
                FindObjectOfType<AudioManager>().PlaySound("Glass");
            }
        }
        else if (dialogueShowed) {

            blackFade.SetActive(true);
            StartCoroutine(wait());
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //Load new level
        Time.timeScale = 1; //Unfreeze the game
    }
}
