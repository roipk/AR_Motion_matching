﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WheelHandler : MonoBehaviour
{
    public float score = 70;
    public float max_obj_norm = 125;


    // Use this for initialization
    void Start()
    {

    }
                //image_color.color = Color.red;
            }
                //image_color.color = Color.yellow;
            }

    int get_level()
    {
        return (int)(((max_obj_norm - score) / max_obj_norm) * 100);

    }
}