using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CateTOTech : MonoBehaviour
{
    public GameObject MainAnimator;
    public GameObject CateAnimator;
    public GameObject Main_Toggle;
    Animator Main_animator, Cate_animator;
    Toggle toggle;
    bool InCate = true;
    bool InMain = true;
    // Start is called before the first frame update
    void Start()
    {
        Main_animator = MainAnimator.GetComponent<Animator>();
        Cate_animator = CateAnimator.GetComponent<Animator>();
        toggle = Main_Toggle.GetComponent<Toggle>();
    }

    // Update is called once per frame
    void Update()
    {
        //If changed to technique panel (changeView is true) let user return with back button
        if (!InCate)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                //Cate_animator.ResetTrigger("CateTech");
                Cate_animator.SetTrigger("TechCate");
                InCate = true;
            }
        }

        bool changeMain = toggle.isOn;
        if (!changeMain && !InMain)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                //Main_animator.ResetTrigger("MainCate");
                Main_animator.SetTrigger("CateMain");
                InMain = true;
            }
        }
    }

    //Changes from category panel to technique panel and the other way around
    public void ChangeView()
    {
        if(CateAnimator != null && Cate_animator != null)
        {
            InCate = false;
            //Cate_animator.ResetTrigger("TechCate");
            Cate_animator.SetTrigger("CateTech");
            //   bool changeView = animator.GetBool("ChangeView");
            //   animator.SetBool("ChangeView", !changeView);
        }
    }

    //Changes from main page to category page based on user decision 
    public void ChangeMain()
    {
        if (MainAnimator != null && Main_animator != null)
        {
            InMain = false;
            //Main_animator.ResetTrigger("CateMain");
            Main_animator.SetTrigger("MainCate");
            //bool changeMain = animator.GetBool("Main");
            //animator.SetBool("Main", !changeMain);
        }
    }
}
