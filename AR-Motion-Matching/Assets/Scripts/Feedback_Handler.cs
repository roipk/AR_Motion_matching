using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feedback_Handler : MonoBehaviour
{
   public void send_to_feedback()
    {
        Application.OpenURL("https://forms.gle/rPksQM4pY5JyR4XX6");
    }
}
