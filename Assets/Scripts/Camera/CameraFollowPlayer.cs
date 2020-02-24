using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public Camera camera;
    public Transform Player;
    public Transform initialPositionOfCamera, endPositionOfCamera;
    public float height = 0f;
    private bool attachCamera;

    private void Start()
    {
        transform.position = new Vector3(initialPositionOfCamera.position.x, height, -12);
    }
/*
    bool UpdatedAttached()
    {
        // the camera won't move until the player reaches the camera initial position
        if (Player.position.x <= initialPositionOfCamera.position.x)
        {
            attachCamera = false;
        }
        else
        {
            // area between the initial position of the camera and the end of the wave
            attachCamera = true;
        }

        return attachCamera;
    }
*/
    private void Update()
    {
        if (Player.position.x > initialPositionOfCamera.position.x && Player.position.x <= endPositionOfCamera.position.x)
        {
            transform.position = new Vector3(Player.position.x, height, -12);
        }
        
    }
}
