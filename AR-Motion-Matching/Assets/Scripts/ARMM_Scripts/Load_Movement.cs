using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Load_Movement : MonoBehaviour
{

    public List<List<BodySegment>> Loaded_movement;
    List<BodySegment> Frame_List;
    HumanBodyData BodyData;
  
    // Start is called before the first frame update
    void Start()
    {
        Loaded_movement = new List<List<BodySegment>>();
        Frame_List = new List<BodySegment>();
        BodyData = GameObject.Find("HumanData").GetComponent<HumanBodyData>();
    }


    public IEnumerator Load_data(string path)
    {
        if (File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream tech_file = File.Open(path, FileMode.Open);
            while (tech_file.Position != tech_file.Length)            {
                Frame_List = (List<BodySegment>)bf.Deserialize(tech_file);
                
            }
            yield return new WaitForSeconds(1);

            tech_file.Close();

            int index = 0;
            if (Frame_List.Count > 0)
            {
                for (int i = 0; i < Frame_List.Count / BodyData.body_part_names.Count; i++)
                {
                    List<BodySegment> Single_Frame = new List<BodySegment>();
                    for (int j = 0; j < BodyData.body_part_names.Count; j++)
                    {
                        if (index < Frame_List.Count)
                        {
                            Single_Frame.Add(Frame_List[index]);
                            index += 1;
                        }

                    }
                    Loaded_movement.Add(Single_Frame);
                }
            }
        }
        else
        {
            Debug.LogError("Didnt find path: " + path);
        }
    }


}
