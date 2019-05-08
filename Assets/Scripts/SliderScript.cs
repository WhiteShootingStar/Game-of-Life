using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    public Text text,startText;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Slider>().value = 0.1f;
        startText.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Point.toStart == true)
        {
            startText.gameObject.SetActive(false);
        }
        Initializing.WaitTime = GetComponent<Slider>().value*2;
        text.text = "Adjust the speed of life. Current speed is " +(2- GetComponent<Slider>().value * 2);
    }
}
