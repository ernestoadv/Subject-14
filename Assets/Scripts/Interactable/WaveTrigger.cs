using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveTrigger : MonoBehaviour
{
    GameObject player;
    Camera camera;
    private bool done = false;

    private void Start()
    {
        player = GameObject.Find("Player");
        camera = Camera.main;
    }

    void Update()
    {
        if(gameObject.transform.position.x < player.transform.position.x + 0.5f && !done)
        {
            gameObject.GetComponent<WaveSpawner>().enabled = true;
            camera.GetComponent<CameraFollow>().enabled = false;
            done = true;
        }
    }
}
