using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    private float length, startposX, startposY;
    private GameObject cam;
    public float parallaxEffect;

    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        // startpos = cam.transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        
    
    }

    void FixedUpdate()
    {
        Vector2 distanceTraveled = cam.transform.position * (1 - parallaxEffect);
        Vector2 parallaxDistance = cam.transform.position * parallaxEffect;

        Vector3 targetPos = new Vector3(startposX + parallaxDistance.x, transform.position.y, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, 0.1f);

        if (distanceTraveled.x > startposX + length) startposX += length;
        else if (distanceTraveled.x < startposX - length) startposX -= length;
    }
}
