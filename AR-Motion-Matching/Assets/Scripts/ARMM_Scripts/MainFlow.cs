using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class MainFlow : MonoBehaviour
{

    HumanBodyData BodyData;
    ObjectNorm obj_norm;
    List<BodySegment> User_data = new List<BodySegment>();
    string user_file_location = "";
    bool started_recording = false;
    int frames_count = 0;
    float frame_rate = 1;
    float capture_time = 0;
    // Start is called before the first frame update
    void Start()
    {
        BodyData = GameObject.Find("HumanData").GetComponent<HumanBodyData>();
        obj_norm = GameObject.Find("ObjectNorm").GetComponent<ObjectNorm>();
        
    }

    // Update is called once per frame
    void Update()
    {
        frames_count++;
        if(frames_count%frame_rate == 0)
        {
            if (HumanBodyTracking.Body_flag)
            {

            }
            frames_count = 0;
        }
       
    }

    //Captures the body movements and writes their locations
    void record_movement()    {        if (!File.Exists(user_file_location))        {            FileStream tech_file = File.Create(user_file_location);            write_body_part(tech_file);            tech_file.Close();        }        else if (File.Exists(user_file_location))        {            FileStream tech_file = File.Open(user_file_location, FileMode.Append);            write_body_part(tech_file);            tech_file.Close();        }    }

    void write_body_part(FileStream fs)    {        List<BodySegment> bp_list = new List<BodySegment>();        BinaryFormatter bf = new BinaryFormatter();
        foreach (KeyValuePair<JointIndices3D, Transform> BodyPart in HumanBodyTracking.bodyJoints)
        {
            bp_list.Add(new BodySegment(BodyPart.Key.ToString(), capture_time, BodyPart.Value.position, BodyPart.Value.rotation));
            //Writes the name of the body part
            bf.Serialize(fs, BodyPart.Key);
            //Writes the current time
            bf.Serialize(fs, capture_time);
            //Create a SerializableVector3 from the bodypart location
            //Writes the new data into the file
            bf.Serialize(fs, (SerializableVector3)BodyPart.Value.position);
            //Create a SerializableQuaternion from the bodypart rotation
            //Writes the new data into the file
            bf.Serialize(fs, (SerializableQuaternion)BodyPart.Value.rotation);
        }
        bf.Serialize(fs, bp_list);
        capture_time += Time.deltaTime;    }
}
