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
    Load_Movement user_move;
    Load_Movement tech_move;
    AudioSource start_sound;
    bool started_recording = false;
    bool Recording_flag = false;
    int frames_count = 0;
    float frame_rate = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        BodyData = GameObject.Find("HumanData").GetComponent<HumanBodyData>();
        obj_norm = GameObject.Find("ObjectNorm").GetComponent<ObjectNorm>();
        cap_user = GameObject.Find("CaptureUser").GetComponent<CaptureUser>();
        user_move = GameObject.Find("Data_Loader").GetComponent<Load_Movement>();
        user_move = GameObject.Find("Tech_Loader").GetComponent<Load_Movement>();
        start_sound = GameObject.Find("Start_sound").GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Recording_flag)
        {
            if (!started_recording && HumanBodyTracking.Body_flag)
            {
                start_sound.Play();
                started_recording = true;
            }
            frames_count++;
            if (frames_count % frame_rate == 0)
            {
                if (HumanBodyTracking.Body_flag)
                {
                    cap_user.record_movement();
                }
                frames_count = 0;
            }
        }
        
        
    }

    public void call_stop_recording()
    {
        StartCoroutine(stop_recording());
    }

    IEnumerator stop_recording()
    {
        Recording_flag = true;
        StartCoroutine(user_move.Load_data(cap_user.user_file_location));
        StartCoroutine(tech_move.Load_data("tech location tbd"));
        while (user_move.Loaded_movement.Count < user_move.Frame_List.Count && tech_move.Loaded_movement.Count < tech_move.Frame_List.Count) {
            yield return new WaitForSeconds(0.1f);
        }
        

    }

   


}
