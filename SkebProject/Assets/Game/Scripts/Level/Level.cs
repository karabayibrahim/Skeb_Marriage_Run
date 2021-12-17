using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    private int _levelIndex;
    public Finish Finish;
    public int LevelIndex
    {
        get
        {
            return _levelIndex;
        }
        set
        {
            if (LevelIndex==value)
            {
                return;
            }
            _levelIndex = value;
            OnLevelIndexChanged();

        }
    }

    public void OnLevelIndexChanged()
    {
        
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
