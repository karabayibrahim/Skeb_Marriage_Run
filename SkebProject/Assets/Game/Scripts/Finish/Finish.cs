using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour,ICollectable
{
    [System.Obsolete]
    public void DoCollect()
    {
        gameObject.GetComponent<Collider>().enabled = false;
        GameManager.Instance.LevelIndex++;
        Application.LoadLevel(Application.loadedLevel);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
