using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewHandler : MonoBehaviour
{
    public GameObject Animator;
    public GameObject Exit;
    Animator animator;
    Button exit_button;
    
    bool InCate = false;
    bool InMain = true;
    bool InTech = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = Animator.GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ResetTrigger();
            if (InMain)
            {
                Exit.SetActive(true);
            }
            else if (InCate)
            {

                InCate = false;
                InMain = true;
                animator.SetTrigger("CateMain");
            }
            else if (InTech)
            {
                InTech = false;
                InCate = true;
                animator.SetTrigger("TechCate");
            }

        }
    }
    public void ExitApp()
    {
        Application.Quit();
    }

    //Changes from category panel to technique panel and the other way around
    public void ChangeView()
    {
        if (Animator != null && animator != null)
        {
            InCate = false;
            InTech = true;
            ResetTrigger();
            animator.SetTrigger("CateTech");
        }
    }

    //Changes from main page to category page based on user decision 
    public void ChangeMain()
    {
        if (Animator != null && animator != null)
        {
            InMain = false;
            InCate = true;
            ResetTrigger();
            animator.SetTrigger("MainCate");
        }
    }

    private void ResetTrigger()
    {
        animator.ResetTrigger("CateTech");
        animator.ResetTrigger("CateMain");
        animator.ResetTrigger("MainCate");
        animator.ResetTrigger("CateMain");
    }
}
