public class User
{
    public string nama;
    public string username;
    public string email;
    public string password;
    public string tanggalLahir;
    public string pendidikanTerakhir;
    public string statusHamil;
    public string statusMenyusui;
    public string infoToilet;
    public string infoAir;

    public User(string _nama, string _username, string _email, string _password, string _tanggalLahir, string _pendidikanTerakhir, string _statusHamil, string _statusMenyusui, string _infoToilet, string _infoAir)
    {
        nama = _nama;
        username = _username;
        email = _email;
        password = _password;
        tanggalLahir = _tanggalLahir;
        pendidikanTerakhir = _pendidikanTerakhir;
        statusHamil = _statusHamil;
        statusMenyusui = _statusMenyusui;
        infoToilet = _infoToilet;
        infoAir = _infoAir;
    }
}