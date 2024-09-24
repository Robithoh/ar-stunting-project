using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Recommendation : MonoBehaviour
{
    [Header("Rekomendasi 1")]
    public float beratBadan;
    public float tinggiBadan;
    public float IMT;

    [Header("Rekomendasi 2")]
    public bool testHB;
    public float hbValue;
    public int usia;

    [Header("Rekomendasi 3")]
    public bool isHamil;
    public bool isUkurLILA;
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

    [Header("Rekomendasi Remaja")]
    public InputField ifNama;
    public InputField ifUmur;
    public InputField ifBeratBadan;
    public InputField ifTinggiBadan;
    public Text tIMT;
    public Text tKlasifikasiIMT;
    public Dropdown ddLILA;
    public InputField ifLILA;
    public Dropdown ddHemo;
    public InputField ifHemo;
    public Text tAnemia;
    public Button bRekomendasiRemaja;
    public bool isBBValid, isTBValid, isHemoValid, isUmurValid;
    bool isHemo;

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
        isHemo = true; // buat jadiin default value dropdown hemoglobin "YA"
        isUkurLILA = true;

        ifBeratBadan.onEndEdit.AddListener(OnInputFieldBBEdit);
        ifTinggiBadan.onEndEdit.AddListener(OnInputFieldTBEdit);

        ddLILA.onValueChanged.AddListener(delegate
        {
            DropdownValueLILAChanged(ddLILA);
        });

        DropdownValueLILAChanged(ddLILA);

        ddHemo.onValueChanged.AddListener(delegate
        {
            DropdownValueHemoChanged(ddHemo);
        });

        DropdownValueHemoChanged(ddHemo);

        ifUmur.onEndEdit.AddListener(OnInputFieldUmurEdit);
        ifLILA.onEndEdit.AddListener(OnInputFieldLILAEdit);
        ifHemo.onEndEdit.AddListener(OnInputFieldHemoEdit);

        bRekomendasiRemaja.onClick.AddListener(RekomendasiRemaja);
    }

    private void IMTCount(float tb, float bb)
    {
        float rightSide = tb / 100;
        float squared = Mathf.Pow(rightSide, 2);

        IMT = bb / squared;
        tIMT.text = IMT.ToString();
        if (IMT < 18.5)
        {
            tKlasifikasiIMT.text = "Kurus";
            Debug.Log("Kurus");
            textRekomendasiIMT = "- Perbaikan gizi, jika sampai hamil akan berisiko melahirkan anak berat badan rendah, risiko stunting";
        }
        else if (IMT < 25.1)
        {
            tKlasifikasiIMT.text = "Normal";
            Debug.Log("Normal");
            textRekomendasiIMT = "- Pertahankan indeks massa tubuh (IMT) dengan memperhatikan berat badan dan tinggi badan";
        }
        else if (IMT >= 25.1)
        {
            tKlasifikasiIMT.text = "Gemuk";
            Debug.Log("Gemuk");
            textRekomendasiIMT = "- Kurangi berat badan agar indeks massa tubuh (IMT) normal";
        }
        else
        {
            tKlasifikasiIMT.text = "-";
            Debug.Log("Invalid Input");
            textRekomendasiIMT = "";
        }
    }

    private void AnemiaCount()
    {
        if (usia <= 11 && usia != 0 && hbValue <= 11.4 && hbValue != 0)
        {
            tAnemia.text = "Anemia";
            Debug.Log("Anemia");
            textRekomendasiAnemia = "- Perbaiki gizi makanan, rutin minum tablet tambah darah, cek HB berkala";
        }
        else if (usia <= 15  && usia != 0 && hbValue <= 11.9 && hbValue != 0)
        {
            tAnemia.text = "Anemia";
            Debug.Log("Anemia");
            textRekomendasiAnemia = "- Perbaiki gizi makanan, rutin minum tablet tambah darah, cek HB berkala";
        }
        else if (usia > 15)
        {
            tAnemia.text = "Tidak termasuk remaja";
            Debug.Log("Tidak termasuk remaja");
            textRekomendasiAnemia = "";
        }
        else if(usia == 0 && hbValue != 0)
        {
            tAnemia.text = "Harap masukkan usia anda";
            Debug.Log("Invalid Input");
            textRekomendasiAnemia = "";
        }
        else if(usia != 0 && isHemo && hbValue == 0)
        {
            tAnemia.text = "Harap masukkan nilai Hb anda";
        }
        else if(usia != 0 && !isHemo)
        {
            tAnemia.text = "Belum melakukan test Hb";
            textRekomendasiAnemia = "- Segera lakukan tes HB dengan konsultasi ke tenaga kesehatan";
        }
        else
        {
            tAnemia.text = "Tidak Anemia";
            Debug.Log("Tidak Anemia");
            textRekomendasiAnemia = "- Tetap mengkonsumsi tablet tambah darah, terutama setelah masa haid";
        }
    }

    private void LILACount()
    {
        // if (isHamil)
        // {
            if (isUkurLILA)
            {
                if (lilaValue <= 23.5)
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
                    Debug.Log("Invalid Input");
                    textRekomendasiLILA = "";
                }
            }
            else
            {
                Debug.Log("Belum diukur LILA");
                textRekomendasiLILA = "Lakukan pengukuran lingkar lengan (LILA)";
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
        isBBValid = float.TryParse(input, out beratBadan);
        IMTFunc();
    }

    public void OnInputFieldTBEdit(string input)
    {
        isTBValid = float.TryParse(input, out tinggiBadan);
        IMTFunc();
    }

    private void IMTFunc()
    {
        if (isBBValid && isTBValid)
        {
            IMTCount(tinggiBadan, beratBadan);
        }
        else
        {
            Debug.Log("error broh");
        }
    }

    private void DropdownValueLILAChanged(Dropdown dropdown)
    {
        if (ddLILA.options[dropdown.value].text == "YA")
        {
            ifLILA.interactable = true;
            LILACount();
        }
        else
        {
            ifLILA.interactable = false;
            LILACount();
        }
    }

    private void DropdownValueHemoChanged(Dropdown dropdown)
    {
        if (ddHemo.options[dropdown.value].text == "YA")
        {
            ifHemo.interactable = true;
            isHemo = true;
            AnemiaCount();
        }
        else
        {
            ifHemo.interactable = false;
            isHemo = false;
            AnemiaCount();
        }
    }

    public void OnInputFieldUmurEdit(string input)
    {
        isUmurValid = int.TryParse(input, out usia);
        AnemiaCount();
    }

    public void OnInputFieldLILAEdit(string input)
    {
        isUkurLILA = float.TryParse(input, out lilaValue);
        LILACount();
    }

    public void OnInputFieldHemoEdit(string input)
    {
        isHemoValid = float.TryParse(input, out hbValue);
        AnemiaCount();
    }

    public void RekomendasiRemaja() // NANTI LANJUT INI
    {
        if (isBBValid && isTBValid && isUmurValid && isHemoValid)
        {
            tRekomendasiResult.text = textRekomendasiIMT + "\n" + "\n" + textRekomendasiAnemia + "\n" + "\n" + textRekomendasiLILA;
            tNamaRekomendasi.text = ifNama.text;
            tUsiaRekomendasi.text = ifUmur + " Tahun";
            tInfoStatusRekomendasi.text = "";
            panelManager.RemajaToResult();
        }
        else
        {
            Debug.Log("error broh");
        }
    }

}
