using UnityEngine;
using UnityEngine.Events;

public class AdsManager : MonoBehaviour
{
    #region Instance
    public static AdsManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        Destroy(gameObject);
    }
    #endregion

    private GoogleAdmobSC sc;
    public UnityAction RewardedCallBack, InterstitialCallBack;

    private void Start()
    {
        sc = GetComponent<GoogleAdmobSC>();
        BannerAD();
    }

    public void BannerAD(bool v = true)
    {
        if (v) sc.LoadBannerAd();
        else sc.DestroyBannerAd();
    }

    public void InterstitialAD()
    {
        sc.InterstitialCallBack = InterstitialCallBack;
        sc.ShowInterstitialAd();
    }

    public void RewardedAD()
    {
        sc.RewardedCallBack = RewardedCallBack;
        sc.ShowRewardedAd();
    }

    public bool IsRewardedReady { get { return sc.IsRewardedReady; } }
}
