using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{

    public bool foundKeyCardOne = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool returnKeyInfo()
    {
        return foundKeyCardOne;
    }

    public void PickUpKey(int value)
    {
        switch (value)
        {
            case 1:
                foundKeyCardOne = true;
                print("key one found");
                break;
            case 2:
                print("key2 found");
                break;

            default:
                break;
        }
    }
}
