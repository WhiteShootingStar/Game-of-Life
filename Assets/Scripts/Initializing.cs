using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Point;

public class Initializing : MonoBehaviour
{
    public int _TotalAmount;
    private int _OneRow;
    Point[,] table;
    float coordinatesToSpawnX, coordinatesToSpawnY;
    public Point point;
    public Camera camera;
    float halfDistance;
    bool hasStarted = false;
    private int wid = PlayerPrefs.Width, hei = PlayerPrefs.Heigth;
    public static float WaitTime;
    private void Awake()
    {

        coordinatesToSpawnX = transform.position.x + 1f;
        coordinatesToSpawnY = transform.position.y;
        _OneRow = (int)Mathf.Sqrt(_TotalAmount);
        table = new Point[hei,wid];
        for (int i = 0; i < hei; i++)
        {
            for (int k = 0; k < wid; k++)
            {
                var spawned = Instantiate(point, new Vector2(coordinatesToSpawnX, coordinatesToSpawnY), transform.rotation);
                spawned.xPos = k;
                spawned.yPos = i;
                table[i, k] = spawned;
                coordinatesToSpawnX += 1.05f;


            }
            coordinatesToSpawnX = transform.position.x + 1f;
            coordinatesToSpawnY -= 1.05f;
        }
        halfDistance = Vector3.Distance(table[0, 0].transform.position, table[hei-1, wid - 1].transform.position) / 2;

        assignCamera();
        array = table;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            camera.orthographicSize = Mathf.Min(camera.orthographicSize + 0.1f*table.GetLength(0), halfDistance * 2);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {

            camera.orthographicSize = Mathf.Max(camera.orthographicSize - 0.1f*table.GetLength(0), 1);
        }
       if(Input.GetMouseButton(1))
        {
           camera. transform.Translate(new Vector2(-Input.GetAxis("Mouse X"), -Input.GetAxis("Mouse Y"))*0.05f*table.GetLength(0));
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Point.toStart = true;
        }
        if (Point.toStart == true && hasStarted == false)
        {
            hasStarted = true;
            StartCoroutine(changeColor());
        }
    }

    void assignCamera()
    {
        var middle = Vector3.MoveTowards(table[0, 0].transform.position, table[hei - 1, wid - 1].transform.position,
         halfDistance);
        camera = Camera.main;
        camera.transform.position = middle + new Vector3(0, 0, -100);
      
        camera.orthographicSize = halfDistance;
        print(Vector3.Distance(table[0, 0].transform.position, table[hei - 1, wid - 1].transform.position) / 2);
    }
    IEnumerator changeColor()
    {
        while (true)
        {
            yield return new WaitForSeconds(WaitTime);
            //yield return new WaitForSeconds(0.5f);
            for (int i = 0; i < hei; i++)
            {
                for (int k = 0; k < wid; k++)
                {

                    var target = table[i, k];
                   
                    if (target.hasToDie())
                    {
                        target.isAlive = false;
                       
                    }
                    else target.isAlive = true; //target.material.color = Color.red;
                }
            }
          
        }
    }


   public void ChangeDrawingMode()
    {
        if (Point.drawMode.Equals(MouseDrawMode.Pressing))
        {
            Point.drawMode = MouseDrawMode.Hoovering;
        }
        else Point.drawMode = MouseDrawMode.Pressing;
    }

    public void ReloadLevel()
    {
        var currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadSceneAsync(currentScene);
        Point.toStart = false;
    }
    public void goBack()
    {
        SceneManager.LoadSceneAsync(0);
        Point.toStart = false;
    }
}
