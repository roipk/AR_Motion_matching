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
using System.Runtime.Serialization.Formatters.Binary;
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
    float time = 0.0f;
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
        BinaryFormatter bf = new BinaryFormatter();        FileStream tech_file = File.Create(movement_path);
        foreach (KeyValuePair<JointIndices3D, Transform> BodyPart in HumanBodyTracking.bodyJoints)
        {
            //Writes the name of the body part
            bf.Serialize(tech_file, BodyPart.Key);
            //Writes the current time
            bf.Serialize(tech_file, time);
            //Create a SerializableVector3 from the bodypart location
            SerializableVector3 body_holder_pos = new SerializableVector3(BodyPart.Value.position.x, BodyPart.Value.position.y, BodyPart.Value.position.z);
            //Writes the new data into the file
            bf.Serialize(tech_file, body_holder_pos);
            //Create a SerializableQuaternion from the bodypart rotation
            SerializableQuaternion body_holder_quat = new SerializableQuaternion(BodyPart.Value.rotation.x, BodyPart.Value.rotation.y, BodyPart.Value.rotation.z, BodyPart.Value.rotation.w);
            //Writes the new data into the file
            bf.Serialize(tech_file, body_holder_quat);
        }
        time += Time.deltaTime;
        tech_file.Close();
    }

    void load_movement()
    {
        if (File.Exists(movement_path))        {            BinaryFormatter bf = new BinaryFormatter();            FileStream tech_file = File.Open(movement_path, FileMode.Open);            string tech_name = (string)bf.Deserialize(tech_file);            float tech_time = (float)bf.Deserialize(tech_file);            SerializableVector3 tech_vector = (SerializableVector3)bf.Deserialize(tech_file);            SerializableQuaternion tech_rot = (SerializableQuaternion)bf.Deserialize(tech_file);                        tech_file.Close();        }
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
#if UNITY_EDITOR        movement_path = Path.Combine(Application.dataPath + target_folder, Move_name.text + ".dat");


#elif UNITY_IOS        movement_path = Path.Combine(Application.persistentDataPath + target_folder, Move_name.text + ".dat");
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
