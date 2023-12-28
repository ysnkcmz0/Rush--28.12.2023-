using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Instance
    public static GameManager Instance;
    private void Awake() { if (Instance == null) Instance = this; }
    #endregion

    [SerializeField] private TextMeshProUGUI skortext;
    private int _skor = 0;
    
    void Start()
    {
        AdsManager.Instance.BannerAD();
        SetSkorText();
    }

    public void SetSkorText()
    {
        skortext.text = _skor.ToString();
    }

    public int Skor { get { return _skor; } set { _skor = value; SetSkorText(); } }
}
