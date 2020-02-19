using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class AnimationCreator : MonoBehaviour
{
    Animation anim;
    AnimationClip clip;
    bool start = false;
    float time = 0.0f;
    AnimationHolder holder;
    public Button start_btn;
    public Button fin_btn;
    public InputField file_name;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animation>();
        clip = new AnimationClip();
        clip.legacy = true;
        holder = new AnimationHolder();
        start_btn.onClick.AddListener(Start_Record);
        fin_btn.onClick.AddListener(Stop_Record);
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            //Add each position as a key frame.
            holder.pos_x = new Keyframe(time, transform.position.x);
            holder.pos_y = new Keyframe(time, transform.position.y);
            holder.pos_z = new Keyframe(time, transform.position.z);

            holder.rot_x = new Keyframe(time, transform.rotation.x);
            holder.rot_y = new Keyframe(time, transform.rotation.y);
            holder.rot_z = new Keyframe(time, transform.rotation.z);
            
            //Each keyframe must have its own curve.
            holder.curve_posx.AddKey(holder.pos_x);
            clip.SetCurve("", typeof(Transform), "localPosition.x", holder.curve_posx);
            holder.curve_posy.AddKey(holder.pos_y);
            clip.SetCurve("", typeof(Transform), "localPosition.y", holder.curve_posy);
            holder.curve_posz.AddKey(holder.pos_z);
            clip.SetCurve("", typeof(Transform), "localPosition.z", holder.curve_posz);

            holder.curve_rotx.AddKey(holder.rot_x);
            clip.SetCurve("", typeof(Transform), "localRotation.x", holder.curve_rotx);
            holder.curve_roty.AddKey(holder.rot_y);
            clip.SetCurve("", typeof(Transform), "localRotation.y", holder.curve_roty);
            holder.curve_rotz.AddKey(holder.rot_z);
            clip.SetCurve("", typeof(Transform), "localRotation.z", holder.curve_rotz);

            //Change time every update
            time += Time.deltaTime;
        }
    }

    void Start_Record()
    {
        start = true;
    }

    void Stop_Record()
    {
        if (start)
        {
            if (file_name.text.Equals("")) file_name.text = "test";
            AssetDatabase.CreateAsset(clip, "Assets/TargetMotionDB/" + file_name.text + ".anim");
        }
        start = false;
    }

    public class AnimationHolder
    {
        //Keyframes of the position
        public Keyframe pos_x;
        public Keyframe pos_y;
        public Keyframe pos_z;
        //Keyframes of the rotation
        public Keyframe rot_x;
        public Keyframe rot_y;
        public Keyframe rot_z;
        //The curves of the keyframes
        public AnimationCurve curve_posx = new AnimationCurve();
        public AnimationCurve curve_posy = new AnimationCurve();
        public AnimationCurve curve_posz = new AnimationCurve();
        public AnimationCurve curve_rotx = new AnimationCurve();
        public AnimationCurve curve_roty = new AnimationCurve();
        public AnimationCurve curve_rotz = new AnimationCurve();
    }
        
}
