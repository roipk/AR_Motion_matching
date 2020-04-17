﻿using System.Collections;
    //average stats of a bodyweight of 80KG
    //Mass in KG
    public List<KeyValuePair<string, float>> avg_mass_list;
    //rad in meter
    public List<KeyValuePair<string, float>> avg_rad_list;
    //height in cm
    public List<KeyValuePair<string, float>> avg_height_list;
    //vol in meter ^3
    public List<KeyValuePair<string, float>> avg_vol_list;
    //mass / rad / height / vol 
    public List<KeyValuePair<string, List<float>>> inertia_full_list;
        //Debug.Log("r;  " + r + " h: " + h + " m" + m) ; 
        List<float> inertia_list = new List<float>();
        //Debug.Log("inertia x: " + inertia_list[0] + " y: " + inertia_list[1] + " z: " + inertia_list[2]);   
        return inertia_list;