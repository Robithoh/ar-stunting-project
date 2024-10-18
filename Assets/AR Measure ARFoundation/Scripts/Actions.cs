using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Actions : MonoBehaviour
{
    // Start is called before the first frame update

    // This is the prefab that is selected by the user
    public GameObject selectedPrefab;

    //these are the prefabs used in the game
    public GameObject pointPrefab;
    public GameObject linePrefab;

    //these are the points that are chosen by the user
    public GameObject firstSelected, secondSelected;

    //used to store the points
    List<GameObject> points;
    
    //the list of lines
    List<GameObject> lines;

    //the list of texts
    List<GameObject> texts;

    //history of elements that have been drawn
    List<GameObject> historyGo;

    public GameObject[] selectors;

    public PanelManager panelManager;

    void Start()
    {
        //initialize the variables
        points = new List<GameObject>();
        texts = new List<GameObject>();
        historyGo = new List<GameObject>();
        lines = new List<GameObject>();
        selectedPrefab = pointPrefab;
        SelectPointer();
    }

    // Update is called once per frame

    public void PerfromTouch(Vector2 vpress,Vector3 pos, Vector3 normal, GameObject outHitGo)
    {

        //instanciate gameobject POINT in space
        if (selectedPrefab == pointPrefab)
        {

            points.Add(GameObject.Instantiate(selectedPrefab, pos, Quaternion.Euler(90, 0, 0)));
            points[points.Count - 1].transform.up = normal;

            historyGo.Add(points[points.Count - 1]);

            DebugConsole.DC.Log("Point: x=" + vpress.x + " y=" + vpress.y);

            if (points.Count > 1)
            {

                lines.Add(GameObject.Instantiate(linePrefab, pos, Quaternion.Euler(90, 0, 0)));
                lines[lines.Count - 1].GetComponent<Line>().SetParamValues(points[points.Count - 1].transform, points[points.Count - 2].transform);

                lines[lines.Count - 1].transform.SetParent(points[points.Count - 1].transform);
            }
        }


    }
    public void PerfromTouch(Vector2 vpress, Pose hit, GameObject outHitGo)
    {
        //instanciate gameobject POINT in space
        if (selectedPrefab == pointPrefab)
        {

            points.Add(GameObject.Instantiate(selectedPrefab, hit.position, Quaternion.Euler(90, 0, 0)));
            points[points.Count - 1].transform.up = hit.up;

            historyGo.Add(points[points.Count - 1]);

            DebugConsole.DC.Log("Point: x=" + vpress.x + " y=" + vpress.y);

            if (points.Count > 1)
            {

                lines.Add(GameObject.Instantiate(linePrefab, hit.position, Quaternion.Euler(90, 0, 0)));
                lines[lines.Count - 1].GetComponent<Line>().SetParamValues(points[points.Count - 1].transform, points[points.Count - 2].transform);

                lines[lines.Count - 1].transform.SetParent(points[points.Count - 1].transform);
            }
        }

    }



    //undo action
    public void UndoDraw()
    {
        if (historyGo.Count > 0)
        {
            GameObject.Destroy(historyGo[historyGo.Count - 1]);
            historyGo.RemoveAt(historyGo.Count - 1);
            
        }
        if (points.Count > 0)
        {
            points.RemoveAt(points.Count - 1);

        }
        if (lines.Count > 1)
        {
            lines.RemoveAt(lines.Count - 1);
        }
    }


    /// <summary>
    ///  SELECTION ACTIONS
    /// </summary>
    public void SelectPoint()
    {
        //we select the point prefab gameobject
        selectedPrefab = pointPrefab;
        ChangeSelector(1);
    }

    public void SelectPointer()
    { 
        //we deselect any prefab
        selectedPrefab = null;
        ChangeSelector(0);
    }


    public void ChangeSelector(int a)
    {
        for(int ii=0;ii<selectors.Length;ii++)
        {
            selectors[ii].SetActive(false);
        }

        selectors[a].SetActive(true);

    }

    public void BacktoMenu()
    {
        panelManager.augmentedReality.SetActive(false);
        panelManager.ClearCanvas();
        panelManager.cMainMenu.enabled = true;
        // SceneManager.LoadScene("!Final");
    }
   
}
