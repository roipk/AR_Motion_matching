﻿using System.Collections;
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
    string user_sub_folder = "Captured_movement";
    string user_file_path = "Users_movement";
    string file_end = ".dat";
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Set_path());
    }

    //Captures the body movements and writes their locations
    public void record_movement()

    void write_body_part(FileStream fs)
        foreach (KeyValuePair<JointIndices3D, Transform> BodyPart in HumanBodyTracking.bodyJoints)
        {
            bp_list.Add(new BodySegment(BodyPart.Key.ToString(), capture_time, BodyPart.Value.position, BodyPart.Value.rotation));
        }
        bf.Serialize(fs, bp_list);
        capture_time += Time.deltaTime;

    IEnumerator Set_path()
    {
#if UNITY_EDITOR
#elif UNITY_IOS
#elif UNITY_ANDROID
#endif
        var folder_status = Directory.CreateDirectory(user_folder_path);
        yield return new WaitForSeconds(0.2f);
        if (!Directory.Exists(user_folder_path))
        {
            Debug.LogError("Failed to create folder at: " + user_folder_path);
        }
        string file_name = Path.Combine(user_file_path, file_end);

        user_file_location = Path.Combine(user_folder_path, file_name);

        File.Create(user_file_location);

        if (!File.Exists(user_file_location))
        {
            Debug.LogError("Failed to create file at: " + user_file_location);
        }
    }

}







