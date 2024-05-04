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
        startpos = cam.transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        
    }

    void FixedUpdate()
    {
        float dist = (cam.transform.position.x * parallaxEffect);

        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);
    }
}
