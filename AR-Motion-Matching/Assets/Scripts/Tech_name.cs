    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;


    public class Tech_name : MonoBehaviour
    {
        public string selected_tech_path;
        public float obj_norm_score;
    public bool DEBUG_MODE;
        UnityEngine.XR.ARFoundation.ARSessionState ARState;
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
            //ARState = UnityEngine.XR.ARFoundation.ARSession.state;
            selected_tech_path = "";
            obj_norm_score = 125f;
            DEBUG_MODE = false;
        }

        //private void Update()
        //{
        
        //    if(ARState != UnityEngine.XR.ARFoundation.ARSession.state)
        //    {
        //        Debug.Log("ARSessionState: " + UnityEngine.XR.ARFoundation.ARSession.state);
        //        Debug.Log("ARsessionState changed, checking aviliablity");
        //        UnityEngine.XR.ARFoundation.ARSession.CheckAvailability();
        //        Debug.Log("New ARSessionState: " + UnityEngine.XR.ARFoundation.ARSession.state);
            
        //    }
        //    ARState = UnityEngine.XR.ARFoundation.ARSession.state;
        //}

    }
