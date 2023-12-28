using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void Restart()
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
