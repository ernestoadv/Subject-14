using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public KeyCode interactKey;
    private bool colliderActive = true;
    public bool gotKeyCard = false;
    public bool doorClosed = true;

    public Transform playerPos;

    public PlayerInventory playerInv;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Distance between door and player: " + Vector2.Distance(this.transform.position, playerPos.position));
        CloseDoor();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerPos = collision.gameObject.GetComponent<Transform>();
            DoorInteraction();
            Debug.Log("Player is in range door");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player is NOT in range with door");
        }
    }

    private void DoorInteraction()
    {
        CheckIfHasKeyCard();
        if (gotKeyCard)
        {
            Debug.Log("able to interact with door");
            GameObject.Find("LevelEnd").GetComponent<BoxCollider2D>().enabled = true;
            if (doorClosed)
            {
                Debug.Log("player is opening door");
                doorClosed = false;
                colliderActive = false;
                //play ui element 
                gameObject.GetComponent<BoxCollider2D>().enabled = colliderActive;
                //change sprite of door
                Debug.Log("boxcollider of door should be disabled");
            }
        }
        else
        {
            Debug.Log("no key card");
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    private void CheckIfHasKeyCard()
    {
        Debug.Log("keycard = " + gotKeyCard);
        gotKeyCard = playerInv.returnKeyInfo();
    }

    private void CloseDoor()
    {
        if (!colliderActive && Vector2.Distance(this.transform.position, playerPos.position) > 2.6)
        {
            Debug.Log("Closing door...");
            colliderActive = true;
            gameObject.GetComponent<BoxCollider2D>().enabled = colliderActive;
            Debug.Log("collider of door should be activated");
            //change sprite of door to closed 
            doorClosed = true;
        }
    }
}
