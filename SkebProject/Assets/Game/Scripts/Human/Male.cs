using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Male : Human
{
    

    void Start()
    {
        base._anim = GetComponent<Animator>();
        PlayerController.WalkAction += WalkState;
        PlayerController.IdleAction += IdleState;
    }

    private void OnDisable()
    {
        PlayerController.WalkAction -= WalkState;
        PlayerController.IdleAction -= IdleState;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void WalkState()
    {
        var Player = GameManager.Instance.Player;
        if (Player.RelationStatus==RelationStatus.EXCELLENT)
        {
            HumanState = HumanState.CARRY;
        }
        else
        {
            transform.DORotate(new Vector3(0, 0f, 0), 0.5f);
            HumanState = HumanState.WALK;
        }
        
    }

    private void IdleState()
    {
        var Player = GameManager.Instance.Player;
        switch (Player.RelationStatus)
        {
            case RelationStatus.TERRIBLE:
                break;
            case RelationStatus.BAD:
                break;
            case RelationStatus.NORMAL:
                HumanState = HumanState.IDLE;
                break;
            case RelationStatus.GOOD:
                HumanState = HumanState.KISS;
                transform.DORotate(new Vector3(0, -90f, 0), 0.5f);
                break;
            case RelationStatus.EXCELLENT:
                HumanState = HumanState.CARRYIDLE;
                break;
            default:
                break;
        }
    }
    

}
