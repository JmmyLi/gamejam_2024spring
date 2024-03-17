using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] public TMP_Text text;
    
    [SerializeField] public float time;
    [SerializeField] public float timeSpeed;
    [SerializeField] public int displayInterval;
    [SerializeField] public int minute;
    [SerializeField] public int hour;


    // Update is called once per frame
    void FixedUpdate()
    {
        time += Time.deltaTime * timeSpeed;
        hour = Mathf.FloorToInt(time / 60);
        minute = Mathf.FloorToInt(Mathf.FloorToInt((time - hour * 60) / displayInterval) * displayInterval);

        text.text = string.Format("{0:00}:{1:00}", hour % 24, minute);
    }

    public void Restart(float startHour)
    {
        time = startHour * 60;
    }
}
