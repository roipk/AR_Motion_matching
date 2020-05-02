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
    CompareMovements comp_move;
    Tech_name technique_name;
    float Start_frame_index;
    bool started_recording = false;
    bool Recording_flag = false;
    int frames_count = 0;
    float frame_rate = 1;
    int corr_index = -1;
    float object_norm_score = -1;
    // Start is called before the first frame update
    void Start()
    {
        BodyData = GameObject.Find("HumanData").GetComponent<HumanBodyData>();
        obj_norm = GameObject.Find("ObjectNorm").GetComponent<ObjectNorm>();
        cap_user = GameObject.Find("CaptureUser").GetComponent<CaptureUser>();
        user_move = GameObject.Find("Data_Loader").GetComponent<Load_Movement>();
        user_move = GameObject.Find("Tech_Loader").GetComponent<Load_Movement>();
        start_sound = GameObject.Find("Start_sound").GetComponent<AudioSource>();
        comp_move = GameObject.Find("Compare").GetComponent<CompareMovements>();
        technique_name = GameObject.Find("Selected_tech").GetComponent<Tech_name>();
        Start_frame_index = -1;
        
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

    public void stop_recording()
    {
        Recording_flag = true;
        StartCoroutine(Load_movements());


    }

    //Once the movements has stopped, load the two movements
    IEnumerator Load_movements() {
        tech_move.Load_data(technique_name.selected_tech_path);
        user_move.Load_data(cap_user.user_file_location);
        
        while (user_move.Loaded_movement.Count < user_move.Frame_List.Count && tech_move.Loaded_movement.Count < tech_move.Frame_List.Count)
        {
            yield return new WaitForSeconds(0.1f);
        }
        final_score();
    }

    void final_score()
    {
        object_norm_score = comp_move.Cross_correlation(user_move.Loaded_movement, tech_move.Loaded_movement);
    }




   


}
