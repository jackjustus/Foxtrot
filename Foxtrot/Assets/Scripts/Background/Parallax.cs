using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    private GameObject cam;                                         // The game camera
    [SerializeField] bool doParallaxX, doParallaxY;                 // Enable / Disable Parallax motion

    [Header("Parallax Coefficients")]
    [SerializeField] public float parallaxEffectX;                  // The greater the parallax coefficent, the more the background moves with the camera
    [SerializeField] public float parallaxEffectY;                  // The greater the parallax coefficent, the more the background moves with the camera
    
    [Header("Parallax Bias")]
    [SerializeField] public float startPosY;                        // The starting position of the background. Almost acts like a bias

    private float lengthX, startPosX, distanceTraveledX, parallaxDistanceX; // Helper Variables for the x axis
    private float parallaxDistanceY;                                        // Helper variables for the y axis


    void Start()
    {
        // Initialize the game camera. Used to track the camera's position
        cam = GameObject.FindGameObjectWithTag("MainCamera");

        // Get the length of the background. Used to determine when to reset the background
        lengthX = GetComponent<SpriteRenderer>().bounds.size.x;
    }



    void FixedUpdate()
    {

        distanceTraveledX = 0;                                                              // IDK What this does, but it always is 0               // distanceTraveled = cam.transform.position.x * (1 - parallaxEffect);   


        // Calculating Parallax X
        if (doParallaxX)
            parallaxDistanceX = startPosX + (cam.transform.position.x * parallaxEffectX);   // How much the background should move (X axis)
        else
            parallaxDistanceX = transform.position.x;                                       // Keep the X constant

        // Calculating Parallax Y
        if (doParallaxY)
            parallaxDistanceY = startPosY + (cam.transform.position.y * parallaxEffectY);   // How much the background should move (Y axis)
        else
            parallaxDistanceY = transform.position.y;                                       // Keep the Y constant
        


        // Updating the background's position with the new parallax distance
        transform.position = new Vector3(startPosX + parallaxDistanceX, parallaxDistanceY, transform.position.z);


        // Resetting the background if it goes out of bounds
        if (distanceTraveledX > startPosX + lengthX) startPosX += lengthX;
        else if (distanceTraveledX < startPosX - lengthX) startPosX -= lengthX;

        // Debug
        // Debug.Log("Distance Traveled: " + distanceTraveled + " Parallax Distance: " + parallaxDistance);
    }
}
