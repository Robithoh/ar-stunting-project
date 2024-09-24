using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Recommendation : MonoBehaviour
{
    [Header("Rekomendasi 1")]
    public string namaLengkap;
    public float beratBadan;
    public float tinggiBadan;
    public float IMT;

    [Header("Rekomendasi 2")]
    public bool testHB;
    public float hbValue;
    public int usia;

    [Header("Rekomendasi 3")]
    public bool isHamil;
    // public bool isUkurLILA;
    public float lilaValue;

    [Header("Rekomendasi 4")]
    public string textRekomendasi4;

    [Header("Rekomendasi 5")]
    public string textRekomendasi5;
    public int usiaBulan;
    public float tinggiBadanAnak;
    public float[] kiriMedianPr;
    public float[] medianPr;
    public float[] kananMedianPr;

    public float[] kiriMedianL;
    public float[] medianL;
    public float[] kananMedianL;

    [Header("UI Rekomendasi Remaja")]
    public InputField ifNamaRemaja;
    public InputField ifUmurRemaja;
    public InputField ifBeratBadanRemaja;
    public InputField ifTinggiBadanRemaja;
    public Text tIMTRemaja;
    public Text tKlasifikasiIMTRemaja;
    public Dropdown ddLILARemaja;
    public InputField ifLILARemaja;
    public Dropdown ddHemoRemaja;
    public InputField ifHemoRemaja;
    public Text tAnemiaRemaja;
    public Button bRekomendasiRemaja;
    public bool isNamaValidR, isBBValidR, isTBValidR, isLilaValidR, isHemoValidR, isUmurValidR;
    bool isHemoR, isLilaR;

    [Header("UI Rekomendasi Ibu Hamil")]
    public InputField ifNamaIbuHamil;
    public InputField ifUmurIbuHamil;
    public InputField ifBeratBadanIbuHamil;
    public InputField ifTinggiBadanIbuHamil;
    public Text tIMTIbuHamil;
    public Text tKlasifikasiIMTIbuHamil;
    public Dropdown ddLILAIbuHamil;
    public InputField ifLILAIbuHamil;
    public Dropdown ddHemoIbuHamil;
    public InputField ifHemoIbuHamil;
    public Text tAnemiaIbuHamil;
    public Button bRekomendasiIbuHamil;
    public bool isNamaValidIH, isBBValidIH, isTBValidIH, isLilaValidIH, isHemoValidIH, isUmurValidIH;
    bool isHemoIH, isLilaIH;

    [Header("Rekomendasi Result")]
    public Text tNamaRekomendasi;
    public Text tUsiaRekomendasi;
    public Text tInfoStatusRekomendasi;
    public Text tRekomendasiResult;
    public string textRekomendasiIMT;
    public string textRekomendasiAnemia;
    public string textRekomendasiLILA;


    [Header("References")]
    public PanelManager panelManager;

    private void Start()
    {
        isHemoR = true; // buat jadiin default value dropdown hemoglobin "YA"
        isLilaR = true;
        isHemoIH = true;
        isLilaIH = true;

        // Remaja

        ifNamaRemaja.onEndEdit.AddListener(OnInputFieldNamaEdit);
        ifBeratBadanRemaja.onEndEdit.AddListener(OnInputFieldBBEdit);
        ifTinggiBadanRemaja.onEndEdit.AddListener(OnInputFieldTBEdit);
        ifUmurRemaja.onEndEdit.AddListener(OnInputFieldUmurEdit);
        ifLILARemaja.onEndEdit.AddListener(OnInputFieldLILAEdit);
        ifHemoRemaja.onEndEdit.AddListener(OnInputFieldHemoEdit);

        bRekomendasiRemaja.onClick.AddListener(RekomendasiRemaja);

        ddLILARemaja.onValueChanged.AddListener(delegate
        {
            DropdownValueLILAChanged(ddLILARemaja);
        });

        DropdownValueLILAChanged(ddLILARemaja);

        ddHemoRemaja.onValueChanged.AddListener(delegate
        {
            DropdownValueHemoChanged(ddHemoRemaja);
        });

        DropdownValueHemoChanged(ddHemoRemaja);

        // Ibu Hamil

        ifNamaIbuHamil.onEndEdit.AddListener(OnInputFieldNamaEdit);
        ifBeratBadanIbuHamil.onEndEdit.AddListener(OnInputFieldBBEdit);
        ifTinggiBadanIbuHamil.onEndEdit.AddListener(OnInputFieldTBEdit);
        ifUmurIbuHamil.onEndEdit.AddListener(OnInputFieldUmurEdit);
        ifLILAIbuHamil.onEndEdit.AddListener(OnInputFieldLILAEdit);
        ifHemoIbuHamil.onEndEdit.AddListener(OnInputFieldHemoEdit);

        bRekomendasiIbuHamil.onClick.AddListener(RekomendasiIbuHamil);

        ddLILAIbuHamil.onValueChanged.AddListener(delegate
        {
            DropdownValueLILAChanged(ddLILAIbuHamil);
        });

        DropdownValueLILAChanged(ddLILAIbuHamil);

        ddHemoIbuHamil.onValueChanged.AddListener(delegate
        {
            DropdownValueHemoChanged(ddHemoIbuHamil);
        });

        DropdownValueHemoChanged(ddHemoIbuHamil);
    }

    private void IMTCount(float tb, float bb)
    {
        float rightSide = tb / 100;
        float squared = Mathf.Pow(rightSide, 2);

        IMT = bb / squared;

        if (panelManager.rekomendasiRemaja.activeSelf)
        {
            tIMTRemaja.text = IMT.ToString();
            if (IMT < 18.5)
            {
                tKlasifikasiIMTRemaja.text = "Kurus";
                Debug.Log("Kurus");
                textRekomendasiIMT = "- Perbaikan gizi, jika sampai hamil akan berisiko melahirkan anak berat badan rendah, risiko stunting";
            }
            else if (IMT < 25.1)
            {
                tKlasifikasiIMTRemaja.text = "Normal";
                Debug.Log("Normal");
                textRekomendasiIMT = "- Pertahankan indeks massa tubuh (IMT) dengan memperhatikan berat badan dan tinggi badan";
            }
            else if (IMT >= 25.1)
            {
                tKlasifikasiIMTRemaja.text = "Gemuk";
                Debug.Log("Gemuk");
                textRekomendasiIMT = "- Kurangi berat badan agar indeks massa tubuh (IMT) normal";
            }
            else
            {
                tKlasifikasiIMTRemaja.text = "-";
                Debug.Log("Invalid Input");
                textRekomendasiIMT = "";
            }
        }
        else if (panelManager.rekomendasiIbuHamil.activeSelf)
        {
            tIMTIbuHamil.text = IMT.ToString();
            if (IMT < 18.5)
            {
                tKlasifikasiIMTIbuHamil.text = "Kurus";
                Debug.Log("Kurus");
                textRekomendasiIMT = "- Perbaikan gizi, jika sampai hamil akan berisiko melahirkan anak berat badan rendah, risiko stunting";
            }
            else if (IMT < 25.1)
            {
                tKlasifikasiIMTIbuHamil.text = "Normal";
                Debug.Log("Normal");
                textRekomendasiIMT = "- Pertahankan indeks massa tubuh (IMT) dengan memperhatikan berat badan dan tinggi badan";
            }
            else if (IMT >= 25.1)
            {
                tKlasifikasiIMTIbuHamil.text = "Gemuk";
                Debug.Log("Gemuk");
                textRekomendasiIMT = "- Kurangi berat badan agar indeks massa tubuh (IMT) normal";
            }
            else
            {
                tKlasifikasiIMTIbuHamil.text = "-";
                Debug.Log("Invalid Input");
                textRekomendasiIMT = "";
            }
        }

    }

    private void AnemiaCount()
    {
        if (panelManager.rekomendasiRemaja.activeSelf)
        {
            if (usia <= 11 && usia != 0 && hbValue <= 11.4 && hbValue != 0)
            {
                tAnemiaRemaja.text = "Anemia";
                Debug.Log("Anemia");
                textRekomendasiAnemia = "- Perbaiki gizi makanan, rutin minum tablet tambah darah, cek HB berkala";
            }
            else if (usia <= 15 && usia != 0 && hbValue <= 11.9 && hbValue != 0)
            {
                tAnemiaRemaja.text = "Anemia";
                Debug.Log("Anemia");
                textRekomendasiAnemia = "- Perbaiki gizi makanan, rutin minum tablet tambah darah, cek HB berkala";
            }
            else if (usia > 15)
            {
                tAnemiaRemaja.text = "Tidak termasuk remaja";
                Debug.Log("Tidak termasuk remaja");
                textRekomendasiAnemia = "- Tidak termasuk remaja, silahkan cek kategori lain";
            }
            else if (usia == 0 && hbValue != 0)
            {
                tAnemiaRemaja.text = "Harap masukkan usia anda";
                Debug.Log("Invalid Input");
                textRekomendasiAnemia = "";
                isHemoValidR = false;
            }
            else if (usia != 0 && isHemoR && hbValue == 0)
            {
                tAnemiaRemaja.text = "Harap masukkan nilai Hb anda";
                isHemoValidR = false;
            }
            else if (usia != 0 && !isHemoR)
            {
                tAnemiaRemaja.text = "Belum melakukan test Hb";
                textRekomendasiAnemia = "- Segera lakukan tes HB dengan konsultasi ke tenaga kesehatan";

            }
            else
            {
                tAnemiaRemaja.text = "Tidak Anemia";
                Debug.Log("Tidak Anemia");
                textRekomendasiAnemia = "- Tetap mengkonsumsi tablet tambah darah, terutama setelah masa haid";
            }
        }
        else if (panelManager.rekomendasiIbuHamil.activeSelf)
        {
            if (hbValue <= 11.4 && hbValue != 0)
            {
                tAnemiaIbuHamil.text = "Anemia";
                Debug.Log("Anemia");
                textRekomendasiAnemia = "- Perbaiki gizi makanan, rutin minum tablet tambah darah, cek HB berkala";
            }
            else if (hbValue <= 11.9 && hbValue != 0)
            {
                tAnemiaIbuHamil.text = "Anemia";
                Debug.Log("Anemia");
                textRekomendasiAnemia = "- Perbaiki gizi makanan, rutin minum tablet tambah darah, cek HB berkala";
            }
            else if (isHemoIH && hbValue == 0)
            {
                tAnemiaIbuHamil.text = "Harap masukkan nilai Hb anda";
                isHemoValidIH = false;
            }
            else if (!isHemoIH)
            {
                tAnemiaRemaja.text = "Belum melakukan test Hb";
                textRekomendasiAnemia = "- Segera lakukan tes HB dengan konsultasi ke tenaga kesehatan";
            }
            else
            {
                tAnemiaIbuHamil.text = "Tidak Anemia";
                Debug.Log("Tidak Anemia");
                textRekomendasiAnemia = "- Tetap mengkonsumsi tablet tambah darah, terutama setelah masa haid";
            }
        }

    }

    private void LILACount()
    {
        // if (isHamil)
        // {
        if (panelManager.rekomendasiRemaja.activeSelf)
        {
            if (isLilaR)
            {
                if (lilaValue <= 23.5 && lilaValue != 0)
                {
                    Debug.Log("LILA rendah");
                    textRekomendasiLILA = "LILA termasuk rendah, agar rutin periksa kehamilan";
                }
                else if (lilaValue > 23.5)
                {
                    Debug.Log("LILA Normal");
                    textRekomendasiLILA = "LILA normal, pertahankan asupan gizi";
                }
                else
                {
                    isLilaValidR = false;
                    Debug.Log("Invalid Input");
                    textRekomendasiLILA = "";
                }
            }
            else
            {
                Debug.Log("Belum diukur LILA");
                textRekomendasiLILA = "- Lakukan pengukuran lingkar lengan (LILA)";
            }
        }
        else if(panelManager.rekomendasiIbuHamil.activeSelf)
        {
            if (isLilaIH)
            {
                if (lilaValue <= 23.5 && lilaValue != 0)
                {
                    Debug.Log("LILA rendah");
                    textRekomendasiLILA = "LILA termasuk rendah, agar rutin periksa kehamilan";
                }
                else if (lilaValue > 23.5)
                {
                    Debug.Log("LILA Normal");
                    textRekomendasiLILA = "LILA normal, pertahankan asupan gizi";
                }
                else
                {
                    isLilaValidR = false;
                    Debug.Log("Invalid Input");
                    textRekomendasiLILA = "";
                }
            }
            else
            {
                Debug.Log("Belum diukur LILA");
                textRekomendasiLILA = "- Lakukan pengukuran lingkar lengan (LILA)";
            }
        }

        // }
        // else
        // {
        //    Debug.Log("Tidak hamil");
        // }
    }

    private void TBCount()
    {
        if (tinggiBadan < 145)
        {
            Debug.Log("Tinggi badan kurang");
            textRekomendasi4 = "Tinggi badan termasuk rendah, agar rutin periksa kehamilan";
        }
    }

    private void ZScorePrCount()
    {
        if (tinggiBadanAnak > medianPr[usiaBulan])
        {
            float zScore = (tinggiBadanAnak - medianPr[usiaBulan]) / (kananMedianPr[usiaBulan] - medianPr[usiaBulan]);
        }
        else
        {
            float zScore = (tinggiBadanAnak - medianPr[usiaBulan]) / (medianPr[usiaBulan] - kiriMedianPr[usiaBulan]);
        }
    }

    private void ZScoreLCount()
    {
        if (tinggiBadanAnak > medianL[usiaBulan])
        {
            float zScore = (tinggiBadanAnak - medianL[usiaBulan]) / (kananMedianL[usiaBulan] - medianL[usiaBulan]);
        }
        else
        {
            float zScore = (tinggiBadanAnak - medianL[usiaBulan]) / (medianL[usiaBulan] - kiriMedianL[usiaBulan]);
        }
    }

    public void OnInputFieldBBEdit(string input)
    {
        if(panelManager.rekomendasiRemaja.activeSelf)
        {
            isBBValidR = float.TryParse(input, out beratBadan);
            IMTFunc();
        }
        else if(panelManager.rekomendasiIbuHamil.activeSelf)
        {
            isBBValidIH = float.TryParse(input, out beratBadan);
            IMTFunc();
        }
    }

    public void OnInputFieldTBEdit(string input)
    {
        if(panelManager.rekomendasiRemaja.activeSelf)
        {
            isTBValidR = float.TryParse(input, out tinggiBadan);
            IMTFunc();
        }
        else if(panelManager.rekomendasiIbuHamil.activeSelf)
        {
            isTBValidIH = float.TryParse(input, out tinggiBadan);
            IMTFunc();
        }
        
    }

    private void IMTFunc()
    {
        if(panelManager.rekomendasiRemaja.activeSelf)
        {
            if (isBBValidR && isTBValidR)
            {
                IMTCount(tinggiBadan, beratBadan);
            }
            else
            {
                Debug.Log("error broh");
            }
        }
        else if(panelManager.rekomendasiIbuHamil.activeSelf)
        {
            if (isBBValidIH && isTBValidIH)
            {
                IMTCount(tinggiBadan, beratBadan);
            }
            else
            {
                Debug.Log("error broh");
            }
        }
    }

    private void DropdownValueLILAChanged(Dropdown dropdown)
    {
        if (panelManager.rekomendasiRemaja.activeSelf)
        {
            if (ddLILARemaja.options[dropdown.value].text == "YA")
            {
                ifLILARemaja.interactable = true;
                isLilaR = true;
                LILACount();
            }
            else
            {
                ifLILARemaja.interactable = false;
                isLilaR = false;
                isLilaValidR = true;
                LILACount();
            }
        }
        else if (panelManager.rekomendasiIbuHamil.activeSelf)
        {
            if (ddLILAIbuHamil.options[dropdown.value].text == "YA")
            {
                ifLILAIbuHamil.interactable = true;
                isLilaIH = true;
                LILACount();
            }
            else
            {
                ifLILAIbuHamil.interactable = false;
                isLilaIH = false;
                isLilaValidIH = true;
                LILACount();
            }
        }

    }

    private void DropdownValueHemoChanged(Dropdown dropdown)
    {
        if (panelManager.rekomendasiRemaja.activeSelf)
        {
            if (ddHemoRemaja.options[dropdown.value].text == "YA")
            {
                ifHemoRemaja.interactable = true;
                isHemoR = true;
                AnemiaCount();
            }
            else
            {
                ifHemoRemaja.interactable = false;
                isHemoR = false;
                isHemoValidR = true;
                AnemiaCount();
            }
        }
        else if (panelManager.rekomendasiIbuHamil.activeSelf)
        {
            if (ddHemoIbuHamil.options[dropdown.value].text == "YA")
            {
                ifHemoIbuHamil.interactable = true;
                isHemoIH = true;
                AnemiaCount();
            }
            else
            {
                ifHemoIbuHamil.interactable = false;
                isHemoIH = false;
                isHemoValidIH = true;
                AnemiaCount();
            }
        }
    }

    public void OnInputFieldNamaEdit(string input)
    {
        if(panelManager.rekomendasiRemaja.activeSelf)
        {
            isNamaValidR = true;
        }
        else if(panelManager.rekomendasiIbuHamil.activeSelf)
        {
            isNamaValidIH = true;
        }

        namaLengkap = input;
    }

    public void OnInputFieldUmurEdit(string input)
    {
        if(panelManager.rekomendasiRemaja.activeSelf)
        {
            isUmurValidR = int.TryParse(input, out usia);
            AnemiaCount();
        }
        else if(panelManager.rekomendasiIbuHamil.activeSelf)
        {
            isUmurValidIH = int.TryParse(input, out usia);
            AnemiaCount();
        }
    }

    public void OnInputFieldLILAEdit(string input)
    {
        if(panelManager.rekomendasiRemaja.activeSelf)
        {
            isLilaValidR = float.TryParse(input, out lilaValue);
            LILACount();
        }
        else if(panelManager.rekomendasiIbuHamil.activeSelf)
        {
            isLilaValidIH = float.TryParse(input, out lilaValue);
            LILACount();
        }
    }

    public void OnInputFieldHemoEdit(string input)
    {
        if(panelManager.rekomendasiRemaja.activeSelf)
        {
            isHemoValidR = float.TryParse(input, out hbValue);
            AnemiaCount();
        }
        else if(panelManager.rekomendasiIbuHamil.activeSelf)
        {
            isHemoValidIH = float.TryParse(input, out hbValue);
            AnemiaCount();
        }
        
    }

    public void RekomendasiRemaja()
    {
        if (isNamaValidR && isBBValidR && isTBValidR && isUmurValidR && isLilaValidR && isHemoValidR)
        {
            tRekomendasiResult.text = textRekomendasiIMT + "\n" + "\n" + textRekomendasiAnemia + "\n" + "\n" + textRekomendasiLILA;
            tNamaRekomendasi.text = namaLengkap;
            tUsiaRekomendasi.text = usia + " Tahun";
            tInfoStatusRekomendasi.text = "";
            panelManager.RemajaToResult();
        }
        else
        {
            Debug.Log("error broh");
        }
    }

    public void RekomendasiIbuHamil()
    {
        if (isNamaValidIH && isBBValidIH && isTBValidIH && isUmurValidIH && isLilaValidIH && isHemoValidIH)
        {
            tRekomendasiResult.text = textRekomendasiIMT + "\n" + "\n" + textRekomendasiAnemia + "\n" + "\n" + textRekomendasiLILA;
            tNamaRekomendasi.text = namaLengkap;
            tUsiaRekomendasi.text = usia + " Tahun";
            tInfoStatusRekomendasi.text = "";
            panelManager.IbuHamilToResult();
        }
        else
        {
            Debug.Log("error broh");
        }
    }

}
