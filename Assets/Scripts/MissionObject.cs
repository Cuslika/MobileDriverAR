using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class MissionObject : MonoBehaviour
{
    public bool isCollected;

    void Update()
    {
        if(!isCollected)
            transform.Rotate(0f, 0f, 1f);
    }
    
}
