﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour{
    
    public GameObject cam;

    private float lenght, startpos;
    public float parallaxEffect;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera");
        startpos = transform.position.x;
        lenght = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void FixedUpdate()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float dist = (cam.transform.position.x * parallaxEffect);

        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);

        if (temp > startpos + lenght) startpos += lenght;
        else if (temp < startpos - lenght) startpos -= lenght;
    }
}
