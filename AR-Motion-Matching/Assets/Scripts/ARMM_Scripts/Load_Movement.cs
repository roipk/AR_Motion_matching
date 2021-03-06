﻿using System;using System.Collections;using System.Collections.Generic;using System.IO;using System.Runtime.Serialization.Formatters.Binary;using UnityEngine;public class Load_Movement : MonoBehaviour{    public List<List<BodySegment>> Loaded_movement;    public List<BodySegment> Frame_List;    HumanBodyData BodyData;

    // Start is called before the first frame update
    void Start()    {        Loaded_movement = new List<List<BodySegment>>();        Frame_List = new List<BodySegment>();        BodyData = GameObject.Find("HumanData").GetComponent<HumanBodyData>();    }    public void Load_data(string path)    {

        if (File.Exists(path))        {            try            {                BinaryFormatter bf = new BinaryFormatter();                FileStream tech_file = File.OpenRead(path);                StartCoroutine(read_file_data(tech_file, bf));



                int index = 0;                if (Frame_List.Count > 0)                {                    for (int i = 0; i < Frame_List.Count / BodyData.body_part_names.Count; i++)                    {                        List<BodySegment> Single_Frame = new List<BodySegment>();                        for (int j = 0; j < BodyData.body_part_names.Count; j++)                        {                            if (index < Frame_List.Count)                            {                                Single_Frame.Add(Frame_List[index]);                                index += 1;                            }                        }                        Loaded_movement.Add(Single_Frame);                    }                }

            }            catch (Exception e)            {                Debug.LogError("Error in Load_data: " + e);            }

        }        else        {            Debug.LogError("Didnt find path: " + path);        }    }    IEnumerator read_file_data(FileStream tech_file, BinaryFormatter bf)    {        while (tech_file.Position != tech_file.Length)        {            Frame_List = (List<BodySegment>)bf.Deserialize(tech_file);        }        tech_file.Close();        yield return new WaitForSeconds(1);    }}