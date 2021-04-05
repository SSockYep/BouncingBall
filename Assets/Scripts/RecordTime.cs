using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RecordTime : MonoBehaviour
{
    public float time = 0;
    public TextMeshProUGUI textUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        TimeSpan timeSpan = TimeSpan.FromSeconds(time);
        string timeText = string.Format("{0:D2}:{1:D2}.{2:D1}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds / 100);
        textUI.text = timeText;
    }
}
