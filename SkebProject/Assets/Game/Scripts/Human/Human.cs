using System;
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
                TrigAnimation("Walk");
                break;
            case HumanState.KISS:
                TrigAnimation("Kiss");
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

   




}
