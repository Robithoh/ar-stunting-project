public class DataRemaja
{
    public string nama;
    public string umur;
    public int beratBadan;
    public int tinggiBadan;
    public float IMT;
    public string kIMT;
    public int lingkarLengan;
    public int hb;
    public string kAnemia;
    public string hasilRekomendasi;

    public DataRemaja(string _nama, string _umur, int _beratBadan, int _tinggiBadan, float _imt, string _kIMT, int _lingkarLengan, int _hb, string _kAnemia, string _hasilRekomendasi)
    {
        this.nama = _nama;
        this.umur = _umur;
        this.beratBadan = _beratBadan;
        this.tinggiBadan = _tinggiBadan;
        this.IMT = _imt;
        this.kIMT = _kIMT;
        this.lingkarLengan = _lingkarLengan;
        this.hb = _hb;
        this.kAnemia = _kAnemia;
        this.hasilRekomendasi = _hasilRekomendasi;
    }
}