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
    private Regex ruleRegex = new Regex("^[0-8]+$");
    private Rules rules;
    private InputField[] inputs;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.rules = null;
        rules = new Rules { };
        inputs = GetComponentsInChildren<InputField>();
    }

    // Update is called once per frame
    void Update()
    {
        var sliders = GetComponentsInChildren<Slider>();

        Color color = new Color(sliders[0].value, sliders[1].value, sliders[2].value);
        img.color = color;

       
        if (regex.IsMatch(inputs[0].text) && regex.IsMatch(inputs[1].text))
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
        if (ruleRegex.IsMatch(inputs[2].text) && ruleRegex.IsMatch(inputs[3].text))
        {
            inputs[2].GetComponent<Image>().color = Color.white;
            inputs[3].GetComponent<Image>().color = Color.white;
            var liveList = processInput(inputs[2].text);
            var deadlist = processInput(inputs[3].text);
            rules.ForAlive = liveList;
            rules.ForDead = deadlist;
            PlayerPrefs.rules = rules;
            SceneManager.LoadSceneAsync(1);
        }
        else
        {
            inputs[2].GetComponent<Image>().color = Color.red;
            inputs[3].GetComponent<Image>().color = Color.red;
        }
       
          
        
    }
    public void close()
    {
        Application.Quit();
    }

    private List<int> processInput(string input)
    {
        List<int> list = new List<int>();
        var splitted = input.ToCharArray();
        foreach (var item in splitted)
        {
            list.Add((int)char.GetNumericValue(item));
        }
        return list;
    }
}
