using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerText : MonoBehaviour
{
    public Text counterText;
    public float sec, min;
    // Start is called before the first frame update
    void Start()
    {
        counterText = GetComponent<Text>() as Text; 
    }

    // Update is called once per frame
    void Update()
    {
        min = (int)(Time.timeSinceLevelLoad / 60f);
        sec = (int)(Time.timeSinceLevelLoad % 60f);
        counterText.text = min.ToString("00") + ":" + sec.ToString("00");
    }

    public string current()
    {
        min = (int)(Time.timeSinceLevelLoad / 60f);
        sec = (int)(Time.timeSinceLevelLoad % 60f);
        counterText.text = min.ToString("00") + ":" + sec.ToString("00");
        gameObject.SetActive(false);
        return counterText.text;
    }
}
