using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RekomendasiRemaja : MonoBehaviour
{
    public float beratBadan;
    public float tinggiBadan;


    private void IMTCount()
    {
        float rightSide = tinggiBadan / 100;
        float squared = Math.Pow(rightSide,2);

        float IMT = beratBadan / squared;
        if (IMT < 18.5)
        {
            Debug.Log("Kurus");
        }
        else if (IMT < 25.1)
        {
            Debug.Log("Normal");
        }
        else if (IMT >= 25.1)
        {
            Debug.Log("Gemuk");
        }
        else
        {
            Debug.Log("Angka Salah, tolong masukkan angka yang benar");
        }
    }
}
