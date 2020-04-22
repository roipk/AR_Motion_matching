using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class MainFlow : MonoBehaviour
{

    HumanBodyData BodyData;
    ObjectNorm obj_norm;
    CaptureUser cap_user;
    Load_Movement load_move;
    AudioSource start_sound;
    bool started_recording = false;
    bool recording_state = false;
    int frames_count = 0;
    float frame_rate = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        BodyData = GameObject.Find("HumanData").GetComponent<HumanBodyData>();
        obj_norm = GameObject.Find("ObjectNorm").GetComponent<ObjectNorm>();
        cap_user = GameObject.Find("CaptureUser").GetComponent<CaptureUser>();
        load_move = GameObject.Find("Data_Loader").GetComponent<Load_Movement>();
        start_sound = GameObject.Find("Start_sound").GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!started_recording && HumanBodyTracking.Body_flag)
        {
            start_sound.Play();
            started_recording = true;
        }
        frames_count++;
        if(frames_count%frame_rate == 0)
        {
            if (HumanBodyTracking.Body_flag)
            {
                if(!recording_state)
                    cap_user.record_movement();
            }
            frames_count = 0;
        }
        
    }

    public void stop_recording()
    {
        if (!started_recording)
        {

        }
    }

   


}
