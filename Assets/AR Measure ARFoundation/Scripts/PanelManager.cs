using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public static PanelManager instance;

    [Header("Panels")]
    public GameObject login;
    public GameObject editProfile;
    public GameObject profile;
    public GameObject mainMenu;
    public GameObject rekomendasiRemaja;
    public GameObject rekomendasiIbuHamil;
    public GameObject rekomenasiAnakLK;
    public GameObject rekomendasiAnakPr;
    public GameObject rekomendasiIbuMenyusui;
    public GameObject rekomendasiResult;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    private void Start()
    {
        login.SetActive(true);
    }

    public void ClearScreen()
    {
        login.SetActive(false);
        editProfile.SetActive(false);
        profile.SetActive(false);
        mainMenu.SetActive(false); 
        rekomendasiRemaja.SetActive(false);  
        rekomendasiIbuHamil.SetActive(false);
        rekomenasiAnakLK.SetActive(false);
        rekomendasiAnakPr.SetActive(false);
        rekomendasiIbuMenyusui.SetActive(false);
        rekomendasiResult.SetActive(false);
    }

    public void LoginButton()
    {
        ClearScreen();
        editProfile.SetActive(true);
    }

    public void SimpanEditProfile()
    {
        ClearScreen();
        profile.SetActive(true);
    }

    public void EditProfileFromProfile()
    {
        ClearScreen();
        editProfile.SetActive(true);
    }

    public void MainMenu()
    {
        ClearScreen();
        mainMenu.SetActive(true);
    }

    public void ProfileFromMainMenu()
    {
        ClearScreen();
        profile.SetActive(true);
    }

    public void MainMenuToRemaja()
    {
        ClearScreen();
        rekomendasiRemaja.SetActive(true);
    }

    public void MainMenuToIbuHamil()
    {
        ClearScreen();
        rekomendasiIbuHamil.SetActive(true);
    }

    public void MainMenuToAnakLK()
    {
        ClearScreen();
        rekomenasiAnakLK.SetActive(true);
    }

    public void MainMenuToAnakPr()
    {
        ClearScreen();
        rekomendasiAnakPr.SetActive(true);
    }

    public void MainMenuToIbuMenyusui()
    {
        ClearScreen();
        rekomendasiIbuMenyusui.SetActive(true);
    }

    public void ToResult()
    {
        ClearScreen();
        rekomendasiResult.SetActive(true);
    }
}
