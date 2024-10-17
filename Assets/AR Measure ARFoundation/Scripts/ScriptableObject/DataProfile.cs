using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataProfile", menuName = "Profile", order = 1)]
public class DataProfile : ScriptableObject
{
    public string username;
    public string password;
    public string tanggalLahir;
    public string pendidikanTerakhir;
    public string hamil;
    public string menyusui;

    public int umur;
}
