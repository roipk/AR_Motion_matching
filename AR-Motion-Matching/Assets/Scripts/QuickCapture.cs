/*******************
 *QuickCapture in an inner class that i will use to capture body movments and save them as animations.
 *
 *
 *
 * ******************/
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class QuickCapture : MonoBehaviour
{

    public Button start_btn;
    public Button stop_btn;
    public InputField Move_name;
    bool recording_state = false;
    int frames = 0;
    string movement_path = "";

    // Start is called before the first frame update
    void Start()
    {
        start_btn.onClick.AddListener(Start_record);
        stop_btn.onClick.AddListener(Stop_record);
    }

    // Update is called once per frame
    void Update()
    {
        if (recording_state)
        {
            frames++;
            //Writes to JSON every 30 frames
            if (frames % 30 == 0)
            {
                record_movement();
            }
            frames = 0;
        }
        
    }

    //Captures the body movements and writes their locations
    void record_movement()
    {
        foreach (KeyValuePair<JointIndices3D, Transform> BodyPart in HumanBodyTracking.bodyJoints)
        {

        }
    }

    string device_path()
    {
        return null;
    }

    IEnumerator WaitForSec(float n)    {        yield return new WaitForSeconds(n);
#if UNITY_EDITOR        string body_location = Path.Combine(Application.dataPath + "/TargetMotionDB", gameObject.name + ".txt");
#elif UNITY_IOS        string body_location = Path.Combine(Application.persistentDataPath + "/TargetMotionDB", file_name.text + ".txt");
#endif       //File.WriteAllText(body_location, json);
    }

    void Start_record()
    {
        //If the human body wasnt detected, do nothing.
        if (!HumanBodyTracking.Body_flag)
        {
            Debug.LogWarning("Human Body not detected.");
            return;
        }
        recording_state = true;

    }

    void Stop_record()
    {
        recording_state = false;
    }
}
