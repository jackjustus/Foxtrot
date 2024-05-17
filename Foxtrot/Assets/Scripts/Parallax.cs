using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    private float length, startpos;
    private GameObject cam;
    public float parallaxEffect;

    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        // startpos = cam.transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    
    }

    public void LevelLoaded() {

    }

    void FixedUpdate()
    {
        float distanceTraveled = cam.transform.position.x * (1 - parallaxEffect);
        float parallaxDistance = cam.transform.position.x * parallaxEffect;

        transform.position = new Vector3(startpos + parallaxDistance, transform.position.y, transform.position.z);

        if (distanceTraveled > startpos + length) startpos += length;
        else if (distanceTraveled < startpos - length) startpos -= length;
    }
}
