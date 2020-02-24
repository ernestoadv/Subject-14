using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScienceBoardScript : MonoBehaviour
{
    public KeyCode interactKey;
    public bool isInRange = false;

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
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = true;
            Debug.Log("Player is in range of scienceboard object");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = false;
            Debug.Log("Player is NOT in range with scienceboard");
        }
    }

    private void BoardInteraction()
    {
        if (isInRange && Input.GetKeyDown(interactKey))
        {
            Debug.Log("player is interacting with scienceboard");
            //play ui element 
        }
    }


}
