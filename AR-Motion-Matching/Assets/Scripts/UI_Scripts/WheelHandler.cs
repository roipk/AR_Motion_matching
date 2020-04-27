using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WheelHandler : MonoBehaviour
{
    public int level = 70;    public Text text;    public Image image_color;

    // Use this for initialization
    void Start()
    {        image_color.fillAmount = 0.0f;    }    public void loading()    {        StartCoroutine(set_color());

    }    IEnumerator set_color()    {        float lerp_time = 1 / 69f;        float lerp_time2 = 1 / 26f;        int fill_level = 0;        while (fill_level < level)        {            fill_level++;            image_color.fillAmount += 0.01f;            if (fill_level > 0 && fill_level <= 69)            {                image_color.color = Color.Lerp(Color.red, Color.yellow, lerp_time);                lerp_time += 1 / 69f;
                //image_color.color = Color.red;
            }            else if (fill_level > 69 && fill_level <= 95)            {                image_color.color = Color.Lerp(Color.yellow, Color.green, lerp_time2);                lerp_time2 += 1 / 26f;
                //image_color.color = Color.yellow;
            }            else if (fill_level > 95 && fill_level <= 100)            {                image_color.color = Color.green;            }            text.text = fill_level + "%";            yield return new WaitForSeconds(0.05f);        }    }
}
