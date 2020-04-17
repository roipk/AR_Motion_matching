/*QuickCompare will use object norm to calc the distance between two bodies. *  *  */
using System.Collections;using System.Collections.Generic;using System.Linq;using UnityEngine;

public class ObjectNorm : MonoBehaviour
{
    HumanBodyData BodyData;    public float CompareFrozenMovement(List<BodySegment> alpha, List<BodySegment> beta)    {

        float BodyDistance = 0;        float vol_const = 4;        if (alpha.Count == BodyData.inertia_full_list.Count && beta.Count == BodyData.inertia_full_list.Count)        {            for (int i = 0; i < BodyData.inertia_full_list.Count; i++)            {
                Quaternion quat_diff = Calc_quat_difference(alpha[i].bodypart_quat, beta[i].bodypart_quat);                Vector3 vector_diff = Calc_vector_difference(alpha[i].bodypart_vector, beta[i].bodypart_vector);                BodyDistance += (vol_const / BodyData.avg_vol_list[i].Value) * (BodyData.inertia_full_list[i].Value[0] * Mathf.Pow(quat_diff.x, 2) +                BodyData.inertia_full_list[i].Value[1] * Mathf.Pow(quat_diff.y, 2) +                BodyData.inertia_full_list[i].Value[2] * Mathf.Pow(quat_diff.z, 2)) +                Mathf.Pow(vector_diff.x, 2) +                Mathf.Pow(vector_diff.y, 2) +                Mathf.Pow(vector_diff.z, 2);            }        }
        //Debug.Log("body: " + BodyDistance);
        return BodyDistance;    }    public float CompareFullMovement(List<List<BodySegment>> alpha, List<List<BodySegment>> beta)    {
        //Loads the numbers of the human body
        BodyData = GameObject.Find("HumanData").GetComponent<HumanBodyData>();        List<float> object_norm_list = new List<float>();
        //Takes the smallest movement
        int small_movement = alpha.Count;        if (beta.Count < alpha.Count) small_movement = beta.Count;
        //Calcs the object norm of every frozen move and sums them up
        for (int i = 0; i < small_movement; i++)        {            object_norm_list.Add(CompareFrozenMovement(alpha[i], beta[i]));
        }        if (object_norm_list.Count > 0)        {
            //Debug.Log("Avg obj is: " + object_norm_list.Average());
            //return full_distance;
            return object_norm_list.Average();        }        Debug.LogError("Failed to compare movements.");        return -5;    }

    //Checks it the movement has any static frames, if it does, return a new movement without it.
    public List<List<BodySegment>> RemoveStaticMovement(List<List<BodySegment>> alpha)    {        int static_index = -1;        int same_frame = 0;        bool static_move = false;
        //print("sss: " + alpha.Count);
        for (int i = 1; i < alpha.Count; i++)        {            same_frame = 0;            for (int j = 0; j < alpha[i].Count; j++)            {                if (Comparer(alpha[i][j], alpha[i - 1][j]))
                {                    same_frame++;                }            }
            //If all the frames match, set the index and change state to found
            if (same_frame == alpha[i].Count)            {                static_index = i;                static_move = true;            }            else if (static_move)            {                break;            }        }
        //If the frames are different but found a static movement - return the new skeleton list
        if (static_move)        {            if (static_index > alpha.Count)            {                Debug.LogError("Static index is greater than list length.\n Static index: " + static_index + "\n list Length: " + alpha.Count);                return null;            }            return alpha.GetRange(static_index, alpha.Count - static_index);        }        if (!static_move)        {            return alpha;        }        Debug.LogError("Failed to process and exit - RemoveStaticMovement");        return null;    }    public bool Comparer(BodySegment a, BodySegment b)    {        int cmp_lvl = 0;        if (a.bodypart_name == b.bodypart_name)        {            if (a.bodypart_quat.w == b.bodypart_quat.w && a.bodypart_quat.x == b.bodypart_quat.x && a.bodypart_quat.y == b.bodypart_quat.y && a.bodypart_quat.z == b.bodypart_quat.z)            {                cmp_lvl++;            }            if (a.bodypart_vector.x == b.bodypart_vector.x && a.bodypart_vector.y == b.bodypart_vector.y && a.bodypart_vector.z == b.bodypart_vector.z)            {                cmp_lvl++;            }        }        if (cmp_lvl > 1) return true;        return false;    }


    //Calcs the quaternion difference bewteen two quaternions
    Quaternion Calc_quat_difference(SerializableQuaternion a, SerializableQuaternion b)    {        Quaternion a_euler = a;        Quaternion b_euler = b;        return (a * Quaternion.Inverse(b)).normalized;    }

    //Calcs the vector3 difference
    Vector3 Calc_vector_difference(SerializableVector3 a, SerializableVector3 b)    {        return Vector3.Scale(new Vector3(a.x - b.x, a.y - b.y, a.z - b.z), new Vector3(a.x - b.x, a.y - b.y, a.z - b.z));    }
}
