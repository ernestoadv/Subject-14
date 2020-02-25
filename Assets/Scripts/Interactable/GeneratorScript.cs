using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorScript : MonoBehaviour
{
    public KeyCode interactKey;
    public bool isInRange = false;

    public Sprite actualSprite;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GeneratorInteraction();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("HumanPlayer"))
        {
            isInRange = true;
            Debug.Log("Player is in range of generator object");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("HumanPlayer"))
        {
            isInRange = false;
            Debug.Log("Player is NOT in range with generator");
        }
    }

    private void GeneratorInteraction()
    {
        if (isInRange && Input.GetKeyDown(interactKey))
        {
            Debug.Log("player is interacting with generator");
            //play ui element 
        }
    }


}
