using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using System;

public class GecisliReklam : MonoBehaviour
{
#if UNITY_ANDROID
    public string _adUnitId = "ca-app-pub-3940256099942544/1033173712";
#elif UNITY_IPHONE
    private string _adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
    private string _adUnitId = "unused";
#endif

    private InterstitialAd interstitialAd;

    public void Start()
    {

        MobileAds.Initialize((InitializationStatus initStatus) =>
        {

            LoadInterstitialAd();
        });
    }

    public void LoadInterstitialAd()
    {

        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
            interstitialAd = null;
        }

        Debug.Log("Loading the interstitial ad.");


        var adRequest = new AdRequest();
        adRequest.Keywords.Add("unity-admob-sample");


        InterstitialAd.Load(_adUnitId, adRequest,
            (InterstitialAd ad, LoadAdError error) =>
            {

                if (error != null || ad == null)
                {
                    Debug.LogError("interstitial ad failed to load an ad " +
                                   "with error: " + error);
                    return;
                }

                Debug.Log("Interstitial ad loaded with response: "
                          + ad.GetResponseInfo());

                interstitialAd = ad;
                RegisterEventHandlers(interstitialAd);
            });
    }

    public void ShowAd()
    {
        if (interstitialAd != null && interstitialAd.CanShowAd())
        {
            Debug.Log("Showing interstitial ad.");
            interstitialAd.Show();
        }
        else
        {
            Debug.LogError("Interstitial ad is not ready yet.");
            GameObject.FindWithTag("Player").GetComponent<PlayerController>().ReloadScene();

        }
    }

    private void RegisterEventHandlers(InterstitialAd ad)
    {

        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(string.Format("Interstitial ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };


        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Interstitial ad recorded an impression.");
        };


        ad.OnAdClicked += () =>
        {
            Debug.Log("Interstitial ad was clicked.");
        };


        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Interstitial ad full-screen content opened.");
        };


        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Interstitial ad full-screen content closed.");
            GameObject.FindWithTag("Player").GetComponent<PlayerController>().ReloadScene();

            LoadInterstitialAd();
        };


        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Interstitial ad failed to open full-screen content " +
                           "with error: " + error);
            GameObject.FindWithTag("Player").GetComponent<PlayerController>().ReloadScene();

            LoadInterstitialAd();
        };
    }
}
