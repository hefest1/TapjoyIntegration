using System;
using System.Threading.Tasks;
using TapjoyUnity;
using UnityEngine;
using UnityEngine.UI;

public class TapjoyManager : MonoBehaviour, IDisposable
{
    private const string kSDKKey = "DYLTODtbRC-fuGYxXPRbdAECZkSnm1Y2wRAqbzO5bYVXjlnLpibNuLoTuyMT";
    //private const string kSDKKey = "Ooj898beQjmDrTmOpvGoPgEC17SNIH3LCWmt0ZnaO5MehJUW_QHIsihNSchp";
    private const string kPlacementNameRewardedAd = "NormalRewardedAd";
    //private const string kPlacementNameRewardedAd = "bank_exit";
    private const string kPlacementNameInterstitialAd = "NormalInterstitialAd";

    private TJPlacement _placementRewardedAd;
    private TJPlacement _placementInterstitialAd;

    //test
    [SerializeField] private Button _btnRewarded;
    [SerializeField] private Button _btnInterstitial;

    private async void Initialize()
    {
        

        Tapjoy.OnConnectSuccess += HandleConnectSuccess;
        Tapjoy.OnConnectFailure += HandleConnectFailure;

        //test
        await Task.Delay(1000);

        Tapjoy.Connect(kSDKKey);

        //test
        _btnRewarded.onClick.AddListener(TryToShowRewardedAd);
        _btnInterstitial.onClick.AddListener(TryToShowInterstitialAd);
    }

    public void Dispose()
    {
        Tapjoy.OnConnectSuccess -= HandleConnectSuccess;
        Tapjoy.OnConnectFailure -= HandleConnectFailure;
    }

    private void Awake()
    {
        Initialize();
    }

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
        Dispose();
    }

    public void HandleConnectSuccess()
    {
        TJPlacement.OnRequestSuccess += HandlePlacementRequestSuccess;
        TJPlacement.OnRequestFailure += HandlePlacementRequestFailure;
        TJPlacement.OnContentReady += HandlePlacementContentReady;
        TJPlacement.OnContentShow += HandlePlacementContentShow;
        TJPlacement.OnContentDismiss += HandlePlacementContentDismiss;

            // test
        Debug.Log($"Tapjoy successfully connected. Tapjoy.IsConnected: {Tapjoy.IsConnected}");

        _placementRewardedAd = TJPlacement.CreatePlacement(kPlacementNameRewardedAd);
        _placementInterstitialAd = TJPlacement.CreatePlacement(kPlacementNameInterstitialAd);

        RequestContent();
    }

    public void HandleConnectFailure()
    {
        //test
        Debug.Log("Tapjoy SDK connection failed.");
    }

    private void RequestContent()
    {
        if (Tapjoy.IsConnected)
        {
            _placementRewardedAd.RequestContent();
            _placementInterstitialAd.RequestContent();

            //test
            Debug.LogError($"RequestContent");
        }
        else
        {
            //test
            Debug.LogWarning("Tapjoy not connected");
        }
    }

    private void TryToShowRewardedAd()
    {
        if (_placementRewardedAd.IsContentReady())
        {
            _placementRewardedAd.ShowContent();
            //_placementRewardedAd.RequestContent();
        }
        else
        {
            //test
            Debug.Log($"Rewarded ad not ready to show");
        }
    }

    private void TryToShowInterstitialAd()
    {
        if (_placementInterstitialAd.IsContentReady())
        {
            _placementInterstitialAd.ShowContent();
            //_placementInterstitialAd.RequestContent();
        }
        else
        {
            //test
            Debug.Log($"Interstitial ad not ready to show");
        }
    }

    public void HandlePlacementRequestSuccess(TJPlacement placement)
    {
        Debug.LogError($"placement request success {placement.GetName()}");
    }

    public void HandlePlacementRequestFailure(TJPlacement placement, string error)
    {
        Debug.LogError($"placement request failure {placement.GetName()}");
    }
    public void HandlePlacementContentReady(TJPlacement placement)
    {
        Debug.LogError($"placement content ready {placement.GetName()}");
    }

    public void HandlePlacementContentShow(TJPlacement placement)
    {
        Debug.LogError($"placement content show {placement.GetName()}");
    }

    public void HandlePlacementContentDismiss(TJPlacement placement)
    {
        Debug.LogError($"placement content dismiss {placement.GetName()}");
    }
}
