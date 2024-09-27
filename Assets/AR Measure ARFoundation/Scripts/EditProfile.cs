using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class EditProfile : MonoBehaviour
{
    // NOTE - input field password sama dropdown pendidikan terakhir belum difungsiin, tanggal lahir masih pake usia
    public string namaLengkap;
    public int usia;
    public bool isHamil;
    public bool isMenyusui;
    PanelManager panelManager;

    [Header("UI")]
    public InputField ifNamaLengkap;
    public InputField ifUsia;
    public Dropdown ddIsHamil;
    public Dropdown ddIsMenyusui;

    private void Start()
    {
        isHamil = true;
        isMenyusui = true;

        panelManager = FindObjectOfType<PanelManager>();

        ifNamaLengkap.onEndEdit.AddListener(delegate 
        { 
            OnInputFieldEdit(ifNamaLengkap); 
        });

        ifUsia.onEndEdit.AddListener(delegate 
        { 
            OnInputFieldEdit(ifUsia); 
        });

        ddIsHamil.onValueChanged.AddListener(delegate
        {
            DropdownValueChanged(ddIsHamil);
        });

        ddIsMenyusui.onValueChanged.AddListener(delegate
        {
            DropdownValueChanged(ddIsMenyusui);
        });
    }
    private void MenuAvailable()
    {
        if (usia < 15)
        {
            panelManager.UpdateRemajaButton(true);
        }

        if (isHamil)
        {
            panelManager.UpdateIbuHamilButton(true);
        }

        if (isMenyusui)
        {
            panelManager.UpdateIbuMenyusuiButton(true);
        }
    }

    private void OnInputFieldEdit(InputField inputField)
    {
        if(inputField == ifNamaLengkap)
        {
            namaLengkap = ifNamaLengkap.text;
        }
        
        if(inputField == ifUsia)
        {
            int.TryParse(ifUsia.text, out usia);
        }
    }

    private void DropdownValueChanged(Dropdown dropdown)
    {
        if(dropdown == ddIsHamil)
        {
            if (ddIsHamil.options[dropdown.value].text == "YA")
            {
                isHamil = true;
            }
            else
            {
                isHamil = false;
            }
        }
        else if(dropdown == ddIsMenyusui)
        {
            if (ddIsMenyusui.options[dropdown.value].text == "YA")
            {
                isMenyusui = true;
            }
            else
            {
                isMenyusui = false;
            }
        }
    }

    public void SimpanEditProfile()
    {
        MenuAvailable();
        panelManager.SimpanEditProfile();
    }

}
