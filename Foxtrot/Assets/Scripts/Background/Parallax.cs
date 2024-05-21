using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    private float length, startPosX;
    private GameObject cam;
    public float parallaxEffect, startPosY;                    // The greater the parallax coefficent, the more the background moves with the camera

    [SerializeField] bool doParallaxY;
    public float parallaxEffectY;

    private float distanceTraveled, parallaxDistance, parallaxDistanceY;

    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        length = GetComponent<SpriteRenderer>().bounds.size.x;


    }

    void FixedUpdate()
    {

        distanceTraveled = 0;                                                       // IDK What this does, but it always is 0               // distanceTraveled = cam.transform.position.x * (1 - parallaxEffect);   
        parallaxDistance = cam.transform.position.x * parallaxEffect;               // Camera distance in x axis * parallax effect = how much the background should move


        if (doParallaxY)
            parallaxDistanceY = startPosY + (cam.transform.position.y * parallaxEffectY);         // Camera dist from start in y axis  * parallax effect = how much the background should move
        else
            parallaxDistanceY = transform.position.y;                               // Keep the Y constant
        

        // Moving the background to the start pos + parallax distance
        transform.position = new Vector3(startPosX + parallaxDistance, parallaxDistanceY, transform.position.z);

        if (distanceTraveled > startPosX + length) startPosX += length;
        else if (distanceTraveled < startPosX - length) startPosX -= length;

        // Debug
        // Debug.Log("Distance Traveled: " + distanceTraveled + " Parallax Distance: " + parallaxDistance);
    }
}
