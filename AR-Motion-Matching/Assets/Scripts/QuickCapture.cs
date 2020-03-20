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
    public Text record_mode;
    bool recording_state = false;
    int frames = 0;
    string movement_path = "";
    string target_folder = "/TargetMotionDB";
    string rec_on = "Recording", rec_off = "Not Recording";
    Animation anim;    AnimationClip clip;

    // Start is called before the first frame update
    void Start()
    {
        clip = new AnimationClip();        clip.legacy = true;
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

    void add_tags()
    {
        foreach (KeyValuePair<JointIndices3D, Transform> BodyPart in HumanBodyTracking.bodyJoints)
        {
            BodyPart.Value.gameObject.tag = "BodyPart";
        }
    }

    bool get_device_path()
    {
        //Checks if the technique name is a valid one
        if (Move_name.text.LastIndexOfAny(Path.GetInvalidFileNameChars()) >= 0 && !Move_name.text.Equals(string.Empty))
            return false;
#if UNITY_EDITOR        movement_path = Path.Combine(Application.dataPath + target_folder, Move_name.text + ".txt");


#elif UNITY_IOS        movement_path = Path.Combine(Application.persistentDataPath + target_folder, Move_name.text + ".txt");
#endif
        return true;
    }


    void Start_record()
    {
        //If the human body wasnt detected, do nothing.
        if (!HumanBodyTracking.Body_flag)
        {
            Debug.LogWarning("Human Body not detected.");
            return;
        }
        //If a invalid name was entered, do nothing.
        if (!get_device_path())
        {
            Debug.Log("movement name not valid.");
            return;
        }
        recording_state = true;
        record_mode.text = rec_on;

    }

    void Stop_record()
    {
        recording_state = false;
        record_mode.text = rec_off;
    }

    [SerializeField]
    public class BodyJson
    {
        public string name;
        public float time;
        public Vector3 position;
        public Quaternion rotation;
        public BodyJson(string newName, float t, Vector3 pos, Quaternion rot)
        {
            time = t;
            position = pos;
            rotation = rot;
            name = newName;
        }
    }
}
