﻿using System.Collections;
    //Mass in KG
    //Mass of a bodyweight of 80KG
    float avg_mass_head;
    float avg_mass_neck;
    //Radius in cm
    float avg_rad_head;
    //Height in cm
    float avg_height_head;
    //Volume
    float avg_vol_head;
    //cylinder calc
    float cylinder_inerta_number;

    List<KeyValuePair<string, List<float>>> inertia_full_list;
    //mass / rad / height / vol
    List<float> avg_vol_list;
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
        avg_vol_foot = 2865.6032f;
        //load_avg_body_data();

        float BodyDistance = 0;
        //Takes the smallest movement
        int small_movement = alpha.Count;

        return Full_body_distance;
        return inertia_list;