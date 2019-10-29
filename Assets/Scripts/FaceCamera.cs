﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FaceCamera : MonoBehaviour
{
    private Transform target;

    void Start()
    {
        target = GameObject.Find("Main Camera").transform;
        
    }
   
    void Update()
    {
        transform.LookAt(target);
    }

}
