using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTimer : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text timerText;
    public float curruntTime;
    public float curruntMinTime;
    public float curruntHourTime;

    [SerializeField] bool countDown;
    public bool isCounting;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isCounting)
            return;

        curruntTime = countDown ? curruntTime -= Time.deltaTime : curruntTime += Time.deltaTime;
        if(curruntTime >= 60)
        {
            curruntTime -= 60;
            curruntMinTime++;
        }
        if (curruntMinTime >= 60)
        {
            curruntMinTime -= 60;
            curruntHourTime++;
        }
        if(curruntHourTime > 0)
            timerText.text = curruntHourTime.ToString("00") + ":" + curruntMinTime.ToString("00") + ":" + curruntTime.ToString("00.00");
        else
            timerText.text = curruntMinTime.ToString("00") + ":" + curruntTime.ToString("00.00");
    }
}
