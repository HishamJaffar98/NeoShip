using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    RocketShip rocket;
    float distanceBetweenCameraAndPlayer;
    float xCoordinateToFollow;
    void Start()
    {
        rocket = FindObjectOfType<RocketShip>();
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 position = transform.position;
        //distanceBetweenCameraAndPlayer = transform.position.x - rocket.gameObject.transform.position.x;
        //float differenceInDistance = initialDistance - distanceBetweenCameraAndPlayer;
        //if(distanceBetweenCameraAndPlayer<initialDistance)
        //{
        //  position.x += differenceInDistance;
        //}
        //transform.position = position;
        Vector3 screenInWorldCoords = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        if(rocket.gameObject.transform.position.x > screenInWorldCoords.x)
        {
            xCoordinateToFollow = rocket.gameObject.transform.position.x;
            transform.position = new Vector3(xCoordinateToFollow, transform.position.y, transform.position.z);
        }
        
    }
}
