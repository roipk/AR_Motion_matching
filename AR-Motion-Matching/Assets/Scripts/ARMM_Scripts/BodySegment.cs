/***
BodySegment is a struct that holds a single body part data.
Its constructed as the following:
bodypart_name: The name of the body part.
capture_time: The time of the capture.
bodypart_vector: The vector data of the body part.
bodypart_quat: The quaternion of the body part.

BodySegment is Serialized in order for it to be written in a file.
***/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct BodySegment 
{
    public string bodypart_name;

    public float capture_time;

    public SerializableVector3 bodypart_vector;

    public SerializableQuaternion bodypart_quat;

    //The Constructor
    public BodySegment(string bp_name, float c_time, SerializableVector3 vector, SerializableQuaternion quat)
    {
        bodypart_name = bp_name;
        capture_time = c_time;
        bodypart_vector = vector;
        bodypart_quat = quat;
    }
    
    //ToString override
    public override string ToString()
    {
        return string.Format("BodyPart name: {0} \n Capture time: {1} \n BodyPart vector: {2} \n BodyPart rotation: {3}", bodypart_name, capture_time, bodypart_vector, bodypart_quat);
    }
}
