using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class CaptureUser : MonoBehaviour
{

    List<BodySegment> User_data = new List<BodySegment>();
    string user_file_location = "";
    float capture_time = 0;
    string user_folder_path = "";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Captures the body movements and writes their locations
    void record_movement()    {        if (!File.Exists(user_file_location))        {            FileStream tech_file = File.Create(user_file_location);            write_body_part(tech_file);            tech_file.Close();        }        else if (File.Exists(user_file_location))        {            FileStream tech_file = File.Open(user_file_location, FileMode.Append);            write_body_part(tech_file);            tech_file.Close();        }    }

    void write_body_part(FileStream fs)    {        List<BodySegment> bp_list = new List<BodySegment>();        BinaryFormatter bf = new BinaryFormatter();
        foreach (KeyValuePair<JointIndices3D, Transform> BodyPart in HumanBodyTracking.bodyJoints)
        {
            bp_list.Add(new BodySegment(BodyPart.Key.ToString(), capture_time, BodyPart.Value.position, BodyPart.Value.rotation));
        }
        bf.Serialize(fs, bp_list);
        capture_time += Time.deltaTime;    }

    void Set_path()
    { 
#if UNITY_EDITOR         user_folder_path = "Assets";#elif UNITY_IOS        user_folder_path = Application.persistentDataPath;
#elif UNITY_ANDROID        user_folder_path = Application.persistentDataPath;
#endif
    }

}








