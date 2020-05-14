using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tech_name : MonoBehaviour
{
    public string selected_tech_path;
    public float obj_norm_score;
    static bool created = false;

    private void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }
        else
        {
            Destroy(this.gameObject);
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        selected_tech_path = "";
        obj_norm_score = -1;
    }

}
