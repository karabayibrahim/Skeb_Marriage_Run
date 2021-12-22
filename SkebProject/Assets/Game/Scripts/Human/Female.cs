using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Female : Human
{

    private GameObject newParticle;
    // Start is called before the first frame update
    void Start()
    {
        base._anim = GetComponent<Animator>();
        GameManager.Instance.Player.Female = this;
    }

    private void OnEnable()
    {
        PlayerController.WalkAction += WalkState;
        PlayerController.IdleAction += IdleState;
        GameManager.AgeChanged += PositionControl;
    }

    private void OnDisable()
    {
        PlayerController.WalkAction -= WalkState;
        PlayerController.IdleAction -= IdleState;
        GameManager.AgeChanged -= PositionControl;
    }

    // Update is called once per frame
    void Update()
    {
        _anim.SetFloat("Blend", GameManager.Instance.Player.AgeCalculator());
        var age = (int)(GameManager.Instance.Player.AgeCalculator()*70)+18;
        GameManager.Instance.AgeCount.text = "Age:" + age;
    }

    public override void WalkState()
    {
        Destroy(newParticle);
        var Player = GameManager.Instance.Player;
        if (Player.RelationStatus == RelationStatus.EXCELLENT)
        {
            if (Player.WalkRandomIndex == 0)
            {
                HumanState = HumanState.SIT;
                AnimationPosition(HumanState.SIT);
            }
            else if (Player.WalkRandomIndex == 1)
            {
                HumanState = HumanState.SHOULDER;
                AnimationPosition(HumanState.SHOULDER);
            }

        }
        else if (Player.RelationStatus == RelationStatus.TERRIBLE)
        {
            HumanState = HumanState.SIT;
            AnimationPosition(HumanState.SIT);
        }
        else if (Player.RelationStatus == RelationStatus.BAD)
        {
            HumanState = HumanState.CLOSEARM;
            AnimationPosition(HumanState.CLOSEARM);
        }
        else if (Player.RelationStatus == RelationStatus.GOOD)
        {
            HumanState = HumanState.HANDWALK;
            AnimationPosition(HumanState.HANDWALK);
        }
        else
        {
            HumanState = HumanState.WALK;
            AnimationPosition(HumanState.WALK);
        }
    }

    public override void IdleState()
    {
        var Player = GameManager.Instance.Player;
        switch (Player.RelationStatus)
        {
            case RelationStatus.TERRIBLE:
                HumanState = HumanState.KISS;
                AnimationPosition(HumanState.KISS);
                break;
            case RelationStatus.BAD:
                HumanState = HumanState.ARGUING;
                AnimationPosition(HumanState.ARGUING);
                break;
            case RelationStatus.NORMAL:
                HumanState = HumanState.TALK;
                AnimationPosition(HumanState.TALK);
                break;
            case RelationStatus.GOOD:
                HumanState = HumanState.KISS;
                AnimationPosition(HumanState.KISS);
                break;
            case RelationStatus.EXCELLENT:
                HumanState = HumanState.RING;
                AnimationPosition(HumanState.RING);
                //HumanState = HumanState.CARRYIDLE;
                //AnimationPosition(HumanState.KISS);
                //newParticle = Instantiate(GameManager.Instance.Data.Particles[4], transform.position, Quaternion.identity, transform);
                //newParticle.transform.localPosition = new Vector3(0, 10f, -2.5f);
                break;
            default:
                break;
        }
    }

    private void AnimationPosition(HumanState humanState)
    {
        var Player = GameManager.Instance.Player;
        switch (humanState)
        {
            case HumanState.IDLE:
                transform.DORotate(new Vector3(0, 0f, 0), 0.5f);
                break;
            case HumanState.WALK:
                transform.DORotate(new Vector3(0, 0f, 0), 0.5f);
                break;
            case HumanState.KISS:
                if (Player.RelationStatus==RelationStatus.GOOD)
                {
                    transform.DORotate(new Vector3(0, 90f, 0), 0.5f);
                    transform.DOLocalMove(new Vector3(-1f, 0f, -0.8f), 0.5f);
                    newParticle = Instantiate(GameManager.Instance.Data.Particles[2], transform.position, Quaternion.identity, transform);
                    newParticle.transform.localPosition = new Vector3(0, 12.5f, -1.2f);
                }
                else if (Player.RelationStatus==RelationStatus.TERRIBLE)
                {
                    transform.DORotate(new Vector3(0, 90f, 0), 0.5f);
                    transform.DOLocalMove(new Vector3(-5.5f,0,-0.8f), 0.5f);
                }

                break;
            case HumanState.CARRY:
                break;
            case HumanState.SIT:
                transform.DORotate(new Vector3(0, -90f - 0), 0.5f);
                if (Player.RelationStatus==RelationStatus.EXCELLENT)
                {
                    transform.DOLocalMove(new Vector3(0f, 3, 1.3f), 0.5f);
                }
                else if (Player.RelationStatus==RelationStatus.TERRIBLE)
                {
                    transform.DOLocalMove(new Vector3(-3f, 3, 1.3f), 0.5f);
                }
                break;
            case HumanState.CARRYIDLE:
                break;
            case HumanState.ARGUING:
                transform.DORotate(new Vector3(0, 90f, 0), 0.5f);
                newParticle = Instantiate(GameManager.Instance.Data.Particles[3], transform.position, Quaternion.identity, transform);
                newParticle.transform.localPosition = new Vector3(0, 12.5f, 0);
                break;
            case HumanState.SADWALK:
                break;
            case HumanState.SADIDLE:
                break;
            case HumanState.TALK:
                transform.DORotate(new Vector3(0, 90f, 0), 0.5f);
                newParticle = Instantiate(GameManager.Instance.Data.Particles[8], transform.position, Quaternion.identity, transform);
                newParticle.transform.localPosition = new Vector3(0, 12.5f, -1.2f);
                break;
            case HumanState.SHOULDER:
                transform.DORotate(new Vector3(0, 0, 0), 0.5f);
                transform.DOLocalMove(new Vector3(-0.25f, 0.25f, -0.55f), 0.5f);
                break;
            case HumanState.CLOSEARM:
                transform.DORotate(new Vector3(0, 0, 0), 0.5f);
                break;
            case HumanState.RING:
                transform.DORotate(new Vector3(0, 90f, 0), 0.5f);
                transform.DOLocalMove(new Vector3(-4f, 0, 0), 0.5f);
                break;
            case HumanState.HANDWALK:
                transform.DORotate(new Vector3(0, 0, 0), 0.5f);
                transform.DOLocalMove(new Vector3(-1.7f, 0f, 0), 0.5f);
                break;
            default:
                break;
        }
    }

    private void PositionControl()
    {
        switch (GameManager.Instance.Player.RelationStatus)
        {
            case RelationStatus.TERRIBLE:
                transform.DOLocalMove(new Vector3(-3f, 3, 1.3f), 0.5f);
                break;
            case RelationStatus.BAD:
                transform.DOLocalMove(new Vector3(-4f, 0, 0f), 0.5f);
                break;
            case RelationStatus.NORMAL:
                transform.DOLocalMove(new Vector3(-2, 0, 0), 0.5f);

                break;
            case RelationStatus.GOOD:
                transform.DOLocalMove(new Vector3(-1.5f, 0f, 0), 0.5f);
                break;
            case RelationStatus.EXCELLENT:
                transform.DOLocalMove(new Vector3(0f, 3, 1.3f), 0.5f);
                break;
            default:
                break;
        }
    }

}
