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
    public InputField Move_name;
    public Text record_mode;
    public Text log;
    bool recording_state = false;
    int frames = 0;
    string movement_path = "";
    string target_folder = "/TargetMotionDB";
    string rec_on = "Recording", rec_off = "Not Recording";
    float time = 0.0f;
    Animation anim;    AnimationClip clip;
    List<BodySegment> bp_list;
    float frame_rate = 16;

    // Start is called before the first frame update
    void Start()
    {
        clip = new AnimationClip();        clip.legacy = true;
        bp_list = new List<BodySegment>();
        record_mode.text = rec_off;
        start_btn.onClick.AddListener(Start_record);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (recording_state)
        {
            //Debug.Log("in update");
            frames++;
            //Debug.Log(frames);
            //Writes to File every 30 frames
            if (frames % frame_rate == 0)
            {
                //Debug.Log("every frame");
                if (HumanBodyTracking.Body_flag) {
                    record_movement();
                }
                
                frames = 0;
            }
            //If frames dont get reset, reset them to prevent higher numbers.
            if (frames > frame_rate) frames = 0;
        }
        
    }

    IEnumerator create_dir()
    {
        string dir_path = "";
#if UNITY_EDITOR
        dir_path = Application.dataPath + target_folder;

#elif UNITY_IOS
        dir_path = Application.persistentDataPath + target_folder;

#endif
        if (!Directory.Exists(dir_path))
        {
            Directory.CreateDirectory(dir_path);
        }
        yield return new WaitForSeconds(0.5f);
    }

    //Captures the body movements and writes their locations
    void record_movement()
    {
        if (!File.Exists(movement_path))
        {
            FileStream tech_file = File.Create(movement_path);
            write_body_part(tech_file);
            
            tech_file.Close();
         
        }
        else if (File.Exists(movement_path))
        {
            
            FileStream tech_file = File.Open(movement_path, FileMode.Append);
            write_body_part(tech_file);

            tech_file.Close();
        }
        
    }

    void write_body_part(FileStream fs)
    {
        BinaryFormatter bf = new BinaryFormatter();
        foreach (KeyValuePair<JointIndices3D, Transform> BodyPart in HumanBodyTracking.bodyJoints)
        {
            bp_list.Add(new BodySegment(BodyPart.Key.ToString(), time, BodyPart.Value.position, BodyPart.Value.rotation));
            //Writes the name of the body part
            //bf.Serialize(fs, BodyPart.Key);
            //Writes the current time
           // bf.Serialize(fs, time);
            //Create a SerializableVector3 from the bodypart location
            //Writes the new data into the file
            //bf.Serialize(fs, (SerializableVector3)BodyPart.Value.position);
            //Create a SerializableQuaternion from the bodypart rotation
            //Writes the new data into the file
            //bf.Serialize(fs, (SerializableQuaternion)BodyPart.Value.rotation);
        }
        bf.Serialize(fs, bp_list);
        time += Time.deltaTime;

    }

    void load_movement()
    {
        string path = Application.dataPath + "/tal.dat";
        Debug.Log(path);
        if (File.Exists(path))        {            Debug.Log("In log");            BinaryFormatter bf = new BinaryFormatter();            FileStream tech_file = File.Open(path, FileMode.Open);            List<BodySegment> out_data = (List<BodySegment>)bf.Deserialize(tech_file);            foreach(BodySegment item in out_data)
            {
                print(item.ToString());
            }            //for (int  i = 0; i < 100; i++)
            //{
               // var tech_name = bf.Deserialize(tech_file);
               // float tech_time = (float)bf.Deserialize(tech_file);
              //  Vector3 tech_vector = (SerializableVector3)bf.Deserialize(tech_file);
               // Quaternion tech_rot = (SerializableQuaternion)bf.Deserialize(tech_file);
              //  Debug.Log("body part: " + tech_name + "\ntime: " + tech_time + "\nvector is: " + tech_vector + "\n quat is: " + tech_rot); 
           // }                                                tech_file.Close();        }
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
        create_dir();
        load_movement();
        if (!recording_state)
        {
            //If the human body wasnt detected, do nothing.
            if (!HumanBodyTracking.Body_flag)
            {
                Debug.LogWarning("Human Body not detected.");
                log.text += "\nHuman Body not detected.";
                //return;
            }
            //If a invalid name was entered, do nothing.
            if (!get_device_path())
            {
                Debug.Log("movement name not valid.");
                log.text += "\nfile name is invalid.";
                return;
            }
            if (File.Exists(movement_path))
            {
                Debug.LogWarning("Path already exists.");
                log.text += "File name already in use";
                return;
            }
            start_btn.GetComponentInChildren<Text>().text = "Stop";
            recording_state = true;
            record_mode.color = Color.green;
            record_mode.text = rec_on;

        }
        else if (recording_state)
        {
            start_btn.GetComponentInChildren<Text>().text = "Start";
            recording_state = false;
            record_mode.color = Color.red;
            record_mode.text = rec_off;
        }

    }

  

    


    
    
}
