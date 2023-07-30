using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class loadingtext : MonoBehaviour {

    public float fillSpeed = 0.5f;
    public float minimumLoadingTime = 2f;
    public TextMeshProUGUI progressText;
    public TextMeshProUGUI statusText;
    public Image loadingBarImage;

    private bool isLoading = false;
    private AsyncOperation asyncOperation;
    private float startTime;

    private void Start()
    {
        loadingBarImage.fillAmount = 0.0f;
        progressText.text = "0%";
        statusText.text = "Loading...";
        StartCoroutine(LoadSceneAsync());
    }

    private IEnumerator LoadSceneAsync()
    {
        asyncOperation = SceneManager.LoadSceneAsync(1);

        asyncOperation.allowSceneActivation = false;

        isLoading = true;
        startTime = Time.time;

        while (isLoading)
        {
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);

            loadingBarImage.fillAmount = Mathf.MoveTowards(loadingBarImage.fillAmount, progress, Time.deltaTime * fillSpeed);

            int percent = Mathf.RoundToInt(loadingBarImage.fillAmount * 100);
            progressText.text = percent + "%";

           
            if (percent <= 33)
            {
                statusText.text = "Loading...";
            }
            else if (percent <= 67)
            {
                statusText.text = "Downloading...";
            }
            else if (percent == 100)
            {
                statusText.text = "Please wait...";
            }

            if (loadingBarImage.fillAmount >= 0.9999f && Time.time - startTime >= minimumLoadingTime)
            {
                isLoading = false;
                asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
