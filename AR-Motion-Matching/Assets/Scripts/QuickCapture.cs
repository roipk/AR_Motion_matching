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
using UnityEditor;

public class QuickCapture : MonoBehaviour
{

    public Button start_btn;
    public InputField Move_name;
    public Text record_mode;
    public Text log;
    Tech_name technique_name;
    bool recording_state = false;
    int frames = 0;
    string movement_path = "";
    string target_folder = "/TargetMotionDB";
    string rec_on = "Recording", rec_off = "Not Recording";
    float time = 0.0f;
    List<BodySegment> bp_list;
    List<BodySegment> skele_data = new List<BodySegment>();
    float frame_rate = 1;

    // Start is called before the first frame update
    void Start()
    {
        technique_name = GameObject.Find("Selected_tech").GetComponent<Tech_name>();
        bp_list = new List<BodySegment>();
        record_mode.text = rec_off;
        start_btn.onClick.AddListener(Start_record);
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (recording_state)
        {

            frames++;
            if (frames % frame_rate == 0)
            {
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
            Debug.Log("Trying to create dir: " + dir_path);
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
        }
        bf.Serialize(fs, bp_list);
        time += Time.deltaTime;

    }
    //void add_tags()
    //{
    //    foreach (KeyValuePair<JointIndices3D, Transform> BodyPart in HumanBodyTracking.bodyJoints)
    //    {
    //        BodyPart.Value.gameObject.tag = "BodyPart";
    //    }
    //}

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
        StartCoroutine(create_dir());
        
        //load_movement();        
        
        if (!recording_state)
        {
            //If the human body wasnt detected, do nothing.
            if (!HumanBodyTracking.Body_flag)
            {
                Debug.LogWarning("Human Body not detected.");
                log.text += "\nHuman Body not detected.";
                return;
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
                log.text += "\nFile name already in use";
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
            Copy_movement_to_tech();
        }

    }

    string get_folder_path()
    {
        string folder_path = Path.Combine(Path.GetDirectoryName(technique_name.selected_tech_path), Move_name.text + ".dat");
        if (folder_path != "")
            return folder_path;
        return "";
    }

    void Copy_movement_to_tech()
    {
       if(File.Exists(movement_path))
            FileUtil.CopyFileOrDirectory(movement_path, get_folder_path());
       else
            Debug.LogError("Failed to copy " + movement_path + " to " + get_folder_path());
    }

  

    


    
    
}
