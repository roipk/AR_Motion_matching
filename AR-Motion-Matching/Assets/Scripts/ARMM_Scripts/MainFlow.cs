using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class MainFlow : MonoBehaviour
{

    HumanBodyData BodyData;
    ObjectNorm obj_norm;
    CaptureUser cap_user;
    bool started_recording = false;
    int frames_count = 0;
    float frame_rate = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        BodyData = GameObject.Find("HumanData").GetComponent<HumanBodyData>();
        obj_norm = GameObject.Find("ObjectNorm").GetComponent<ObjectNorm>();
        cap_user = GameObject.Find("CaptureUser").GetComponent<CaptureUser>();
        
    }

    // Update is called once per frame
    void Update()
    {
        frames_count++;
        if(frames_count%frame_rate == 0)
        {
            if (HumanBodyTracking.Body_flag)
            {

            }
            frames_count = 0;
        }
       
    }


}
