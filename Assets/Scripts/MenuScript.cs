using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void PlayButton()
    {
        AdsManager.Instance.InterstitialCallBack = null;
        AdsManager.Instance.InterstitialCallBack = LoadScene;
        AdsManager.Instance.InterstitialAD();
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitButton()
    {
        Application.Quit();

    }
}
