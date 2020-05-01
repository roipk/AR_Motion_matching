    /*************
 *This class handles the main UI components.
 *The basic flow of the UI will run from here.
 *Sends to ARMm the path to the selected technique
 *
 * ***********/

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ViewHandler : MonoBehaviour
{
    public GameObject Animator;
    public GameObject Exit;
    public GameObject cath_btn;
    public GameObject tech_btn;
    GameObject content_cate, content_tech;
    Tech_name selected_technique;
    Animator animator;
    Button exit_button;
    List<KeyValuePair<string, string>> Cate_list;
    List<KeyValuePair<string, string>> Cate_list_takwando;
    List<KeyValuePair<string, string>> Cate_list_karate;
    bool InCate = false;
    bool InMain = true;
    bool InTech = false;
    string cate_path = "/Resources/Category/";
    string tech_path = "/MotionsDB/";
    string path_way;
    public string tech_folder;
    List<string> Martial_List;

    List<GameObject> cate_item_holder;

    // Start is called before the first frame update
    void Start()
    {
        Martial_List = new List<string>();
        
        animator = Animator.GetComponent<Animator>();
        content_cate = GameObject.Find("Content_Cate");
        content_tech = GameObject.Find("Content_Tech");
        selected_technique = GameObject.Find("Selected_tech").GetComponent<Tech_name>();
        Cate_list = new List<KeyValuePair<string, string>>();
        Cate_list_karate = new List<KeyValuePair<string, string>>();
        Cate_list_takwando = new List<KeyValuePair<string, string>>();
        cate_item_holder = new List<GameObject>();
        create_cate_list();
        Cate_JSON_maker();
        set_marital_list();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ResetTrigger();
            //Trying to exit from main page
            if (InMain)
            {
                Exit.SetActive(true);
            }
            //Trying to exit from category page
            else if (InCate)
            {
                Cate_list = null;   
                for (int i = 0; i < cate_item_holder.Count; i++)
                {
                    Destroy(cate_item_holder[i]);
                }
                InCate = false;
                InMain = true;
                animator.SetTrigger("CateMain");
            }
            //Try to exit from technique page
            else if (InTech)
            {
                InTech = false;
                InCate = true;
                animator.SetTrigger("TechCate");
            }

        }
    }

    void set_marital_list()
    {
        Martial_List.Add("null");
        Martial_List.Add("Takwando");
        Martial_List.Add("Karate");
    }

    public void ExitApp()
    {
        Application.Quit();
    }

    private void MartialMode(int mode)
    {
        tech_path += Martial_List[mode] + "/";
        //mode == 1-> takwando
        if (mode == 1)
        {
            Cate_list = Cate_list_takwando;
            

        }
        //mode == 2 -> Karate
        else if(mode == 2)
        {
            Cate_list = Cate_list_karate;
        }
        else
        {
            Debug.LogError("Incorrect martial arts mode.");
        }
    }

    private void create_cate_list()
    {
        
#if UNITY_EDITOR
         path_way = "Assets";
#elif UNITY_IOS
        path_way = Application.persistentDataPath;
#elif UNITY_ANDROID
        path_way = Application.persistentDataPath;
#endif

        Cate_list_takwando.Add(new KeyValuePair<string, string>("Kick_Techniques", path_way + cate_path + "cate_kick.png"));
        Cate_list_takwando.Add(new KeyValuePair<string, string>("Hand_Techniques", path_way + cate_path + "cate_punch.png"));
        Cate_list_takwando.Add(new KeyValuePair<string, string>("Stances", path_way + cate_path + "cate_stance.png"));
        Cate_list_takwando.Add(new KeyValuePair<string, string>("Combinations_Techniques", path_way + cate_path + "cate_combination.png"));
        Cate_list_takwando.Add(new KeyValuePair<string, string>("Poomase", path_way + cate_path + "cate_poomase.png"));

        Cate_list_karate.Add(new KeyValuePair<string, string>("Kick_Techniques", path_way + cate_path + "cate_kick.png"));
        Cate_list_karate.Add(new KeyValuePair<string, string>("Hand_Techniques", path_way + cate_path + "cate_punch.png"));
        Cate_list_karate.Add(new KeyValuePair<string, string>("Stances", path_way + cate_path + "cate_stance.png"));
        Cate_list_karate.Add(new KeyValuePair<string, string>("Combinations_Techniques", path_way + cate_path + "cate_combination.png"));
        Cate_list_karate.Add(new KeyValuePair<string, string>("Kata", path_way + "/Resources/Category/cate_kata.png"));
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
    public void ChangeMain(int mode)
    {
        MartialMode(mode);
        if (Animator != null && animator != null)
        {
            InMain = false;
            InCate = true;
            FillScrollView();
            ResetTrigger();
            animator.SetTrigger("MainCate");
        }
    }

    //Fills the category scroll view with cate buttons
    private void FillScrollView()
    {
        if(content_tech == null || content_cate == null)
        {
            Debug.LogError("Failed to find contents.");
            return;
        }
        GameObject cate_item;
        foreach (KeyValuePair<string, string> item in Cate_list)
        {
            //Creates a prefab of the category button
            cate_item = Instantiate(cath_btn);
            //Sets the item to be a child of the scrollview content
            cate_item.transform.SetParent(content_cate.transform);
            //Finds the first component which is a text and change its propierty 
            cate_item.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = item.Key;
            //Finds the image component and change its image
            cate_item.transform.GetChild(0).transform.GetChild(1).GetComponent<Image>().sprite = Cate_image_filler(item.Value);
            //Changes the name of the object
            cate_item.transform.name = item.Key;
            //Adds an onclick listener 
            cate_item.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(Selected_category);
            cate_item_holder.Add(cate_item);
        }
    }

    void Selected_category()
    {
        tech_folder = path_way + tech_path + ((EventSystem.current.currentSelectedGameObject.GetComponentInParent<Transform>()).parent.name);
        FillTechScrollView();
        ChangeView();
    }

    private void FillTechScrollView()
    {
        if(tech_folder == null && !Directory.Exists(tech_folder))
        {
            Debug.LogError("Tech_Folder is empty or doesnt exists");
            return;
            
        }
        DirectoryInfo tech_files = new DirectoryInfo(tech_folder);
        FileInfo[] tech_items = tech_files.GetFiles("*.dat");
        if(tech_items.Length < 1)
        {
            Debug.LogError("No techniques in folder!");
            return;
        }
        GameObject tech_item;
        foreach(FileInfo item in tech_items)
        {
            tech_item = Instantiate(tech_btn);
            tech_item.transform.SetParent(content_tech.transform);
            tech_item.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = Path.GetFileNameWithoutExtension(item.Name);
            tech_item.transform.name = Path.GetFileNameWithoutExtension(item.Name);
            tech_item.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(load_selected_tech);
        }
    }

    void load_selected_tech()
    {
        selected_technique.selected_tech_path = Path.Combine(tech_folder, (EventSystem.current.currentSelectedGameObject.GetComponentInParent<Transform>()).parent.name);
        Debug.Log("technqiue chosen is: " + selected_technique.selected_tech_path);
        loadARMm();
    }

    private Sprite Cate_image_filler(string path)
    {
        Texture2D texture = null;
        Byte[] file = File.ReadAllBytes(path);
        texture = new Texture2D(2, 2, TextureFormat.RGB24, false);
        texture.LoadImage(file);
        return Sprite.Create(texture, new Rect(0,0, texture.width, texture.height), new Vector2(0.5f,0.5f) );
    }

    private void ResetTrigger()
    {
        animator.ResetTrigger("CateTech");
        animator.ResetTrigger("CateMain");
        animator.ResetTrigger("MainCate");
        animator.ResetTrigger("CateMain");
    }

    public void loadARMm()
    {
        SceneManager.LoadScene("ARMm", LoadSceneMode.Single);
    }




    /*  private void Cate_JSON_maker()
      {
          if (File.Exists("/Data_Files/Cate_main"))
          {
              return;
          }

          //File.Create("/Data_Files/Cate_main");
          Cate_main test = new Cate_main();
          cate_name cate_list = new cate_name("basic");
          cate_list.tech_list = new List<tech_item>();
          test.cate_list = new List<cate_name>();
          tech_item punch = new tech_item("punch");
          tech_item kick = new tech_item("kick");


          cate_list.tech_list.Add(punch);
          Debug.Log(cate_list.tech_list[0].tech_name);
          cate_list.tech_list.Add(kick);
          Debug.Log(cate_list.tech_list[1].tech_name);

          test.cate_list.Add(cate_list);
          Debug.Log(test.cate_list[0].tech_list[0].tech_name);
          string s = JsonUtility.ToJson(test);
          Debug.Log(s);
          Cate_main main_2 = JsonUtility.FromJson<Cate_main>(s);
          Debug.Log(main_2.cate_list[0].tech_list[0].tech_name);


      }
      [Serializable]
      public class Cate_main
      {
          public List<cate_name> cate_list; 
      }
      [Serializable]
      public class cate_name
      {
          public string cate_title;
          public List<tech_item> tech_list;
          public cate_name(string name)
          {
              cate_title = name;
          }
      }
      [Serializable]
      public class tech_item
      {
          public string tech_name;
          public tech_item (string tech)
          {
              tech_name = tech;
          }
      }
      */



}
