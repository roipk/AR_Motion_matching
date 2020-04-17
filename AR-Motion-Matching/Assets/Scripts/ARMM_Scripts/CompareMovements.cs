using System.Collections;using System.Collections.Generic;using System.Linq;using UnityEngine;public class CompareMovements : MonoBehaviour{    ObjectNorm obj_norm;    int feature_length;    private void Start()    {        feature_length = 10;        obj_norm = GameObject.Find("ObjectNorm").GetComponent<ObjectNorm>();    }

    /******     * INPUT PARAMETERS:     *      alpha - A single movement     *      beta - A single movement movement     * OUTPUT:     *      The object norm of the movements     *     ******/
    public float Corr_mult(List<BodySegment> alpha, List<BodySegment> beta)    {        return Mathf.Abs(obj_norm.CompareFrozenMovement(alpha, beta));    }

    /******     * INPUT PARAMETERS:     *      alpha - A list of movements - The target motion     *      beta - A list of movements - the users motion     * OUTPUT:     *      cross-correlation of two movements.     *     ******/
    public int Cross_correlation(List<List<BodySegment>> alpha, List<List<BodySegment>> beta)    {        int small_count = alpha.Count;        bool alpha_small = true;        int start_index = 2, mult_index = 1, moved_index = start_index * mult_index; ;        List<float> distance_list = new List<float>();


        if (beta.Count < alpha.Count)        {            small_count = beta.Count;            alpha_small = false;        }        distance_list.Add(obj_norm.CompareFullMovement(alpha, beta));        while (moved_index + 1 < small_count)        {

            moved_index = start_index * mult_index;            if (alpha_small)                distance_list.Add(obj_norm.CompareFullMovement(alpha, beta.GetRange(moved_index, beta.Count - moved_index)));            else if (!alpha_small)                distance_list.Add(obj_norm.CompareFullMovement(alpha.GetRange(moved_index, alpha.Count - moved_index), beta));            mult_index += 1;        }        for (int i = 0; i < distance_list.Count; i++)        {            Debug.Log("list is: " + distance_list[i]);        }        Debug.Log("cross is: " + distance_list.IndexOf(distance_list.Min()) * start_index);        return distance_list.IndexOf(distance_list.Min()) * start_index;    }


    /******     * INPUT PARAMETERS:     *      alpha - A list of movements - The target motion     *      beta - A list of movements - the users motion     * OUTPUT:     *      The index of the first detected feature.     *     ******/
    public int Feature_Detect(List<List<BodySegment>> alpha, List<List<BodySegment>> beta)    {        float current_obj_norm = 0;        List<float> beta_obj_norm = new List<float>();        if (beta.Count < alpha.Count)        {            Debug.LogError("Users motion shorter than target motion.");            return -2;        }        for (int i = 0; i < beta.Count; i++)
        {            if (i + feature_length < beta.Count && i + feature_length < alpha.Count)            {                for (int j = 0; j < feature_length; j++)                {                    current_obj_norm = Corr_mult(alpha[i + j], beta[i + j]);                }                beta_obj_norm.Add(current_obj_norm);            }        }

        if (beta_obj_norm.Count > 0)        {            Debug.Log("lowest index is: " + beta_obj_norm.IndexOf(beta_obj_norm.Min()));            return beta_obj_norm.IndexOf(beta_obj_norm.Min());        }        return -1;    }}