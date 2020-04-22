using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class CaptureUser : MonoBehaviour
{

    public string user_file_location;
    float capture_time;
    string user_folder_path;
    string user_sub_folder;
    string user_file_path;
    string file_end;
    // Start is called before the first frame update
    void Start()
    {
        user_file_location = "";
        capture_time = 0;
        user_folder_path = "";
        user_sub_folder = "Captured_movement";
        user_file_path = "Users_movement";
        file_end = ".dat";
        StartCoroutine(Set_path());
    }

    //Captures the body movements and writes their locations
    public void record_movement()    {                if (File.Exists(user_file_location))        {            FileStream tech_file = File.Open(user_file_location, FileMode.Append);            write_body_part(tech_file);            tech_file.Close();        }    }

    void write_body_part(FileStream fs)    {        List<BodySegment> bp_list = new List<BodySegment>();        BinaryFormatter bf = new BinaryFormatter();
        foreach (KeyValuePair<JointIndices3D, Transform> BodyPart in HumanBodyTracking.bodyJoints)
        {
            bp_list.Add(new BodySegment(BodyPart.Key.ToString(), capture_time, BodyPart.Value.position, BodyPart.Value.rotation));
        }
        bf.Serialize(fs, bp_list);
        capture_time += Time.deltaTime;    }

    IEnumerator Set_path()
    {
#if UNITY_EDITOR         user_folder_path = Path.Combine("Assets", user_sub_folder);
#elif UNITY_IOS        user_folder_path = Path.Combine(Application.persistentDataPath,user_sub_folder); 
#elif UNITY_ANDROID        user_folder_path = Path.Combine(Application.persistentDataPath,user_sub_folder);    
#endif
        var folder_status = Directory.CreateDirectory(user_folder_path);
        yield return new WaitForSeconds(0.2f);
        if (!Directory.Exists(user_folder_path))
        {
            Debug.LogError("Failed to create folder at: " + user_folder_path);
        }
        string file_name = user_file_path + file_end;

        user_file_location = Path.Combine(user_folder_path, file_name);

        File.Create(user_file_location);

        if (!File.Exists(user_file_location))
        {
            Debug.LogError("Failed to create file at: " + user_file_location);
        }
    }

}








