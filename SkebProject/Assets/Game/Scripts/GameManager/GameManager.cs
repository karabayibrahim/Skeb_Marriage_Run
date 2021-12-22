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
    public TextData TextData;
    public static bool StartStatus = false;
    public bool AgeControl = false;
    public ConvetModelSample MaleConvert;
    public ConvetModelSample FemaleConvert;
    public TextMesh AgeCount;
    void Start()
    {
        Application.targetFrameRate = 60;
        CamAssigment();
        AgeChanged += MaleConvert.InProgress;
        AgeChanged += FemaleConvert.InProgress;
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
}


