using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject panelTutorial;
    public GameObject slide1;
    public GameObject slide2;
    // Start is called before the first frame update
    void Start()
    {
        panelTutorial.SetActive(true);
        slide1.SetActive(true);
        slide2.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
		}
    }

    public void NextSlide()
    {
        slide2.SetActive(true);
        slide1.SetActive(false);
    }

    public void TutorialOver()
    {
        panelTutorial.SetActive(false);
    }
}
