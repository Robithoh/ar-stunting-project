using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataProfile", menuName = "Profile")]
public class DataProfile : ScriptableObject
{
    public string nama;
    public string username;
    public string email;
    public string password;
    public string tanggalLahir;
    public string pendidikanTerakhir;
    public string hamil;
    public string menyusui;
    public string toilet;
    public string aksesAir;

    public int umur;
}
