using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasScript : MonoBehaviour
{
    public Image img;
    private Regex regex = new Regex("^[0-9]+$");
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var sliders = GetComponentsInChildren<Slider>();

        Color color = new Color(sliders[0].value, sliders[1].value, sliders[2].value);
        img.color = color;

        var inputs = GetComponentsInChildren<InputField>();
        if(  regex.IsMatch(inputs[0].text) && regex.IsMatch(inputs[1].text))
        {
            PlayerPrefs.Heigth = int.Parse(inputs[0].text);
            PlayerPrefs.Width = int.Parse(inputs[1].text);
            inputs[0].GetComponent<Image>().color = Color.white;
            inputs[1].GetComponent<Image>().color = Color.white;
        }
        else
        {
            inputs[0].GetComponent<Image>().color = Color.red;
            inputs[1].GetComponent<Image>().color = Color.red;
        }
        PlayerPrefs.color = color;
        
    }

  public void proceed()
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void close()
    {
        Application.Quit();
    }
}
