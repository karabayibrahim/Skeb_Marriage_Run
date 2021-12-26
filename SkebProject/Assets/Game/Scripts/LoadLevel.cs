using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadLevel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("Level")!=0)
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("Level"), LoadSceneMode.Single);
        }
        //Destroy(gameObject);
    }


}
