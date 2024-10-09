using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
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

    [Header("Button Menu")]
    public Button buttonRemaja;
    public Button buttonIbuHamil;
    public Button buttonIbuMenyusui;
    public Button buttonAnakLK;
    public Button buttonAnakPr;

    private void Start() 
    {
        login.SetActive(true);
        editProfile.SetActive(false);
        profile.SetActive(false);
        mainMenu.SetActive(false); 
        rekomendasiRemaja.SetActive(false);  
        rekomendasiIbuHamil.SetActive(false);
        rekomenasiAnakLK.SetActive(false);
        rekomendasiAnakPr.SetActive(false);
        rekomendasiIbuMenyusui.SetActive(false);
        rekomendasiResult.SetActive(false);

        buttonRemaja.interactable = false;
        buttonIbuHamil.interactable = false;
        buttonIbuMenyusui.interactable = false;
        buttonAnakLK.interactable = false;
        buttonAnakPr.interactable = false;
    }

    public void LoginButton()
    {
        login.SetActive(false);
        editProfile.SetActive(true);
    }

    public void SimpanEditProfile()
    {
        editProfile.SetActive(false);
        profile.SetActive(true);
    }

    public void EditProfileFromProfile()
    {
        profile.SetActive(false);
        editProfile.SetActive(true);
    }

    public void BatalEditProfile()
    {
        editProfile.SetActive(false);
        profile.SetActive(true);
    }

    public void MainMenu()
    {
        profile.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void ProfileFromMainMenu()
    {
        mainMenu.SetActive(false);
        profile.SetActive(true);
    }

    public void MainMenuToRemaja()
    {
        mainMenu.SetActive(false);
        rekomendasiRemaja.SetActive(true);
    }

    public void MainMenuToIbuHamil()
    {
        mainMenu.SetActive(false);
        rekomendasiIbuHamil.SetActive(true);
    }

    public void MainMenuToAnakLK()
    {
        mainMenu.SetActive(false);
        rekomenasiAnakLK.SetActive(true);
    }

    public void MainMenuToAnakPr()
    {
        mainMenu.SetActive(false);
        rekomendasiAnakPr.SetActive(true);
    }

    public void MainMenuToIbuMenyusui()
    {
        mainMenu.SetActive(false);
        rekomendasiIbuMenyusui.SetActive(true);
    }

    public void RemajaToResult()
    {
        rekomendasiRemaja.SetActive(false);
        rekomendasiResult.SetActive(true);
    }

    public void IbuHamilToResult()
    {
        rekomendasiIbuHamil.SetActive(false);
        rekomendasiResult.SetActive(true);
    }

    public void AnakLKToResult()
    {
        rekomenasiAnakLK.SetActive(false);
        rekomendasiResult.SetActive(true);
    }

    public void AnakPrToResult()
    {
        rekomendasiAnakPr.SetActive(false);
        rekomendasiResult.SetActive(true);
    }

    public void IbuMenyusuiToResult()
    {
        rekomendasiIbuMenyusui.SetActive(false);
        rekomendasiResult.SetActive(true);
    }

    public void UpdateRemajaButton(bool isRemaja)
    {
        buttonRemaja.interactable = isRemaja;
    }

    public void UpdateIbuHamilButton(bool isIbuHamil)
    {
        buttonIbuHamil.interactable = isIbuHamil;
    }

    public void UpdateIbuMenyusuiButton(bool isIbuMenyusui)
    {
        buttonIbuMenyusui.interactable = isIbuMenyusui;
        buttonAnakLK.interactable = isIbuMenyusui;
        buttonAnakPr.interactable = isIbuMenyusui;
    }
}
