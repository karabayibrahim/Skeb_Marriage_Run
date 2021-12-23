using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
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
            if (HumanState == value)
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
            case HumanState.TALK:
                TrigAnimation("Talk");
                break;
            case HumanState.SHOULDER:
                TrigAnimation("Shoulder");
                break;
            case HumanState.CLOSEARM:
                TrigAnimation("CloseArm");
                break;
            case HumanState.RING:
                TrigAnimation("Ring");
                break;
            case HumanState.HANDWALK:
                TrigAnimation("HandWalk");
                break;
            case HumanState.TURN:
                TrigAnimation("Turn");
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
    public void TrigAnimation(string animName)
    {
        _anim.CrossFade(animName, 0.05f);
    }

    public void PositionChange(Vector3 _newposition)
    {
        transform.DOLocalMove(_newposition, 0.5f).SetEase(Ease.OutCubic);
    }

    public abstract void WalkState();
    public abstract void IdleState();





}
