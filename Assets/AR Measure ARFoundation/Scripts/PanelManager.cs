using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelManager : MonoBehaviour
{
    public static PanelManager instance;
    //public ARChecker arChecker;

    [Header("Panels")]
    public GameObject login;
    public GameObject editProfile;
    public GameObject register;
    public GameObject profile;
    public GameObject mainMenu;
    public GameObject rekomendasiRemaja;
    public GameObject rekomendasiIbuHamil;
    public GameObject rekomenasiAnakLK;
    public GameObject rekomendasiAnakPr;
    public GameObject rekomendasiIbuMenyusui;
    public GameObject rekomendasiResult;
    public GameObject augmentedReality;

    [Header("Canvas")]
    private Canvas cLogin;
    private Canvas cEditProfile;
    private Canvas cRegister;
    private Canvas cProfile;
    public Canvas cMainMenu;
    private Canvas cRekomendasiRemaja;
    private Canvas cRekomendasiIbuHamil;
    private Canvas cRekomendasiAnakLK;
    private Canvas cRekomendasiAnakPr;
    private Canvas cRekomendasiIbuMenyusui;
    private Canvas cRekomendasiResult;

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
        augmentedReality.SetActive(false);
        InitializeCanvas();
        ClearCanvas();
        cLogin.enabled = true;
        // if(arChecker.isLogin == true)
        // {
        //     cLogin.enabled = false;
        //     cMainMenu.enabled = true;
        //     arChecker.isLogin = false;
        // }
        // else if(arChecker.isLogin == false)
        // {
        //     cLogin.enabled = true;
        //     arChecker.isLogin = false;
        // }
    }

    public void InitializeCanvas()
    {
        cLogin = login.GetComponent<Canvas>();
        cRegister = register.GetComponent<Canvas>();
        cEditProfile = editProfile.GetComponent<Canvas>();
        cProfile = profile.GetComponent<Canvas>();
        cMainMenu = mainMenu.GetComponent<Canvas>();
        cRekomendasiRemaja = rekomendasiRemaja.GetComponent<Canvas>();
        cRekomendasiIbuHamil = rekomendasiIbuHamil.GetComponent<Canvas>();
        cRekomendasiAnakLK = rekomenasiAnakLK.GetComponent<Canvas>();
        cRekomendasiAnakPr = rekomendasiAnakPr.GetComponent<Canvas>();
        cRekomendasiIbuMenyusui = rekomendasiIbuMenyusui.GetComponent<Canvas>();
        cRekomendasiResult = rekomendasiResult.GetComponent<Canvas>();
    }

    public void ClearCanvas()
    {
        cLogin.enabled = false;
        cRegister.enabled = false;
        cEditProfile.enabled = false;
        cProfile.enabled = false;
        cMainMenu.enabled = false;
        cRekomendasiRemaja.enabled = false;
        cRekomendasiIbuHamil.enabled = false;
        cRekomendasiAnakLK.enabled = false;
        cRekomendasiAnakPr.enabled = false;
        cRekomendasiIbuMenyusui.enabled = false;
        cRekomendasiResult.enabled = false;
    }

    public void LoginButton()
    {
        ClearCanvas();
        cEditProfile.enabled = true;
    }

    public void SimpanEditProfile()
    {
        ClearCanvas();
        cProfile.enabled = true;
    }

    public void EditProfileFromProfile()
    {
        ClearCanvas();
        cEditProfile.enabled = true;
    }

    public void MainMenu()
    {
        ClearCanvas();
        cMainMenu.enabled = true;
    }

    public void ProfileFromMainMenu()
    {
        ClearCanvas();
        cProfile.enabled = true;
    }

    public void MainMenuToRemaja()
    {
        ClearCanvas();
        cRekomendasiRemaja.enabled = true;
    }

    public void MainMenuToIbuHamil()
    {
        ClearCanvas();
        cRekomendasiIbuHamil.enabled = true;
    }

    public void MainMenuToAnakLK()
    {
        ClearCanvas();
        cRekomendasiAnakLK.enabled = true;
    }

    public void MainMenuToAnakPr()
    {
        ClearCanvas();
        cRekomendasiAnakPr.enabled = true;
    }

    public void MainMenuToIbuMenyusui()
    {
        ClearCanvas();
        cRekomendasiIbuMenyusui.enabled = true;
    }

    public void ToResult()
    {
        ClearCanvas();
        cRekomendasiResult.enabled = true;
    }

    public void MainMenuToARFeature()
    {
        ClearCanvas();
        augmentedReality.SetActive(true);
        //arChecker.isLogin = true;
        //SceneManager.LoadScene("PackageScene");
    }
}
