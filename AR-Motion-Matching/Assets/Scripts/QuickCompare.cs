using System.Collections;using System.Collections.Generic;using UnityEngine;public class QuickCompare : MonoBehaviour{
    //Mass in KG
    //Mass of a bodyweight of 80KG
    float avg_mass_head;
    float avg_mass_neck;    float avg_mass_arm;    float avg_mass_forearm;    float avg_mass_hand;    float avg_mass_upleg;    float avg_mass_leg;    float avg_mass_foot;
    //Radius in cm
    float avg_rad_head;    float avg_rad_neck;    float avg_rad_arm;    float avg_rad_forearm;    float avg_rad_hand;    float avg_rad_upleg;    float avg_rad_leg;    float avg_rad_foot;
    //Height in cm
    float avg_height_head;    float avg_height_neck;    float avg_height_arm;    float avg_height_forearm;    float avg_height_hand;    float avg_height_upleg;    float avg_height_leg;    float avg_height_foot;
    //Volume
    float avg_vol_head;    float avg_vol_neck;    float avg_vol_arm;    float avg_vol_forearm;    float avg_vol_hand;    float avg_vol_upleg;    float avg_vol_leg;    float avg_vol_foot;
    //cylinder calc
    float cylinder_inerta_number;

    List<KeyValuePair<string, List<float>>> inertia_full_list;
    //mass / rad / height / vol
    List<float> avg_vol_list;    float Full_body_distance = 0;    void load_avg_body_data()    {
        avg_mass_head = 6.58f;
        avg_mass_neck = 1.92f;
        avg_mass_arm = 2.104f;
        avg_mass_forearm = 1.5f;
        avg_mass_hand = 0.46f;
        avg_mass_upleg = 11.576f;
        avg_mass_leg = 2f;
        avg_mass_foot = 1.064f;
        //Radius in cm
        avg_rad_head = 8.572f;
        avg_rad_neck = 9.284f;
        avg_rad_arm = 3.7736f;
        avg_rad_forearm = 5.74f;
        avg_rad_hand = 7.031f;
        avg_rad_upleg = 12.48f;
        avg_rad_leg = 7.984f;
        avg_rad_foot = 6.544f;
        //Height in cm
        avg_height_head = 23.39f;
        avg_height_neck = 18.2f;
        avg_height_arm = 35.3f;
        avg_height_forearm = 31.4f;
        avg_height_hand = 18.2f;
        avg_height_upleg = 41.9f;
        avg_height_leg = 42.3f;
        avg_height_foot = 21.3f;
        //Volume
        avg_vol_head = 5399.3865f;
        avg_vol_neck = 4928.2363f;
        avg_vol_arm = 1579.197f;
        avg_vol_forearm = 3250.1493f;
        avg_vol_hand = 2826.5421f;
        avg_vol_upleg = 20501.8507f;
        avg_vol_leg = 8470.934f;
        avg_vol_foot = 2865.6032f;        cylinder_inerta_number = 1 / 12;        inertia_full_list = new List<KeyValuePair<string, List<float>>>();        avg_vol_list = new List<float>();        avg_vol_list.Add(avg_vol_head);        avg_vol_list.Add(avg_vol_neck);        avg_vol_list.Add(avg_vol_arm);        avg_vol_list.Add(avg_vol_forearm);        avg_vol_list.Add(avg_vol_hand);        avg_vol_list.Add(avg_vol_upleg);        avg_vol_list.Add(avg_vol_leg);        avg_vol_list.Add(avg_vol_foot);        inertia_full_list.Add(new KeyValuePair<string, List<float>>("head", Calc_inertia(avg_rad_head, avg_height_head, avg_mass_head)));        inertia_full_list.Add(new KeyValuePair<string, List<float>>("neck1", Calc_inertia(avg_rad_neck, avg_height_neck, avg_mass_neck)));        inertia_full_list.Add(new KeyValuePair<string, List<float>>("LeftArm", Calc_inertia(avg_rad_arm, avg_height_arm, avg_mass_arm)));        inertia_full_list.Add(new KeyValuePair<string, List<float>>("RightArm", Calc_inertia(avg_rad_arm, avg_height_arm, avg_mass_arm)));        inertia_full_list.Add(new KeyValuePair<string, List<float>>("LeftForearm", Calc_inertia(avg_rad_forearm, avg_height_forearm, avg_mass_forearm)));        inertia_full_list.Add(new KeyValuePair<string, List<float>>("RightForearm", Calc_inertia(avg_rad_forearm, avg_height_forearm, avg_mass_forearm)));        inertia_full_list.Add(new KeyValuePair<string, List<float>>("LeftHand", Calc_inertia(avg_rad_hand, avg_height_hand, avg_mass_hand)));        inertia_full_list.Add(new KeyValuePair<string, List<float>>("RightHand", Calc_inertia(avg_rad_hand, avg_height_hand, avg_mass_hand)));        inertia_full_list.Add(new KeyValuePair<string, List<float>>("LeftUpLeg", Calc_inertia(avg_rad_upleg, avg_height_upleg, avg_mass_upleg)));        inertia_full_list.Add(new KeyValuePair<string, List<float>>("RightUpLeg", Calc_inertia(avg_rad_upleg, avg_height_upleg, avg_mass_upleg)));        inertia_full_list.Add(new KeyValuePair<string, List<float>>("LeftLeg", Calc_inertia(avg_rad_leg, avg_height_leg, avg_mass_leg)));        inertia_full_list.Add(new KeyValuePair<string, List<float>>("RightLeg", Calc_inertia(avg_rad_leg, avg_height_leg, avg_mass_leg)));        inertia_full_list.Add(new KeyValuePair<string, List<float>>("LeftFoot", Calc_inertia(avg_rad_foot, avg_height_foot, avg_mass_foot)));        inertia_full_list.Add(new KeyValuePair<string, List<float>>("RightFoot", Calc_inertia(avg_rad_foot, avg_height_foot, avg_mass_foot)));    }    public float CompareFrozenMovement(List<BodySegment> alpha, List<BodySegment> beta)    {
        //load_avg_body_data();

        float BodyDistance = 0;        if (alpha.Count == 1 && beta.Count == 1)        {            for (int i = 0; i < inertia_full_list.Count; i++)            {                Debug.Log(alpha[i].bodypart_name);                BodyDistance += 4 / avg_vol_list[i] * (inertia_full_list[i].Value[0] * (Calc_quat_difference(alpha[i].bodypart_quat, beta[i].bodypart_quat).eulerAngles.x) +                inertia_full_list[i].Value[1] * (Calc_quat_difference(alpha[i].bodypart_quat, beta[i].bodypart_quat).eulerAngles.y) +                inertia_full_list[i].Value[2] * (Calc_quat_difference(alpha[i].bodypart_quat, beta[i].bodypart_quat).eulerAngles.z)) +                Calc_vector_difference(alpha[i].bodypart_vector, beta[i].bodypart_vector).x +                Calc_vector_difference(alpha[i].bodypart_vector, beta[i].bodypart_vector).y +                Calc_vector_difference(alpha[i].bodypart_vector, beta[i].bodypart_vector).z;            }        }        return BodyDistance;    }    public float CompareFullMovement(List<List<BodySegment>> alpha, List<List<BodySegment>> beta)    {        load_avg_body_data();
        //Takes the smallest movement
        int small_movement = alpha.Count;        if (beta.Count < alpha.Count) small_movement = beta.Count;        for (int i = 0; i < small_movement; i++)        {            Full_body_distance += CompareFrozenMovement(alpha[i], beta[i]);        }

        return Full_body_distance;    }    Quaternion Calc_quat_difference(SerializableQuaternion a, SerializableQuaternion b)    {        Quaternion a_euler = a;        Quaternion b_euler = b;        return Quaternion.Euler(a_euler.x - b_euler.x, a_euler.y - b_euler.y, a_euler.z - b_euler.z).normalized * Quaternion.Euler(a_euler.x - b_euler.x, a_euler.y - b_euler.y, a_euler.z - b_euler.z).normalized;    }    Vector3 Calc_vector_difference(SerializableVector3 a, SerializableVector3 b)    {        return Vector3.Scale(new Vector3(a.x - b.x, a.y - b.y, a.z - b.z), new Vector3(a.x - b.x, a.y - b.y, a.z - b.z));    }    List<float> Calc_inertia(float r, float h, float m)    {        List<float> inertia_list = new List<float>();        inertia_list.Add(cylinder_inerta_number * m * (3 * Mathf.Pow(r, 3) + Mathf.Pow(h, 2)));        inertia_list.Add(inertia_list[0]);        inertia_list.Add(0.5f * m * Mathf.Pow(r, 2));
        return inertia_list;    }}