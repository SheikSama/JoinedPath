using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUIController : MonoBehaviour
{

    public Slider slider;

    private float targetProgress = 0;
    public float CDTime = 0;
    private float currentTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            slider.value = currentTime / CDTime;
        }
        else
        {
            slider.gameObject.SetActive(false);
        }
    }

    public void decreasSliderProgress(float CDTime)
    {
        slider.gameObject.SetActive(true);
        this.CDTime = CDTime;
        this.currentTime = CDTime;
    }
}
