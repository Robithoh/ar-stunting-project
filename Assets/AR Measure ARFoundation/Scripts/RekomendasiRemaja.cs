using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class RekomendasiRemaja : MonoBehaviour
{
    [Header("UI")]
    public InputField ifNama;
    public InputField ifUmur;
    public InputField ifBeratBadan;
    public InputField ifTinggiBadan;
    public Text tIMT;
    public Text tKlasifikasiIMT;

    [Header("Value")]
    public string namaLengkap;
    public int umur;
    public float beratBadan;
    public float tinggiBadan;
    public float IMT;
    public string klasifikasiIMT;

    public Recommendation recommendation;

    

}
