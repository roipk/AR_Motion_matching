using System;
using System.Collections;using System.Collections.Generic;using System.IO;using System.Runtime.Serialization.Formatters.Binary;using UnityEngine;using UnityEngine.SceneManagement;using UnityEngine.UI;using UnityEngine.XR.ARFoundation;public class MainFlow : MonoBehaviour{    public ARCameraManager cam;    public GameObject DEBUG_UI;    CaptureUser cap_user;    Load_Movement user_move;    Load_Movement tech_move;    AudioSource start_sound;    CompareMovements comp_move;    Tech_name technique_name;    float Start_frame_index;    bool started_recording = false;    bool Recording_flag = false;    int frames_count = 0;    float frame_rate = 1;    int corr_index = -1;    float object_norm_score = -1;

    // Start is called before the first frame update
    void Start()    {        cap_user = GameObject.Find("CaptureUser").GetComponent<CaptureUser>();        user_move = GameObject.Find("Data_Loader").GetComponent<Load_Movement>();        tech_move = GameObject.Find("Tech_Loader").GetComponent<Load_Movement>();        start_sound = GameObject.Find("Start_sound").GetComponent<AudioSource>();        comp_move = GameObject.Find("Compare").GetComponent<CompareMovements>();        technique_name = GameObject.Find("Selected_tech").GetComponent<Tech_name>();        Start_frame_index = -1;        //Debug.LogError("ARCamera Status: " + UnityEngine.XR.ARFoundation.ARSession.state + "\nAR Reason: " + UnityEngine.XR.ARFoundation.ARSession.notTrackingReason);        //Debug.Log("Trying to install ARKIT.");                //try
        //{
        //    UnityEngine.XR.ARFoundation.ARSession.Install();
        //    UnityEngine.XR.ARFoundation.ARSession.CheckAvailability();
        //}        //catch(Exception e)
        //{
        //    Debug.LogError("Failed to install ARSession: " + e);
        //}            }

    // Update is called once per frame
    void Update()    {        if (technique_name.DEBUG_MODE)
        {
            DEBUG_UI.SetActive(true);
        }        else if (!technique_name.DEBUG_MODE)        {            DEBUG_UI.SetActive(false);        }        if (!Recording_flag)        {            if (!started_recording && HumanBodyTracking.Body_flag)            {                start_sound.Play();                started_recording = true;            }            frames_count++;            if (frames_count % frame_rate == 0)            {                if (HumanBodyTracking.Body_flag)                {                    cap_user.record_movement();                }                frames_count = 0;            }        }
    }    public void stop_recording()    {        Recording_flag = true;
        //StartCoroutine(Load_movements());
        HumanBodyTracking.Body_flag = false;        Load_movements();    }

    //Once the movements has stopped, load the two movements
    void Load_movements()
    {        if (technique_name.selected_tech_path == "")            Debug.LogError("Didn't get a path from selection.");        tech_move.Load_data(technique_name.selected_tech_path + ".dat");        user_move.Load_data(cap_user.user_file_location);        final_score();    }    void final_score()    {        object_norm_score = comp_move.Cross_correlation(user_move.Loaded_movement, tech_move.Loaded_movement);        if (object_norm_score < 0)        {            Debug.LogError("Failed to perform cross correlation.");            technique_name.obj_norm_score = 126;
            //return;
        }        else        {            technique_name.obj_norm_score = object_norm_score;        }

        Load_Feedback();
    }    void Reset_ARMm()
    {
        started_recording = false;
        Recording_flag = false;
        technique_name.selected_tech_path = "";
        technique_name.obj_norm_score = 127f;
    }    public void Load_Feedback()    {        SceneManager.LoadSceneAsync("Feedback", LoadSceneMode.Additive);    }    public void Load_MainUI()    {        SceneManager.LoadSceneAsync("MainUI", LoadSceneMode.Additive);    }    public void Load_ARMm()    {        Reset_ARMm();        //SceneManager.LoadScene("ARMm");    }


}