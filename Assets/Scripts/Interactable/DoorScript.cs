using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    private bool colliderActive = true;
    public bool gotKeyCard = false;
    public bool isDoorClosed = true;

    private Animator animator;

    public Transform playerPos;
    public PlayerInventory playerInv;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Distance between door and player: " + Vector2.Distance(this.transform.position, playerPos.position));
        CloseDoor();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player is in range door");
            playerPos = collision.gameObject.GetComponent<Transform>();
            DoorInteraction();
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
            Debug.Log("got keycard and able to interact with door");
            if (isDoorClosed)
            {
                Debug.Log("door is opening...");
                //animation
                Debug.Log("should play open animation");
                animator.SetTrigger("opening");
                isDoorClosed = false;
                colliderActive = false;
                gameObject.GetComponent<BoxCollider2D>().enabled = colliderActive;
                Debug.Log("boxcollider of door should be disabled");
            }
            else if(Vector2.Distance(this.transform.position, playerPos.position) < 4.2)
            {
                GameObject.Find("LevelEnd").GetComponent<BoxCollider2D>().enabled = true;
            }
        }
        else
        {
            Debug.Log("no key card");
        }
    }

    private void CheckIfHasKeyCard()
    {
        gotKeyCard = playerInv.returnKeyInfo();
        Debug.Log("keycard = " + gotKeyCard);
    }

    private void CloseDoor()
    {
        if (!colliderActive && Vector2.Distance(this.transform.position, playerPos.position) > 5.1)
        {
            Debug.Log("Closing door...");
            colliderActive = true;
            gameObject.GetComponent<BoxCollider2D>().enabled = colliderActive;
            Debug.Log("collider of door should be activated");
            isDoorClosed = true;
            //animation
            Debug.Log("should play close animation");
            animator.SetTrigger("closing");
        }
        else if(Vector2.Distance(this.transform.position, playerPos.position) < 5.1)
        {
            DoorInteraction();
        }
    }
}
