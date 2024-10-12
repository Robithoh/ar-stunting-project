public class User
{
    public string nama;
    public string password;
    public string tanggalLahir;
    public string pendidikanTerakhir;
    public string statusHamil;
    public string statusMenyusui;

    public User(string _nama, string _password, string _tanggalLahir, string _pendidikanTerakhir, string _statusHamil, string _statusMenyusui)
    {
        nama = _nama;
        password = _password;
        tanggalLahir = _tanggalLahir;
        pendidikanTerakhir = _pendidikanTerakhir;
        statusHamil = _statusHamil;
        statusMenyusui = _statusMenyusui;
    }
}