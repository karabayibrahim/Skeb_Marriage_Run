using System.Collections;
using System.Collections.Generic;
//using Facebook.Unity;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sceeneloadscript : MonoBehaviour
{
    int currentSceeneIndex;
    void Start()
    {
        //if (!FB.IsInitialized)
        //    FB.Init(OnInitComplete);
        //else
        //    FB.ActivateApp();

        //GameAnalyticsSDK.GameAnalytics.Initialize();

        if (PlayerPrefs.HasKey("Level"))
        {
            if (PlayerPrefs.GetInt("Level") > 3)
            {
                SceneManager.LoadScene("Level " + Random.Range(1, 3));
            }
            else
            {

                SceneManager.LoadScene("Level " + PlayerPrefs.GetInt("Level"));
            }
        }
        else
        {
            PlayerPrefs.SetInt("Level", 1);
            SceneManager.LoadScene("Level " + PlayerPrefs.GetInt("Level"));
        }
    }

    //void OnInitComplete()
    //{
    //    if (FB.IsInitialized)
    //    {
    //        FB.ActivateApp();
    //        FB.LogAppEvent(AppEventName.ActivatedApp);
    //    }
    //    else
    //        Debug.Log("OnInitComplete Failed");
    //}
}

//using ElephantSDK;

//Elephant.LevelStarted(PlayerPrefs.GetInt("LevelIndex"));
//Elephant.LevelFailed(PlayerPrefs.GetInt("LevelIndex"));
//Elephant.LevelCompleted(PlayerPrefs.GetInt("LevelIndex"));
