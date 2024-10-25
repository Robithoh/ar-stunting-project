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
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser User;
    public DatabaseReference DBreference;

    [Header("Register")]
    public InputField Nama;
    public InputField Email;
    public InputField Password;
    public InputField PasswordConfirm;
    public InputField TanggalLahir;
    public Dropdown PendidikanTerakhir;
    public Dropdown tHamil;
    public Dropdown tMenyusui;
    public Dropdown tToilet;
    public Dropdown tAir;
    public Text WarningText;
    public GameObject warningBox;
    public GameObject EditButton;
    public GameObject BatalEditButton;

    [Header("Login")]
    public InputField LuserName;
    public InputField Lpassword;
    public Text Message;

    [Header("Text")]
    public Text NamaText;
    public Text TanggalLahirText;
    public Text PendidikanTerakhirText;
    public Text NamaMenuText;
    public Text KeteranganMenuText;
    public Text ToiletText;
    public Text AirText;
    public Text emailText;

    [Header("Button Main Menu")]
    public Button bRemaja;
    public Button bIbuHamil;
    public Button bIbuMenyusui;
    public Button bAnakLK;
    public Button bAnakPr;

    [Header("Pop Up")]
    public GameObject popUpMainMenu;
    private bool isFirstTimePopUp = false;
    public Text popUpTextHeader;
    public Text popUpTextContent;
   

    [Header("References")]
    private PanelManager PanelManager;
    private Recommendation rekomendasi;
    public DataProfile userData;

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
        PanelManager = FindObjectOfType<PanelManager>();
        if (rekomendasi == null)
        {
            Debug.LogError("Recommendation object not found in the scene!");
        }
        LoadProfileData();

         bIbuMenyusui.interactable = false;
        bIbuHamil.interactable = false;
        bAnakLK.interactable = false;
        bAnakPr.interactable = false;
        bRemaja.interactable = false;
    }

    private void Update()
    {
        LoadProfileData();

        if(PanelManager.mainMenu.GetComponent<Canvas>().enabled)
        {
            EnableButtonMainMenu();
            
            if(!isFirstTimePopUp)
            {
                popUpMainMenu.SetActive(true);
                isFirstTimePopUp = true;

                if(userData.menyusui == tMenyusui.options[0].text && userData.hamil == tHamil.options[0].text && userData.umur <= 15)
                {
                    popUpTextHeader.text = "Status Anda adalah Remaja, ibu Hamil, dan Ibu Menyusui, Anda bisa mengakses menu:";
                    popUpTextContent.text = "Remaja\nIbu Hamil\nIbu Menyusui\nAnak Laki-laki\nAnak Perempuan";
                }
                else if(userData.menyusui == tMenyusui.options[0].text && userData.hamil == tHamil.options[0].text && userData.umur > 15)
                {
                    popUpTextHeader.text = "Status Anda adalah ibu Hamil, dan Ibu Menyusui, Anda bisa mengakses menu:";
                    popUpTextContent.text = "Ibu Hamil\nIbu Menyusui\nAnak Laki-laki\nAnak Perempuan";
                }
                else if(userData.menyusui == tMenyusui.options[0].text && userData.hamil != tHamil.options[0].text && userData.umur <= 15)
                {
                    popUpTextHeader.text = "Status Anda adalah Ibu Menyusui dan Remaja, Anda bisa mengakses menu:";
                    popUpTextContent.text = "Remaja\nIbu Menyusui\nAnak Laki-laki\nAnak Perempuan";
                }
                else if(userData.menyusui == tMenyusui.options[0].text && userData.hamil != tHamil.options[0].text && userData.umur > 15)
                {
                    popUpTextHeader.text = "Status Anda adalah Ibu Menyusui, Anda hanya bisa mengakses menu:";
                    popUpTextContent.text = "Ibu Menyusui\nAnak Laki-laki\nAnak Perempuan";
                }
                else if(userData.menyusui != tMenyusui.options[0].text && userData.hamil == tHamil.options[0].text && userData.umur < 15)
                {
                    popUpTextHeader.text = "Status Anda adalah Ibu Hamil dan Remaja, Anda bisa mengakses menu:";
                    popUpTextContent.text = "Ibu Hamil\nRemaja";
                }
                else if(userData.menyusui != tMenyusui.options[0].text && userData.hamil == tHamil.options[0].text && userData.umur > 15)
                {
                    popUpTextHeader.text = "Status Anda adalah Ibu Hamil, Anda hanya bisa mengakses menu:";
                    popUpTextContent.text = "Ibu Hamil";
                }
                else if(userData.menyusui != tMenyusui.options[0].text && userData.hamil != tHamil.options[0].text && userData.umur < 15)
                {
                    popUpTextHeader.text = "Status Anda adalah Remaja, Anda hanya bisa mengakses menu:";
                    popUpTextContent.text = "Remaja";
                }
                else if(userData.menyusui != tMenyusui.options[0].text && userData.hamil != tHamil.options[0].text && userData.umur > 15)
                {
                    popUpTextHeader.text = "Status Anda tidak ada dalam daftar, Anda hanya bisa mengakses menu:";
                    popUpTextContent.text = "Ukur Tinggi Badan";
                }
            }
        }
    }

    public void LoadProfileData()
    {
        NamaText.text = userData.username;
        NamaMenuText.text = userData.username;

        if (userData.menyusui == tMenyusui.options[0].text)
        {
            TanggalLahirText.text = userData.umur + " tahun, Sedang Menyusui";
            KeteranganMenuText.text = "Sedang Menyusui";
        }
        else
        {
            TanggalLahirText.text = userData.umur + " tahun, Tidak Sedang Menyusui";
            KeteranganMenuText.text = "Tidak Sedang Menyusui";
        }

        PendidikanTerakhirText.text = userData.pendidikanTerakhir;

        ToiletText.text = userData.toilet;
        AirText.text = userData.aksesAir;
        emailText.text = userData.email;
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
        StartCoroutine(Register(Nama.text, Email.text, Password.text, PasswordConfirm.text, TanggalLahir.text));
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
        string LiLa = rekomendasi.ifLILAIbuHamil.text;
        string HB = rekomendasi.ifHemoIbuHamil.text;
        string tAnemia = rekomendasi.tAnemiaIbuHamil.text;
        string rekomendasiIbuHamil = rekomendasi.tRekomendasiResult.text;

        StartCoroutine(RekomendasiIbuHamil(nama, umur, bb, tb, imt, klasIMT, LiLa, HB, tAnemia, rekomendasiIbuHamil));
    }

    public void SimpanRekomendasiRemaja()
    {
        string nama = rekomendasi.ifNamaRemaja.text;
        string umur = rekomendasi.ifUmurRemaja.text;
        int bb = int.Parse(rekomendasi.ifBeratBadanRemaja.text);
        int tb = int.Parse(rekomendasi.ifTinggiBadanRemaja.text);
        float imt = float.Parse(rekomendasi.tIMTRemaja.text);
        string klasIMT = rekomendasi.tKlasifikasiIMTRemaja.text;
        string LiLa = rekomendasi.ifLILARemaja.text;
        string HB = rekomendasi.ifHemoRemaja.text;
        string tAnemia = rekomendasi.tAnemiaRemaja.text;
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

    public void StartLogin()
    {
        StartCoroutine(Login(LuserName.text, Lpassword.text));
    }

    private IEnumerator Login(string _usernameOrEmail, string _password)
    {
        if (string.IsNullOrEmpty(_usernameOrEmail))
        {
            Message.text = "Username atau Email tidak boleh kosong";
            Message.color = Color.red;
        }
        else if (string.IsNullOrEmpty(_password))
        {
            Message.text = "Password tidak boleh kosong";
            Message.color = Color.red;
        }
        else
        {
            string loginEmail = _usernameOrEmail.Contains("@") ? _usernameOrEmail : _usernameOrEmail + "@gmail.com";

            Task<AuthResult> LoginTask = auth.SignInWithEmailAndPasswordAsync(loginEmail, _password);
            yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

            if (LoginTask.Exception != null)
            {
                Debug.LogWarning($"Failed to login task with {LoginTask.Exception}");
                FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                string message = "Login Failed!";
                switch (errorCode)
                {
                    case AuthError.MissingEmail:
                        message = "Email is missing";
                        break;
                    case AuthError.MissingPassword:
                        message = "Password is missing";
                        break;
                    case AuthError.WrongPassword:
                        message = "Incorrect password";
                        break;
                    case AuthError.InvalidEmail:
                        message = "Invalid email format";
                        break;
                    case AuthError.UserNotFound:
                        message = "No user with this email";
                        break;
                }
                Message.text = message;
                Message.color = Color.red;
            }
            else
            {
                User = LoginTask.Result.User;
                if (User != null)
                {
                    Debug.Log("User logged in successfully");

                    StartCoroutine(LoadUserData());
                    PanelManager.instance.SimpanEditProfile();
                }
            }
        }
    }

    private IEnumerator Register(string _username, string _email, string _password, string _passwordConfirm, string _tanggalLahir)
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
            warningBox.SetActive(true);
        }
        else if (_password != _passwordConfirm)
        {
            WarningText.text = "Password dan Konfirmasi Password tidak cocok!";
            WarningText.color = Color.red;
            warningBox.SetActive(true);
        }
        else if (string.IsNullOrEmpty(_password) || string.IsNullOrEmpty(_passwordConfirm))
        {
            WarningText.text = "Password tidak boleh kosong";
            WarningText.color = Color.red;
            warningBox.SetActive(true);
        }
        else if (string.IsNullOrEmpty(_tanggalLahir))
        {
            WarningText.text = "Tanggal Lahir tidak boleh kosong";
            WarningText.color = Color.red;
            warningBox.SetActive(true);
        }
        else if (!isValid)
        {
            WarningText.text = "Format Tanggal Lahir harus dd-MM-yyyy!";
            WarningText.color = Color.red;
            warningBox.SetActive(true);
        }
        else
        {
            Task<AuthResult> RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(_email, _password);
            yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);

            if (RegisterTask.Exception != null)
            {
                Debug.LogWarning($"Failed to register task with {RegisterTask.Exception}");
                FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                warningBox.SetActive(true);

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
                        string toilet = tToilet.options[tToilet.value].text;
                        string air = tAir.options[tAir.value].text;

                        userData.username = _username;
                        userData.email = _email;
                        userData.password = _password;
                        userData.tanggalLahir = _tanggalLahir;
                        userData.pendidikanTerakhir = pendidikanTerakhir;
                        userData.hamil = hamil;
                        userData.menyusui = menyusui;
                        userData.toilet = toilet;
                        userData.aksesAir = air;

                        int umur = HitungUmur(tanggal);
                        userData.umur = umur;

                        User customUser = new User(userData.username, userData.email, userData.password, userData.tanggalLahir, userData.pendidikanTerakhir, userData.hamil, userData.menyusui, userData.toilet, userData.aksesAir);
                        string json = JsonUtility.ToJson(customUser);

                        DBreference.Child("users").Child(User.UserId).SetRawJsonValueAsync(json);

                        PanelManager.instance.LoginScreen();
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
        BatalEditButton.SetActive(true);

        var DBTask = DBreference.Child("users").Child(User.UserId).GetValueAsync();
        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning($"Failed to load user data: {DBTask.Exception}");
        }
        else
        {
            DataSnapshot snapshot = DBTask.Result;

            if (snapshot.Exists)
            {
                Nama.text = userData.username;
                Email.text = userData.email;
                Password.text = userData.password;
                PasswordConfirm.text = userData.password;
                TanggalLahir.text = userData.tanggalLahir;

                userData.username = snapshot.Child("nama").Value.ToString();
                userData.email = snapshot.Child("email").Value.ToString();
                userData.password = snapshot.Child("password").Value.ToString();
                userData.tanggalLahir = snapshot.Child("tanggalLahir").Value.ToString();
                userData.pendidikanTerakhir = snapshot.Child("pendidikanTerakhir").Value.ToString();

                string hamil = snapshot.Child("statusHamil").Value.ToString();
                userData.hamil = hamil;
                if (hamil == "YA")
                {
                    tHamil.value = 0;
                }
                else
                {
                    tHamil.value = 1;
                }

                string menyusui = snapshot.Child("statusMenyusui").Value.ToString();
                userData.menyusui = menyusui; 
                if (menyusui == "YA")
                {
                    tMenyusui.value = 0; 
                }
                else
                {
                    tMenyusui.value = 1;
                }

                string infoToilet = snapshot.Child("infoToilet").Value.ToString();
                userData.toilet = infoToilet;
                if (infoToilet == "Ada")
                {
                    tToilet.value = 0;
                }
                else
                {
                    tToilet.value = 1;
                }

                string infoAir = snapshot.Child("infoAir").Value.ToString();
                userData.aksesAir = infoAir;
                if (infoAir == "Ada")
                {
                    tAir.value = 0;
                }
                else
                {
                    tAir.value = 1;
                }
            }
            else
            {
                Debug.LogWarning("No data found for this user.");
            }
        }
    }

    public void SimpanEdit()
    {
        StartCoroutine(UpdateUserProfile(Nama.text, Email.text, Password.text, PasswordConfirm.text, TanggalLahir.text));
    }

    private IEnumerator UpdateUserProfile(string _username, string _email, string _password, string _passwordConfirm, string _tanggalLahir)
    {
        DateTime tanggal;
        bool isValid = DateTime.TryParseExact(_tanggalLahir, "dd-MM-yyyy",
                                              System.Globalization.CultureInfo.InvariantCulture,
                                              System.Globalization.DateTimeStyles.None,
                                              out tanggal);

        if (string.IsNullOrEmpty(_username))
        {
            SetWarning("Nama tidak boleh kosong");
        }
        else if (_password != _passwordConfirm)
        {
            SetWarning("Password dan Konfirmasi Password tidak cocok!");
        }
        else if (string.IsNullOrEmpty(_password) || string.IsNullOrEmpty(_passwordConfirm))
        {
            SetWarning("Password tidak boleh kosong");
        }
        else if (string.IsNullOrEmpty(_tanggalLahir))
        {
            SetWarning("Tanggal Lahir tidak boleh kosong");
        }
        else if (!isValid)
        {
            SetWarning("Format Tanggal Lahir harus dd-MM-yyyy!");
        }
        else
        {
            UserProfile profile = new UserProfile { DisplayName = _username };
            Task ProfileTask = User.UpdateUserProfileAsync(profile);
            yield return new WaitUntil(() => ProfileTask.IsCompleted);

            if (ProfileTask.Exception != null)
            {
                Debug.LogWarning($"Failed to update username: {ProfileTask.Exception}");
                SetWarning("Gagal memperbarui username!");
            }
            else
            {
                string _pendidikanTerakhir = PendidikanTerakhir.options[PendidikanTerakhir.value].text;
                string _hamil = tHamil.options[tHamil.value].text;
                string _menyusui = tMenyusui.options[tMenyusui.value].text;
                string toilet = tToilet.options[tToilet.value].text;
                string air = tAir.options[tAir.value].text;

                userData.username = _username;
                userData.email = _email;
                userData.password = _password;
                userData.tanggalLahir = _tanggalLahir;
                userData.pendidikanTerakhir = _pendidikanTerakhir;
                userData.hamil = _hamil;
                userData.menyusui = _menyusui;
                userData.toilet = toilet;
                userData.aksesAir = air;

                User customUser = new User(userData.username, userData.email, userData.password, userData.tanggalLahir, userData.pendidikanTerakhir, userData.hamil, userData.menyusui, userData.toilet, userData.aksesAir);
                string json = JsonUtility.ToJson(customUser);

                var DBTask = DBreference.Child("users").Child(User.UserId).SetRawJsonValueAsync(json);
                yield return new WaitUntil(() => DBTask.IsCompleted);

                if (DBTask.Exception != null)
                {
                    Debug.LogWarning($"Failed to update user data: {DBTask.Exception}");
                    SetWarning("Gagal memperbarui profil!");
                }
                else
                {
                    int umur = HitungUmur(tanggal);
                    userData.umur = umur;

                    NamaText.text = _username;
                    NamaMenuText.text = _username;

                    string menyusuiText = (_menyusui == tMenyusui.options[0].text) ? "Sedang Menyusui" : "Tidak Sedang Menyusui";
                    TanggalLahirText.text = $"{umur} tahun, {menyusuiText}";
                    KeteranganMenuText.text = menyusuiText;

                    PendidikanTerakhirText.text = _pendidikanTerakhir;

                    PanelManager.instance.SimpanEditProfile();
                    EditButton.SetActive(false);
                    BatalEditButton.SetActive(false);
                    ClearRegisterFields();
                }
            }
        }
    }

    private void SetWarning(string message)
    {
        warningBox.SetActive(true);
        WarningText.text = message;
        WarningText.color = Color.red;
    }

    private IEnumerator RekomendasiIbuMenyusui(string _nama, string _umur, string _tAsi, int _beratBadan, string _hasilRekomendasi)
    {
        DataIbuMenyusui rekIbuMenyusui = new DataIbuMenyusui(_nama, _umur, _tAsi, _beratBadan, _hasilRekomendasi);

        string json = JsonUtility.ToJson(rekIbuMenyusui);

        string tanggal = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
        Task dbTask = DBreference.Child("rekomendasi_ibu_menyusui").Child(tanggal).SetRawJsonValueAsync(json);
        yield return new WaitUntil(() => dbTask.IsCompleted);

        if (dbTask.Exception != null)
        {
            Debug.LogError($"Gagal menyimpan rekomendasi ibu menyusui: {dbTask.Exception}");
        }
        else
        {
            PanelManager.instance.ToResult();
            Debug.Log("Rekomendasi ibu menyusui berhasil disimpan.");
        }
    }

    private IEnumerator RekomendasiIbuHamil(string _nama, string _umur, int _beratBadan, int _tinggiBadan, float _imt, string _kIMT, string _lingkarLengan, string _hb, string _kAnemia, string _hasilRekomendasi)
    {
        string Lila = string.IsNullOrEmpty(_lingkarLengan) ? "unknown" : _lingkarLengan;
        string habe = string.IsNullOrEmpty(_hb) ? "unknown" : _hb;

        DataIbuHamil rekIbuHamil = new DataIbuHamil(_nama, _umur, _beratBadan, _tinggiBadan, _imt, _kIMT, Lila, habe, _kAnemia, _hasilRekomendasi);
        string json = JsonUtility.ToJson(rekIbuHamil);

        string tanggal = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
        Task dbTask = DBreference.Child("rekomendasi_ibu_hamil").Child(tanggal).SetRawJsonValueAsync(json);
        yield return new WaitUntil(() => dbTask.IsCompleted);

        if (dbTask.Exception != null)
        {
            Debug.LogError($"Gagal menyimpan rekomendasi ibu hamil: {dbTask.Exception}");
        }
        else
        {
            PanelManager.instance.ToResult();
            Debug.Log("Rekomendasi ibu hamil berhasil disimpan.");
        }
    }

    private IEnumerator RekomendasiRemaja(string _nama, string _umur, int _beratBadan, int _tinggiBadan, float _imt, string _kIMT, string _lingkarLengan, string _hb, string _kAnemia, string _hasilRekomendasi)
    {
        string Lila = string.IsNullOrEmpty(_lingkarLengan) ? "unknown" : _lingkarLengan;
        string habe = string.IsNullOrEmpty(_hb) ? "unknown" : _hb;

        DataRemaja rekRemaja = new DataRemaja(_nama, _umur, _beratBadan, _tinggiBadan, _imt, _kIMT, Lila, habe, _kAnemia, _hasilRekomendasi);
        string json = JsonUtility.ToJson(rekRemaja);

        string tanggal = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
        Task dbTask = DBreference.Child("rekomendasi_remaja").Child(tanggal).SetRawJsonValueAsync(json);
        yield return new WaitUntil(() => dbTask.IsCompleted);

        if (dbTask.Exception != null)
        {
            Debug.LogError($"Gagal menyimpan rekomendasi Remaja: {dbTask.Exception}");
        }
        else
        {
            PanelManager.instance.ToResult();
            Debug.Log("Rekomendasi Remaja berhasil disimpan.");
        }
    }

    private IEnumerator RekomendasiAnakLaki(int _umur, int _tinggiBadan, string _statGizi, string _hasilRekomendasi)
    {
        DataAnakLaki rekAnakLaki = new DataAnakLaki( _umur, _tinggiBadan, _statGizi, _hasilRekomendasi);

        string json = JsonUtility.ToJson(rekAnakLaki);

        string tanggal = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
        Task dbTask = DBreference.Child("rekomendasi_anak_laki-laki").Child(tanggal).SetRawJsonValueAsync(json);
        yield return new WaitUntil(() => dbTask.IsCompleted);

        if (dbTask.Exception != null)
        {
            Debug.LogError($"Gagal menyimpan rekomendasi Anak laki-laki: {dbTask.Exception}");
        }
        else
        {
            PanelManager.instance.ToResult();
            Debug.Log("Rekomendasi Anak laki-laki berhasil disimpan.");
        }
    }

    private IEnumerator RekomendasiAnakPerempuan(int _umur, int _tinggiBadan, string _statGizi, string _hasilRekomendasi)
    {
        DataAnakPerempuan rekAnakPerempuan = new DataAnakPerempuan(_umur, _tinggiBadan, _statGizi, _hasilRekomendasi);

        string json = JsonUtility.ToJson(rekAnakPerempuan);

        string tanggal = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
        Task dbTask = DBreference.Child("rekomendasi_anak_perempuan").Child(tanggal).SetRawJsonValueAsync(json);
        yield return new WaitUntil(() => dbTask.IsCompleted);

        if (dbTask.Exception != null)
        {
            Debug.LogError($"Gagal menyimpan rekomendasi Anak Perempuan: {dbTask.Exception}");
        }
        else
        {
            PanelManager.instance.ToResult();
            Debug.Log("Rekomendasi Anak Perempuan berhasil disimpan.");
        }
    }

    public void CloseWarningBox()
    {
        warningBox.SetActive(false);
    }

    public void EnableButtonMainMenu()
    {
        // bIbuMenyusui.interactable = false;
        // bIbuHamil.interactable = false;
        // bAnakLK.interactable = false;
        // bAnakPr.interactable = false;
        // bRemaja.interactable = false;

        if(userData.menyusui == tMenyusui.options[0].text)
        {
            bIbuMenyusui.interactable = true;
            bAnakLK.interactable = true;
            bAnakPr.interactable = true;
        }
        else
        {
            bIbuMenyusui.interactable = false;
            bAnakLK.interactable = false;
            bAnakPr.interactable = false;
        }

        if(userData.hamil == tHamil.options[0].text)
        {
            bIbuHamil.interactable = true;
        }
        else
        {
            bIbuHamil.interactable = false;
        }

        if(userData.umur <= 15 )
        {
            bRemaja.interactable = true;
        }
        else
        {
            bRemaja.interactable = false;
        }
    }

    public void ClosePopUpMainMenu()
    {
        popUpMainMenu.SetActive(false);
    }
}
