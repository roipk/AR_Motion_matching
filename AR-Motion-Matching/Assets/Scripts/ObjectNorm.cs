﻿/*QuickCompare will use object norm to calc the distance between two bodies.
using System.Collections;

public class ObjectNorm : MonoBehaviour
{
    HumanBodyData BodyData;

        float BodyDistance = 0;
                Quaternion quat_diff = Calc_quat_difference(alpha[i].bodypart_quat, beta[i].bodypart_quat);
        //Debug.Log("body: " + BodyDistance);
        return BodyDistance;
        //Loads the numbers of the human body
        BodyData = GameObject.Find("HumanData").GetComponent<HumanBodyData>();
        //Takes the smallest movement
        int small_movement = alpha.Count;
        //Calcs the object norm of every frozen move and sums them up
        for (int i = 0; i < small_movement; i++)
        }
            //Debug.Log("Avg obj is: " + object_norm_list.Average());
            //return full_distance;
            return object_norm_list.Average();

    //Checks it the movement has any static frames, if it does, return a new movement without it.
    public List<List<BodySegment>> RemoveStaticMovement(List<List<BodySegment>> alpha)
        //print("sss: " + alpha.Count);
        for (int i = 1; i < alpha.Count; i++)
                {
            //If all the frames match, set the index and change state to found
            if (same_frame == alpha[i].Count)
        //If the frames are different but found a static movement - return the new skeleton list
        if (static_move)


    //Calcs the quaternion difference bewteen two quaternions
    Quaternion Calc_quat_difference(SerializableQuaternion a, SerializableQuaternion b)

    //Calcs the vector3 difference
    Vector3 Calc_vector_difference(SerializableVector3 a, SerializableVector3 b)
}