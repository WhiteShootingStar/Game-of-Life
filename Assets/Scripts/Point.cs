using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public static bool toStart = false;
    public static Point[,] array;
    public int xPos, yPos;
    public Material material;
    public bool started = false;
    public bool isAlive = false;
    public static MouseDrawMode drawMode;
    private Color color = PlayerPrefs.color;
  
    //public static
    // Start is called before the first frame update

    public enum MouseDrawMode
    {
        Pressing, Hoovering
    }
    void Start()
    {
        material = GetComponent<Renderer>().material;
        drawMode = MouseDrawMode.Pressing;
        material.color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive == true && material.color.Equals(Color.white))
        {
            material.color = color;
        }
        else if( isAlive==false && material.color.Equals(color))
        {
            material.color = Color.white;
        }
        //if (started == false && toStart == true)
        //{

        //    StartCoroutine(changecolor());
        //    started = true;
        //}
        //print(started);
    }
    private void OnMouseDown()
    {
        if (toStart == false && drawMode.Equals(MouseDrawMode.Pressing))
        {
            if (material.color.Equals(Color.white))
            {
                // material.color = Color.red;
                isAlive = true;
            }
            else
            {
                isAlive = false;
                //material.color = Color.white; 

            }
        }
       
    }
    private void OnMouseOver()
    {
        if (toStart == false && drawMode.Equals(MouseDrawMode.Hoovering))
        {
            if (material.color.Equals(Color.white))
            {
                // material.color = Color.red;
                isAlive = true;
            }
           
        }
    }
   

    IEnumerator changecolor()
    {
        if (toStart == true)
        {
            while (true)
            {
                yield return new WaitForSeconds(0.5f);
                if (hasToDie())
                {
                    array[xPos, yPos].GetComponent<Renderer>().material.color = color;
                }
                else
                {
                    array[xPos, yPos].GetComponent<Renderer>().material.color = Color.white;
                }
            }
        }

    }

    public bool hasToDie()
    {
        int counter = 0;
        if (material.color.Equals(color))
        {
            for (int i = yPos - 1; i <= yPos + 1; i++)
            {
                for (int k = xPos - 1; k <= xPos + 1; k++)
                {
                    int y = i;
                    int x = k;
                    if (y < 0) y += array.GetLength(0) ;
                    else if (y >= array.GetLength(0)) y -= array.GetLength(0);

                    if (x < 0) x += array.GetLength(1) ;
                    else if (x >= array.GetLength(1)) x -= array.GetLength(1);


                    if (array[y, x].GetComponent<Renderer>().material.color.Equals(color))
                    {
                        counter++;
                    }
                }
            }
            // print(counter - 1 + " for continue liveing");
            return counter - 1 < 4 && counter - 1 > 1 ? false : true;
        }
        else
        {

            for (int i = yPos - 1; i <= yPos + 1; i++)
            {
                for (int k = xPos - 1; k <= xPos + 1; k++)
                {
                    int y = i;
                    int x = k;
                    if (y < 0) y += array.GetLength(0) ;
                    else if (y >= array.GetLength(0)) y -= array.GetLength(0);

                    if (x < 0) x += array.GetLength(1) ;
                    else if (x >= array.GetLength(1)) x -= array.GetLength(1);


                    if (array[y, x].GetComponent<Renderer>().material.color.Equals(color))
                    {
                        counter++;
                    }
                }
            }
            print(counter - 1 + " for getting birthted at " + xPos + " " + yPos);
            return counter - 1 != 2;
        }
    }

    

    public bool IsAlive()
    {
        return material.color.Equals(color);
    }
}
