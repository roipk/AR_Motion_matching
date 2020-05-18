using System.Collections;using System.Collections.Generic;using System.IO;using UnityEngine;using UnityEngine.SceneManagement;using UnityEngine.UI;

public class FeedbackUI_Handler : MonoBehaviour
{
    public Text move_name;    WheelHandler wheel_hand;    Tech_name technique_name;

    // Start is called before the first frame update
    void Start()    {        wheel_hand = GameObject.Find("WheelHandler").GetComponent<WheelHandler>();        technique_name = GameObject.Find("Selected_tech").GetComponent<Tech_name>();        wheel_hand.score = technique_name.obj_norm_score;        wheel_hand.loading();        update_move_name();    }    void update_move_name()    {        string move_path = Path.GetFileNameWithoutExtension(technique_name.selected_tech_path);        if (move_path != "")            move_name.text = "Comparing your movement to " + Path.GetFileNameWithoutExtension(technique_name.selected_tech_path);
        else            move_name.text = "Failed to find a move to compare to.";    }    public void Load_Same_ARMm()    {        SceneManager.UnloadSceneAsync("Feedback");    }    public void Load_MainUI()    {        SceneManager.UnloadSceneAsync("Feedback");        SceneManager.LoadSceneAsync("MainUI", LoadSceneMode.Additive);    }
}
