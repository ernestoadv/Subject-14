using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableScript : MonoBehaviour
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
        TableInteraction();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("HumanPlayer"))
        {
            isInRange = true;
            Debug.Log("Player is in range of table object");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("HumanPlayer"))
        {
            isInRange = false;
            Debug.Log("Player is NOT in range with table");
        }
    }

    private void TableInteraction()
    {
        if (isInRange && (Input.GetButtonDown("interact") || Input.GetButtonDown("interactAlt")))
        {
            Debug.Log("player is interacting with table");
            //play ui element 
        }
    }


}
