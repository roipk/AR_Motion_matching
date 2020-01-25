using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System;
using System.Runtime.Serialization;

public class Data_Handler : MonoBehaviour
{
	string device_path;
	private int frames = 0;
	DateTime current_time;
    	//public GameObject tempCube;
    	string[] rowDataTemp = new string[5];
    	private List<string[]> rowData = new List<string[]>();

	// Start is called before the first frame update
	void Start()
	{
		//Gets the device path
		device_path = getPath();

		Debug.Log("datapath: " + device_path);
		//Creates a CSV
		Create_CSV();
	}

	// Update is called once per frame
	void Update()
	{
		frames++;
		//Writes to the CSV once every 30 frames
		if (frames % 30 == 0){
			if(HumanBodyTracking.Body_flag)
                		Write_CSV();
		}
           frames = 0;
        
	}

    private string get_data_path()
    {
        string datafile = "";
#if UNITY_EDITOR
        datafile = Path.Combine(Application.dataPath, datafile);
#elif UNITY_IPHONE
            return Path.Combine(Application.persistentDataPath, datafile);
#endif
        return datafile;
    }

//Writes the body location to the CSV
    void Create_CSV()
	{
	//Only saves a few of the body locations to not overflow with data
        rowDataTemp[0] = "Time";
        rowDataTemp[1] = "Head_cords";
        rowDataTemp[2] = "Neck_cords";
        rowDataTemp[3] = "LeftArm_cords";
        rowDataTemp[4] = "RightArm_cords";
        rowData.Add(rowDataTemp);
        string datafile = get_data_path();

        StreamWriter outStream = File.CreateText(device_path);
		outStream.Close();
        if(!File.Exists(device_path))
        {   
            Debug.LogError("Didn't create +" + device_path);
        }

    }

    //void Debug_Write_CSV()
    //{

    //    Vector3 Head_vec = Vector3.zero;
    //    rowDataTemp = new string[1];
    //    rowDataTemp[0] = tempCube.transform.position.ToString().Replace(",", "~").Replace(" ","");

    //    string[] output = new string[1];

    //    for (int i = 0; i < output.Length; i++)
    //    {
    //        output = rowDataTemp;
    //    }

    //    int length = output.GetLength(0);
    //    string delimiter = ",";

    //    StringBuilder sb = new StringBuilder();

    //    for (int index = 0; index < length; index++)
    //    {
    //        sb.AppendLine(string.Join(delimiter, output[index]));
    //    }

    //    StreamWriter outStream = new StreamWriter(device_path, true);
    //    outStream.WriteLine(sb);
    //    outStream.Close();
    //    rowData.Clear();
    //    string s = tempCube.transform.position.ToString();
    //    s = s.Replace(",", " ~");
    //    Debug.Log(s);
    //}

    void Write_CSV()
	{
        current_time = DateTime.Now;

        Vector3 Head_vec = Vector3.zero;
        Vector3 Neck_vec = Vector3.zero;
        Vector3 LeftArm_vec = Vector3.zero;
        Vector3 RightArm_vec = Vector3.zero;

        if (HumanBodyTracking.bodyJoints != null)
        {
            foreach (KeyValuePair<JointIndices3D, Transform> item in HumanBodyTracking.bodyJoints)
            {
                
                switch (item.Key)
                {
                    case JointIndices3D.Head:
                        {
                            Head_vec = item.Value.position;
                            break;
                        }
                    case JointIndices3D.Neck1:
                        {
                            Neck_vec = item.Value.position;
                            break;
                        }
                    case JointIndices3D.LeftArm:
                        {
                            LeftArm_vec = item.Value.position;
                            break;
                        }
                    case JointIndices3D.RightArm:
                        {
                            RightArm_vec = item.Value.position;
                            break;
                        }
                    default: break;
                }
            }
        }

	//Changes the string to fit with CSV reqs
        rowDataTemp = new string[5];
        rowDataTemp[0] = current_time.ToString().Replace(",", "~").Replace(" ", ""); 
        rowDataTemp[1] = Head_vec.ToString().Replace(",", "~").Replace(" ", ""); 
        rowDataTemp[2] = Neck_vec.ToString().Replace(",", "~").Replace(" ", ""); 
        rowDataTemp[3] = LeftArm_vec.ToString().Replace(",", "~").Replace(" ", ""); 
        rowDataTemp[4] = RightArm_vec.ToString().Replace(",", "~").Replace(" ", ""); 
        rowData.Add(rowDataTemp);

		string[][] output = new string[rowData.Count][];

		for (int i = 0; i < output.Length; i++)
		{
			output[i] = rowData[i];
		}

		int length = output.GetLength(0);
		string delimiter = ",";

		StringBuilder sb = new StringBuilder();

		for (int index = 0; index < length; index++)
		{
			sb.AppendLine(string.Join(delimiter, output[index]));
		}

		StreamWriter outStream = new StreamWriter(device_path, true);
		outStream.WriteLine(sb);
		outStream.Close();
        rowData.Clear();
	}

    

    private string getPath()
	{
        string body_location = "Saved_data" + DateTime.Now.ToString("dd_MM_yy_HH_mm_ss") + ".csv";
#if UNITY_EDITOR
        return Path.Combine(Application.dataPath+"/DataFiles", body_location);
#else
		return Path.Combine(Application.persistentDataPath+ "/DataFiles", body_location);
#endif
    }

}
