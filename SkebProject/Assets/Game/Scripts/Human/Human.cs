﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Human : MonoBehaviour
{
    private HumanState _humanState;

    public Animator _anim;


    public HumanState HumanState
    {
        get
        {
            return _humanState;
        }
        set
        {
            if (HumanState==value)
            {
                return;
            }
            _humanState = value;
            OnHumanStateChanged();
        }
    }

    private void OnHumanStateChanged()
    {
        switch (HumanState)
        {
            case HumanState.IDLE:
                TrigAnimation("Idle");
                break;
            case HumanState.WALK:
                TrigAnimation("BlendWalk");
                break;
            case HumanState.KISS:
                TrigAnimation("Kiss");
                break;
            case HumanState.CARRY:
                TrigAnimation("Carry");
                break;
            case HumanState.SIT:
                TrigAnimation("Sit");
                break;
            case HumanState.CARRYIDLE:
                TrigAnimation("CarryIdle");
                break;
            case HumanState.ARGUING:
                TrigAnimation("Arguing");
                break;
            case HumanState.SADWALK:
                TrigAnimation("SadWalk");
                break;
            case HumanState.SADIDLE:
                TrigAnimation("SadIdle");
                break;
            default:
                break;
        }
    }

    void Start()
    {
        
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
    public  void TrigAnimation(string animName)
    {
        _anim.CrossFade(animName, 0.05f);
    }

    public abstract void WalkState();
    public abstract void IdleState();
   




}
