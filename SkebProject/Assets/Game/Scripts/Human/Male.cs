using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Male : Human
{
    private GameObject newParticle;
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
        Destroy(newParticle);
        var Player = GameManager.Instance.Player;
        if (Player.RelationStatus == RelationStatus.EXCELLENT)
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
                HumanState = HumanState.ARGUING;
                transform.DORotate(new Vector3(0, -90f, 0), 0.5f);
                newParticle = Instantiate(GameManager.Instance.Data.Particles[3], transform.position, Quaternion.identity,transform);
                newParticle.transform.localPosition = new Vector3(0, 12.5f, 0);
                break;
            case RelationStatus.NORMAL:
                HumanState = HumanState.IDLE;
                break;
            case RelationStatus.GOOD:
                HumanState = HumanState.KISS;
                transform.DORotate(new Vector3(0, -90f, 0), 0.5f);
                newParticle = Instantiate(GameManager.Instance.Data.Particles[2], transform.position, Quaternion.identity,transform);
                newParticle.transform.localPosition = new Vector3(0, 12.5f, -1.2f);
                break;
            case RelationStatus.EXCELLENT:
                HumanState = HumanState.CARRYIDLE;
                //newParticle = Instantiate(GameManager.Instance.Data.Particles[4], transform.position, Quaternion.identity, transform);
                //newParticle.transform.localPosition = new Vector3(0, 12f, -2.5f);
                break;
            default:
                break;
        }
    }


}
