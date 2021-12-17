using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;
public class GameManager :MonoSingleton<GameManager>
{
    public PlayerController Player;
    public DataSystem Data;
    public LevelData LevelData;
    public Level CurrentLevel;
    public CinemachineVirtualCamera Cam;
    public UIManager UIManager;
    public int GameStateIndex;
    public static Action GameStateChanged;

    void Start()
    {
        CamAssigment();
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
