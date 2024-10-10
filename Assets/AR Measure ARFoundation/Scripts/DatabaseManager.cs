using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using System;

public class DatabaseManager : MonoBehaviour
{
    // Firebase variables
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser User;
    public DatabaseReference DBreference;
    //private string UserId;

    [Header("Register")]
    public InputField Nama;
    public InputField Password;
    public InputField PasswordConfirm;
    public InputField TanggalLahir;
    public Dropdown PendidikanTerakhir;
    public Dropdown tHamil;
    public Dropdown tMenyusui;
    public Text WarningText;
    public GameObject EditButton;

    [Header("Text")]
    public Text NamaText;
    public Text TanggalLahirText;
    public Text PendidikanTerakhirText;
    public Text tHamilText;
    public Text tMenyusuiText;

    public Recommendation rekomendasi;

    void Awake()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                InitializeFirebase();
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
    }

    private void Start()
    {
        rekomendasi = FindObjectOfType<Recommendation>();
        if (rekomendasi == null)
        {
            Debug.LogError("Recommendation object not found in the scene!");
        }
    }

    private void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        auth = FirebaseAuth.DefaultInstance;
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void ClearRegisterFields()
    {
        Nama.text = "";
        Password.text = "";
        PasswordConfirm.text = "";
        TanggalLahir.text = "";
    }

    public void StartRegister()
    {
        StartCoroutine(Register(Nama.text, Password.text, PasswordConfirm.text, TanggalLahir.text));
    }

    public void SimpanRekomendasiIbuMenyusui()
    {
        string nama = rekomendasi.ifNamaIbuMenyusui.text;
        string umur = rekomendasi.ifUmurIbuMenyusui.text;
        string tAsi = rekomendasi.tInfoStatusRekomendasi.text;
        int beratBadan = int.Parse(rekomendasi.ifBeratBadanBayi.text);
        string rekomendasiIbuMenyusui = rekomendasi.tRekomendasiResult.text;

        StartCoroutine(RekomendasiIbuMenyusui(nama, umur, tAsi, beratBadan, rekomendasiIbuMenyusui));
    }

    public void SimpanRekomendasiIbuHamil()
    {
        string nama = rekomendasi.ifNamaIbuHamil.text;
        string umur = rekomendasi.ifUmurIbuHamil.text;
        int bb = int.Parse(rekomendasi.ifBeratBadanIbuHamil.text);
        int tb = int.Parse(rekomendasi.ifTinggiBadanIbuHamil.text);
        float imt = float.Parse(rekomendasi.tIMTIbuHamil.text);
        string klasIMT = rekomendasi.tKlasifikasiIMTIbuHamil.text;
        int LiLa = int.Parse(rekomendasi.ifLILAIbuHamil.text);
        int HB = int.Parse(rekomendasi.ifHemoIbuHamil.text);
        string tAnemia = rekomendasi.tAnemiaIbuHamil.text;
        string rekomendasiIbuHamil = rekomendasi.tRekomendasiResult.text;

        StartCoroutine(RekomendasiIbuHamil(nama, umur, bb, tb, imt, klasIMT, LiLa, HB, tAnemia, rekomendasiIbuHamil));
    }

    public void SimpanRekomendasiRemaja()
    {
        string nama = rekomendasi.ifNamaIbuHamil.text;
        string umur = rekomendasi.ifUmurIbuHamil.text;
        int bb = int.Parse(rekomendasi.ifBeratBadanIbuHamil.text);
        int tb = int.Parse(rekomendasi.ifTinggiBadanIbuHamil.text);
        float imt = float.Parse(rekomendasi.tIMTIbuHamil.text);
        string klasIMT = rekomendasi.tKlasifikasiIMTIbuHamil.text;
        int LiLa = int.Parse(rekomendasi.ifLILAIbuHamil.text);
        int HB = int.Parse(rekomendasi.ifHemoIbuHamil.text);
        string tAnemia = rekomendasi.tAnemiaIbuHamil.text;
        string rekomendasiRemaja = rekomendasi.tRekomendasiResult.text;

        StartCoroutine(RekomendasiRemaja(nama, umur, bb, tb, imt, klasIMT, LiLa, HB, tAnemia, rekomendasiRemaja));
    }

    public void SimpanRekomendasiAnakLaki()
    {
        int umur = int.Parse(rekomendasi.ifUmurLk.text);
        int tb = int.Parse(rekomendasi.ifTinggiBadanLk.text);
        string statGizi = rekomendasi.tGiziLk.text;
        string rekomendasiAnakLaki = rekomendasi.tRekomendasiResult.text;

        StartCoroutine(RekomendasiAnakLaki(umur, tb, statGizi, rekomendasiAnakLaki));
    }
    public void SimpanRekomendasiAnakPerempuan()
    {
        int umur = int.Parse(rekomendasi.ifUmurPr.text);
        int tb = int.Parse(rekomendasi.ifTinggiBadanPr.text);
        string statGizi = rekomendasi.tGiziPr.text;
        string rekomendasiAnakPerempuan = rekomendasi.tRekomendasiResult.text;

        StartCoroutine(RekomendasiAnakPerempuan(umur, tb, statGizi, rekomendasiAnakPerempuan));
    }

    private IEnumerator Register(string _username, string _password, string _passwordConfirm, string _tanggalLahir)
    {
        DateTime tanggal;
        bool isValid = DateTime.TryParseExact(_tanggalLahir, "dd-MM-yyyy",
                                              System.Globalization.CultureInfo.InvariantCulture,
                                              System.Globalization.DateTimeStyles.None,
                                              out tanggal);

        if (string.IsNullOrEmpty(_username))
        {
            WarningText.text = "Nama tidak boleh kosong";
            WarningText.color = Color.red;
        }
        else if (_password != _passwordConfirm)
        {
            WarningText.text = "Password dan Konfirmasi Password tidak cocok!";
            WarningText.color = Color.red;
        }
        else if (string.IsNullOrEmpty(_password) || string.IsNullOrEmpty(_passwordConfirm))
        {
            WarningText.text = "Password tidak boleh kosong";
            WarningText.color = Color.red;
        }
        else if (string.IsNullOrEmpty(_tanggalLahir))
        {
            WarningText.text = "Tanggal Lahir tidak boleh kosong";
            WarningText.color = Color.red;
        }
        else if (!isValid)
        {
            WarningText.text = "Format Tanggal Lahir harus dd-MM-yyyy!";
            WarningText.color = Color.red;
        }
        else
        {
            Task<AuthResult> RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(_username + "@dummy.com", _password);
            yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);

            if (RegisterTask.Exception != null)
            {
                Debug.LogWarning($"Failed to register task with {RegisterTask.Exception}");
                FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                string message = "Register Failed!";
                switch (errorCode)
                {
                    case AuthError.MissingEmail:
                        message = "Missing Email";
                        break;
                    case AuthError.MissingPassword:
                        message = "Missing Password";
                        break;
                    case AuthError.WeakPassword:
                        message = "Weak Password";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        message = "Email Already In Use";
                        break;
                }
                WarningText.text = message;
                WarningText.color = Color.red;
            }
            else
            {
                User = RegisterTask.Result.User;

                if (User != null)
                {
                    UserProfile profile = new UserProfile { DisplayName = _username };
                    Task ProfileTask = User.UpdateUserProfileAsync(profile);
                    yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

                    if (ProfileTask.Exception != null)
                    {
                        Debug.LogWarning($"Failed to set username: {ProfileTask.Exception}");
                        WarningText.text = "Username gagal disimpan!";
                        WarningText.color = Color.red;
                    }
                    else
                    {
                        string pendidikanTerakhir = PendidikanTerakhir.options[PendidikanTerakhir.value].text;
                        string hamil = tHamil.options[tHamil.value].text;
                        string menyusui = tMenyusui.options[tMenyusui.value].text;

                        User customUser = new User(_username, _password, _tanggalLahir, pendidikanTerakhir, hamil, menyusui);
                        string json = JsonUtility.ToJson(customUser);

                        DBreference.Child("users").Child(User.UserId).SetRawJsonValueAsync(json);

                        //TanggalLahirText.text = tanggal.ToString("dd-MM-yyyy");

                        int umur = HitungUmur(tanggal);

                        NamaText.text = _username;

                        if (menyusui == tMenyusui.options[0].text)
                        {
                            TanggalLahirText.text = umur + " tahun, Sedang Menyusui";
                        }
                        else
                        {
                            TanggalLahirText.text = umur + " tahun, Tidak Sedang Menyusui";
                        }

                        PendidikanTerakhirText.text = pendidikanTerakhir;
                        PanelManager.instance.SimpanEditProfile();
                        ClearRegisterFields();
                    }
                }
            }
        }
    }

    private int HitungUmur(DateTime tanggalLahir)
    {
        DateTime today = DateTime.Today;
        int umur = today.Year - tanggalLahir.Year;

        if (tanggalLahir.Date > today.AddYears(-umur))
        {
            umur--;
        }

        return umur;
    }

    public void EditDataProfile()
    {
        StartCoroutine(LoadUserData());
    }

    private IEnumerator LoadUserData()
    {
        EditButton.SetActive(true);
        var DBTask = DBreference.Child("users").Child(User.UserId).GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning($"Failed to load user data: {DBTask.Exception}");
        }
        else
        {
            DataSnapshot snapshot = DBTask.Result;

            Nama.text = snapshot.Child("nama").Value.ToString();
            Password.text = snapshot.Child("password").Value.ToString();
            PasswordConfirm.text = snapshot.Child("password").Value.ToString();
            TanggalLahir.text = snapshot.Child("tanggalLahir").Value.ToString();

            string hamil = snapshot.Child("statusHamil").Value.ToString();
            if (hamil == "YA")
            {
                tMenyusui.value = 0;
            }
            else
            {
                tMenyusui.value = 1;
            }

            string menyusui = snapshot.Child("statusMenyusui").Value.ToString();
            if (menyusui == "YA")
            {
                tMenyusui.value = 0;
            }
            else
            {
                tMenyusui.value = 1;
            }
        }
    }

    public void SimpanEdit()
    {
        StartCoroutine(UpdateUserProfile(Nama.text, Password.text, PasswordConfirm.text, TanggalLahir.text));
    }

    private IEnumerator UpdateUserProfile(string _username, string _password, string _passwordConfirm, string _tanggalLahir)
    {
        DateTime tanggal;
        bool isValid = DateTime.TryParseExact(_tanggalLahir, "dd-MM-yyyy",
                                              System.Globalization.CultureInfo.InvariantCulture,
                                              System.Globalization.DateTimeStyles.None,
                                              out tanggal);

        if (string.IsNullOrEmpty(_username))
        {
            WarningText.text = "Nama tidak boleh kosong";
            WarningText.color = Color.red;
        }
        else if (_password != _passwordConfirm)
        {
            WarningText.text = "Password dan Konfirmasi Password tidak cocok!";
            WarningText.color = Color.red;
        }
        else if (string.IsNullOrEmpty(_password) || string.IsNullOrEmpty(_passwordConfirm))
        {
            WarningText.text = "Password tidak boleh kosong";
            WarningText.color = Color.red;
        }
        else if (string.IsNullOrEmpty(_tanggalLahir))
        {
            WarningText.text = "Tanggal Lahir tidak boleh kosong";
            WarningText.color = Color.red;
        }
        else if (!isValid)
        {
            WarningText.text = "Format Tanggal Lahir harus dd-MM-yyyy!";
            WarningText.color = Color.red;
        }
        else
        {
            UserProfile profile = new UserProfile { DisplayName = _username };
            Task ProfileTask = User.UpdateUserProfileAsync(profile);
            yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

            if (ProfileTask.Exception != null)
            {
                Debug.LogWarning($"Failed to update username: {ProfileTask.Exception}");
                WarningText.text = "Gagal memperbarui nama pengguna!";
                WarningText.color = Color.red;
            }
            else
            {
                string _pendidikanTerakhir = PendidikanTerakhir.options[PendidikanTerakhir.value].text;
                string _hamil = tHamil.options[tHamil.value].text;
                string _menyusui = tMenyusui.options[tMenyusui.value].text;

                User customUser = new User(_username, _password, _pendidikanTerakhir, _tanggalLahir, _hamil, _menyusui);
                string json = JsonUtility.ToJson(customUser);

                Task DBTask = DBreference.Child("users").Child(User.UserId).SetRawJsonValueAsync(json);
                yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

                if (DBTask.Exception != null)
                {
                    Debug.LogWarning($"Failed to update user data: {DBTask.Exception}");
                    WarningText.text = "Gagal memperbarui profil!";
                    WarningText.color = Color.red;
                }
                else
                {
                    int umur = HitungUmur(tanggal);

                    NamaText.text = _username;

                    if (_menyusui == tMenyusui.options[0].text)
                    {
                        TanggalLahirText.text = umur + " tahun, Sedang Menyusui";
                    }
                    else
                    {
                        TanggalLahirText.text = umur + " tahun, Tidak Sedang Menyusui";
                    }

                    PendidikanTerakhirText.text = _pendidikanTerakhir;
                    PanelManager.instance.SimpanEditProfile();
                    EditButton.SetActive(false);
                    ClearRegisterFields();
                }
            }
        }
    }

    private IEnumerator RekomendasiIbuMenyusui(string _nama, string _umur, string _tAsi, int _beratBadan, string _hasilRekomendasi)
    {
        DataIbuMenyusui rekIbuMenyusui = new DataIbuMenyusui(_nama, _umur, _tAsi, _beratBadan, _hasilRekomendasi);

        string json = JsonUtility.ToJson(rekIbuMenyusui);

        Task dbTask = DBreference.Child("rekomendasi_ibu_menyusui").SetRawJsonValueAsync(json);
        yield return new WaitUntil(() => dbTask.IsCompleted);

        if (dbTask.Exception != null)
        {
            Debug.LogError($"Gagal menyimpan rekomendasi ibu menyusui: {dbTask.Exception}");
            WarningText.text = "Gagal menyimpan rekomendasi!";
        }
        else
        {
            Debug.Log("Rekomendasi ibu menyusui berhasil disimpan.");
            WarningText.text = "Rekomendasi ibu menyusui berhasil disimpan!";
        }
    }

    private IEnumerator RekomendasiIbuHamil(string _nama, string _umur, int _beratBadan, int _tinggiBadan, float _imt, string _kIMT, int _lingkarLengan, int _hb, string _kAnemia, string _hasilRekomendasi)
    {
        DataIbuHamil rekIbuHamil = new DataIbuHamil( _nama, _umur, _beratBadan, _tinggiBadan, _imt, _kIMT, _lingkarLengan, _hb, _kAnemia, _hasilRekomendasi);

        string json = JsonUtility.ToJson(rekIbuHamil);

        Task dbTask = DBreference.Child("rekomendasi_ibu_hamil").SetRawJsonValueAsync(json);
        yield return new WaitUntil(() => dbTask.IsCompleted);

        if (dbTask.Exception != null)
        {
            Debug.LogError($"Gagal menyimpan rekomendasi ibu hamil: {dbTask.Exception}");
            WarningText.text = "Gagal menyimpan rekomendasi!";
        }
        else
        {
            Debug.Log("Rekomendasi ibu hamil berhasil disimpan.");
            WarningText.text = "Rekomendasi ibu hamil berhasil disimpan!";
        }
    }

    private IEnumerator RekomendasiRemaja(string _nama, string _umur, int _beratBadan, int _tinggiBadan, float _imt, string _kIMT, int _lingkarLengan, int _hb, string _kAnemia, string _hasilRekomendasi)
    {
        DataRemaja rekRemaja = new DataRemaja(_nama, _umur, _beratBadan, _tinggiBadan, _imt, _kIMT, _lingkarLengan, _hb, _kAnemia, _hasilRekomendasi);

        string json = JsonUtility.ToJson(rekRemaja);

        Task dbTask = DBreference.Child("rekomendasi_remaja").SetRawJsonValueAsync(json);
        yield return new WaitUntil(() => dbTask.IsCompleted);

        if (dbTask.Exception != null)
        {
            Debug.LogError($"Gagal menyimpan rekomendasi Remaja: {dbTask.Exception}");
            WarningText.text = "Gagal menyimpan rekomendasi!";
        }
        else
        {
            Debug.Log("Rekomendasi Remaja berhasil disimpan.");
            WarningText.text = "Rekomendasi Remaja berhasil disimpan!";
        }
    }

    private IEnumerator RekomendasiAnakLaki(int _umur, int _tinggiBadan, string _statGizi, string _hasilRekomendasi)
    {
        DataAnakLaki rekAnakLaki = new DataAnakLaki( _umur, _tinggiBadan, _statGizi, _hasilRekomendasi);

        string json = JsonUtility.ToJson(rekAnakLaki);

        Task dbTask = DBreference.Child("rekomendasi_anak_laki-laki").SetRawJsonValueAsync(json);
        yield return new WaitUntil(() => dbTask.IsCompleted);

        if (dbTask.Exception != null)
        {
            Debug.LogError($"Gagal menyimpan rekomendasi Anak laki-laki: {dbTask.Exception}");
            WarningText.text = "Gagal menyimpan rekomendasi!";
        }
        else
        {
            Debug.Log("Rekomendasi Anak laki-laki berhasil disimpan.");
            WarningText.text = "Rekomendasi Anak laki-laki berhasil disimpan!";
        }
    }

    private IEnumerator RekomendasiAnakPerempuan(int _umur, int _tinggiBadan, string _statGizi, string _hasilRekomendasi)
    {
        DataAnakPerempuan rekAnakPerempuan = new DataAnakPerempuan(_umur, _tinggiBadan, _statGizi, _hasilRekomendasi);

        string json = JsonUtility.ToJson(rekAnakPerempuan);

        Task dbTask = DBreference.Child("rekomendasi_anak_perempuan").SetRawJsonValueAsync(json);
        yield return new WaitUntil(() => dbTask.IsCompleted);

        if (dbTask.Exception != null)
        {
            Debug.LogError($"Gagal menyimpan rekomendasi Anak Perempuan: {dbTask.Exception}");
            WarningText.text = "Gagal menyimpan rekomendasi!";
        }
        else
        {
            Debug.Log("Rekomendasi Anak Perempuan berhasil disimpan.");
            WarningText.text = "Rekomendasi Anak Perempuan berhasil disimpan!";
        }
    }
}
