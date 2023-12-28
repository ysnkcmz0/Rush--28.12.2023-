using GoogleMobileAds;
using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.Events;

public class GoogleAdmobSC : MonoBehaviour
{
    #region Main
    [SerializeField] private bool IsTest = false;
    [SerializeField] private string _BannerAdID, _RewardedAdID, _InterstitialAdID;
    private string _TestBannerAdID = "ca-app-pub-3558488567081433/5310553887", 
        _TestRewardedAdID = "ca-app-pub-3558488567081433/6945486603", 
        _TestInterstitialAdID = "ca-app-pub-3558488567081433/1683080314";
    private BannerView _bannerAd;
    private InterstitialAd _interstitialAd;
    private RewardedAd _rewardedAd;
    private int repeatCount = 0;
    public UnityAction RewardedCallBack, InterstitialCallBack;


    private void Start()
    {
        MobileAds.Initialize((InitializationStatus initStatus) => { });
        LoadBannerAd();
        LoadInterstitialAd();
        LoadRewardedAd();
    }
    #endregion

    #region Interstitial
    private void LoadInterstitialAd()
    {
        if (_interstitialAd != null)
        {
            _interstitialAd.Destroy();
            _interstitialAd = null;
        }
        var adRequest = new AdRequest();
        InterstitialAd.Load(IsTest? _TestInterstitialAdID : _InterstitialAdID, adRequest,
            (InterstitialAd ad, LoadAdError loadAdError) =>
            {
                if (loadAdError != null || ad == null)
                {
                    Debug.LogError("interstitial ad failed to load an ad " +
                                   "with error : " + loadAdError);
                    return;
                }
                Debug.Log("Interstitial ad loaded with response : "  + ad.GetResponseInfo());
                _interstitialAd = ad;
                ListenToInterstitialAdEvents();
            });
    }
    public void ShowInterstitialAd()
    {
        if (_interstitialAd != null && _interstitialAd.CanShowAd())
        {
            _interstitialAd.Show();
        }
        else
        {
            InterstitialCallBack?.Invoke();
        }
    }
    private void ListenToInterstitialAdEvents()
    {
        _interstitialAd.OnAdPaid += (AdValue adValue) => { };
        _interstitialAd.OnAdImpressionRecorded += () => { };
        _interstitialAd.OnAdClicked += () => { };
        _interstitialAd.OnAdFullScreenContentOpened += () => { };
        _interstitialAd.OnAdFullScreenContentClosed += () => { InterstitialCallBack?.Invoke();  LoadInterstitialAd(); };
        _interstitialAd.OnAdFullScreenContentFailed += (AdError error) => { InterstitialCallBack?.Invoke(); LoadInterstitialAd();  };
    }
    #endregion

    #region Rewarded
    private void LoadRewardedAd()
    {
        if (_rewardedAd != null)
        {
            _rewardedAd.Destroy();
            _rewardedAd = null;
        }
        var adRequest = new AdRequest();
        RewardedAd.Load(IsTest? _TestRewardedAdID : _RewardedAdID, adRequest,
            (RewardedAd ad, LoadAdError loadError) =>
            {
                if (loadError != null || ad == null)
                {
                    Debug.LogError("Rewarded ad failed to load an ad " +
                                   "with error : " + loadError);
                    return;
                }

                Debug.Log("Rewarded ad loaded with response : "
                          + ad.GetResponseInfo());

                _rewardedAd = ad;
                ListenToRewardedAdEvents();
            });
    }
    public void ShowRewardedAd()
    {
        if (_rewardedAd != null && _rewardedAd.CanShowAd())
        {
            _rewardedAd.Show((Reward reward) =>
            {
                RewardedCallBack?.Invoke();
            });
        }
        else if (repeatCount < 3)
        {
            LoadRewardedAd();
            Invoke(nameof(ShowRewardedAd), 2f);
            repeatCount++;
        }
        else
        {
            repeatCount = 0;
        }
    }
    private void ListenToRewardedAdEvents()
    {
        _rewardedAd.OnAdPaid += (AdValue adValue) => { };
        _rewardedAd.OnAdImpressionRecorded += () => { };
        _rewardedAd.OnAdClicked += () => { };
        _rewardedAd.OnAdFullScreenContentOpened += () => { };
        _rewardedAd.OnAdFullScreenContentClosed += () => { LoadRewardedAd(); };
        _rewardedAd.OnAdFullScreenContentFailed += (AdError error) => { LoadRewardedAd(); };
    }
    public bool IsRewardedReady { get { return _rewardedAd.CanShowAd(); } }
    #endregion

    #region banner
    public void LoadBannerAd()
    {
        DestroyBannerAd();
        if (_bannerAd == null) CreateBannerView();

        var adRequest = new AdRequest();
        _bannerAd.LoadAd(adRequest);
    }
    public void CreateBannerView()
    {
        _bannerAd = new BannerView(IsTest ? _TestBannerAdID : _BannerAdID, AdSize.Banner, AdPosition.Bottom);
        ListenToBannerAdEvents();
    }
    public void DestroyAndLoad()
    {
        DestroyBannerAd();
        LoadBannerAd();
    }
    private void ListenToBannerAdEvents()
    {
        _bannerAd.OnBannerAdLoadFailed += (LoadAdError error) =>
        {
            Invoke(nameof(DestroyAndLoad), 2f);
        };
        _bannerAd.OnBannerAdLoaded += () => { };
        _bannerAd.OnAdPaid += (AdValue adValue) => { };
        _bannerAd.OnAdImpressionRecorded += () => { };
        _bannerAd.OnAdClicked += () => { };
        _bannerAd.OnAdFullScreenContentOpened += () => { };
        _bannerAd.OnAdFullScreenContentClosed += () => { LoadBannerAd(); };
    }
    public void DestroyBannerAd()
    {
        if (_bannerAd != null)
        {
            _bannerAd.Destroy();
            _bannerAd = null;
        }
    }
    #endregion
}
