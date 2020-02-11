using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        DestroySelf();
    }

    private void DestroySelf()
    {
        if (Input.GetButtonUp("Ability1"))
        {
            print("shield disapeare..");
            Destroy(gameObject);
        }
    }
}
