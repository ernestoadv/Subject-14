using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCard : MonoBehaviour
{
    int keyValue = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //check if the collision is the player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("Collision with keycard");
        PlayerInventory playerInventory = collision.GetComponent<PlayerInventory>();
        if (playerInventory)
        {
            print("calling player function...");
            playerInventory.PickUpKey(keyValue);
            Destroy(gameObject);
        }

    }
}
