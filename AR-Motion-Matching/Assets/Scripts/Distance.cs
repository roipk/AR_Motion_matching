using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distance : MonoBehaviour
{
   
    float q1, q2, q3;
    float q4, q5, q6;

    // Start is called before the first frame update
    void Start()
    {
        q1 = 0;
        q2 = 0;
        q3 = 0;
        q4 = 0;
        q5 = 0;
        q6 = 0; 
    }

    public float object_norm_calc(Transform A, Transform B)
    {
        Quaternion relative = Quaternion.Inverse(A.rotation) * B.rotation;
        q1 = relative.x;
        q2 = relative.y;
        q3 = relative.z;

        q4 = A.position.x - B.position.x;
        q5 = A.position.y - B.position.y;
        q6 = A.position.z - B.position.z;

        return q1 + q2 + q3 + q4 + q5 + q6;
    }

}
