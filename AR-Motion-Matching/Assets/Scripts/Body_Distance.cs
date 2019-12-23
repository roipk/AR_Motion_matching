using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body_Distance : MonoBehaviour
{
    //distance for each body part.
    float dis_Head;
    float dis_Neck;
    float dis_LeftArm;
    float dis_RightArm;
    float dis_LeftForearm;
    float dis_RightForearm;
    float dis_LeftHand;
    float dis_RightHand;
    float dis_LeftUpLeg;
    float dis_RightUpLeg;
    float dis_LeftLeg;
    float dis_RightLeg;
    float dis_LeftFoot;
    float dis_RightFoot;

    //distance for a group of body parts.
    float dis_FullHead;
    float dis_FullLeftArm;
    float dis_FullRightArm;
    float dis_FullLeftLeg;
    float dis_FullRightLeg;

    //distance of the full body.
    float dis_FullBody;

    Distance dist;

    // Start is called before the first frame update
    void Start()
    {
        dist = gameObject.GetComponent<Distance>();
        
        dis_Head = 0;
        dis_Neck = 0;
        dis_LeftArm = 0;
        dis_LeftFoot = 0;
        dis_LeftForearm = 0;
        dis_LeftHand = 0;
        dis_LeftLeg = 0;
        dis_LeftUpLeg = 0;
        dis_RightArm = 0;
        dis_RightFoot = 0;
        dis_RightForearm = 0;
        dis_RightHand = 0;
        dis_RightLeg = 0;
        dis_RightUpLeg = 0;
        dis_FullBody = 0;
        dis_FullHead = 0;
        dis_FullLeftArm = 0;
        dis_FullLeftLeg = 0;
        dis_FullRightArm = 0;
        dis_FullRightLeg = 0;
    }

    void calc_distance()
    {
        if (HumanBodyTracking.bodyJoints != null)
        {
            foreach (KeyValuePair<JointIndices3D, Transform> item in HumanBodyTracking.bodyJoints)
            {

                switch (item.Key)
                {
                    case JointIndices3D.Head:
                        {
                            dis_Head = dist.object_norm_calc(item.Value, item.Value);
                            break;
                        }
                    case JointIndices3D.Neck1:
                        {
                            dis_Neck = dist.object_norm_calc(item.Value, item.Value);
                            break;
                        }
                    case JointIndices3D.LeftArm:
                        {
                            dis_LeftArm = dist.object_norm_calc(item.Value, item.Value);
                            break;
                        }
                    case JointIndices3D.RightArm:
                        {
                            dis_Head = dist.object_norm_calc(item.Value, item.Value);
                            break;
                        }
                    case JointIndices3D.LeftForearm:
                        {
                            dis_LeftForearm = dist.object_norm_calc(item.Value, item.Value);
                            break;
                        }
                    case JointIndices3D.RightForearm:
                        {
                            dis_RightForearm = dist.object_norm_calc(item.Value, item.Value);
                            break;
                        }
                    case JointIndices3D.LeftHand:
                        {
                            dis_LeftHand = dist.object_norm_calc(item.Value, item.Value);
                            break;
                        }
                    case JointIndices3D.RightHand:
                        {
                            dis_RightHand = dist.object_norm_calc(item.Value, item.Value);
                            break;
                        }
                    case JointIndices3D.LeftUpLeg:
                        {
                            dis_LeftUpLeg = dist.object_norm_calc(item.Value, item.Value);
                            break;
                        }
                    case JointIndices3D.RightUpLeg:
                        {
                            dis_RightUpLeg = dist.object_norm_calc(item.Value, item.Value);
                            break;
                        }
                    case JointIndices3D.LeftLeg:
                        {
                            dis_LeftLeg = dist.object_norm_calc(item.Value, item.Value);
                            break;
                        }
                    case JointIndices3D.RightLeg:
                        {
                            dis_RightLeg = dist.object_norm_calc(item.Value, item.Value);
                            break;
                        }
                    case JointIndices3D.LeftFoot:
                        {
                            dis_LeftFoot = dist.object_norm_calc(item.Value, item.Value);
                            break;
                        }
                    case JointIndices3D.RightFoot:
                        {
                            dis_RightFoot = dist.object_norm_calc(item.Value, item.Value);
                            break;
                        }
                    default: break;
                }
            }

            dis_FullHead = dis_Head + dis_Neck;
            dis_FullLeftArm = dis_LeftArm + dis_LeftForearm + dis_LeftHand;
            dis_FullRightArm = dis_RightArm + dis_RightForearm + dis_RightHand;
            dis_FullLeftLeg = dis_LeftLeg + dis_LeftUpLeg + dis_LeftFoot;
            dis_FullRightLeg = dis_RightLeg + dis_RightUpLeg + dis_RightFoot;

            dis_FullBody = dis_FullHead + dis_FullLeftArm + dis_FullRightArm + dis_FullLeftLeg + dis_FullRightLeg;
        }
    }

}

