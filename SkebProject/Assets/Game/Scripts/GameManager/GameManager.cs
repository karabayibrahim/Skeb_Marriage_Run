using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;
using BzKovSoft.SmoothMeshConverter;
using BzKovSoft.SmoothMeshConverter.Samples;
public class GameManager : MonoSingleton<GameManager>
{
    public PlayerController Player;
    public DataSystem Data;
    public LevelData LevelData;
    public Level CurrentLevel;
    public CinemachineVirtualCamera Cam;
    public UIManager UIManager;
    public int GameStateIndex;
    public static Action GameStateChanged;
    public static Action AgeChanged;
    public static Action FinishEvent;
    public static Action<GameObject> ParticleDestroyEvent;
    public TextData TextData;
    public static bool StartStatus = false;
    public bool AgeControl = false;
    public ConvetModelSample MaleConvert;
    public ConvetModelSample FemaleConvert;
    public TextMesh AgeCount;
    public List<Male> Males = new List<Male>();
    public List<Female> Females = new List<Female>();


    private int _levelIndex;


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

   

    void Start()
    {
        LevelSpawn();
        Application.targetFrameRate = 60;
        CamAssigment();
        AgeChanged += MaleConvert.InProgress;
        AgeChanged += FemaleConvert.InProgress;
        HumansAdjust();
    }

    private void OnDisable()
    {
        AgeChanged -= MaleConvert.InProgress;
        AgeChanged -= FemaleConvert.InProgress;
    }
    // Update is called once per frame
    void Update()
    {
       
    }

    private void CamAssigment()
    {
        Cam.Follow = Player.transform;
        Cam.LookAt = Player.transform;
    }

    private void OnLevelIndexChanged()
    {
        if (CurrentLevel!=null)
        {
            Destroy(CurrentLevel.gameObject);
        }
        LevelSpawn();
    }

    private void LevelSpawn()
    {
        var currentLevel=Instantiate(LevelData.levels[LevelIndex], transform.position, Quaternion.identity);
        CurrentLevel = currentLevel;
    }

    public void HumansAdjust()
    {
        for (int i = 0; i < Females.Count; i++)
        {
            FemaleConvert._convertStack[i] = Females[i].gameObject;
        }
        for (int i = 0; i < Males.Count; i++)
        {
            MaleConvert._convertStack[i] = Males[i].gameObject;
        }
    }

}


