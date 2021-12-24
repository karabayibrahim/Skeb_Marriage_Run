using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor;
public class Male : Human
{
    private GameObject newParticle;
    private bool _adultControl = false;
    private bool _oldControl = false;


    public HumanState TempHumanState;
    public GameObject Ring;
    public bool IsTurn = false;

    void Start()
    {
        base._anim = GetComponent<Animator>();
        GameManager.Instance.Player.Male = this;
        Ring.SetActive(false);

    }
    private void OnEnable()
    {
        PlayerController.WalkAction += WalkState;
        PlayerController.IdleAction += IdleState;
        GameManager.AgeChanged += PositionControl;
        GameManager.FinishEvent += FinishState;
    }

    private void OnDisable()
    {
        PlayerController.WalkAction -= WalkState;
        PlayerController.IdleAction -= IdleState;
        GameManager.AgeChanged -= PositionControl;
        GameManager.FinishEvent -= FinishState;
    }

    // Update is called once per frame
    void Update()
    {
        _anim.SetFloat("Blend", GameManager.Instance.Player.AgeCalculator());
        if (GameManager.Instance.Player.AgeCalculator() >= 0.3f && !_adultControl)
        {
            GameManager.AgeChanged?.Invoke();
            _adultControl = true;
        }
        else if (GameManager.Instance.Player.AgeCalculator() >= 0.6f && !_oldControl)
        {
            GameManager.AgeChanged?.Invoke();
            _oldControl = true;
        }
    }
    public override void WalkState()
    {
        Ring.SetActive(false);
        Destroy(newParticle);
        var Player = GameManager.Instance.Player;
        if (!IsTurn)
        {
            if (Player.RelationStatus == RelationStatus.EXCELLENT)
            {
                if (Player.WalkRandomIndex == 0)
                {
                    HumanState = HumanState.CARRY;
                    AnimationPosition(HumanState.CARRY);
                }
                else if (Player.WalkRandomIndex == 1)
                {
                    HumanState = HumanState.SHOULDER;
                    AnimationPosition(HumanState.SHOULDER);
                }

            }
            else if (Player.RelationStatus == RelationStatus.TERRIBLE)
            {
                HumanState = HumanState.SADWALK;
                AnimationPosition(HumanState.SADWALK);
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

    }

    private void FinishState()
    {
        var Player = GameManager.Instance.Player;
        switch (Player.RelationStatus)
        {
            case RelationStatus.TERRIBLE:
                HumanState = HumanState.INSULT;
                AnimationPosition(HumanState.INSULT);
                break;
            case RelationStatus.BAD:
                HumanState = HumanState.CRY;
                AnimationPosition(HumanState.CRY);
                break;
            case RelationStatus.NORMAL:
                HumanState = HumanState.TALK;
                AnimationPosition(HumanState.TALK);
                break;
            case RelationStatus.GOOD:
                HumanState = HumanState.BLOWKISS;
                AnimationPosition(HumanState.BLOWKISS);
                break;
            case RelationStatus.EXCELLENT:
                HumanState = HumanState.DANCE;
                AnimationPosition(HumanState.DANCE);
                break;
            default:
                break;
        }
    }


    public override void IdleState()
    {
        var Player = GameManager.Instance.Player;
        switch (Player.RelationStatus)
        {
            case RelationStatus.TERRIBLE:
                HumanState = HumanState.SADIDLE;
                AnimationPosition(HumanState.SADIDLE);
                Ring.SetActive(false);
                break;
            case RelationStatus.BAD:
                HumanState = HumanState.ARGUING;
                AnimationPosition(HumanState.ARGUING);
                Ring.SetActive(false);
                break;
            case RelationStatus.NORMAL:
                HumanState = HumanState.TALK;
                AnimationPosition(HumanState.TALK);
                Ring.SetActive(false);
                break;
            case RelationStatus.GOOD:
                HumanState = HumanState.KISS;
                AnimationPosition(HumanState.KISS);
                Ring.SetActive(false);
                break;
            case RelationStatus.EXCELLENT:
                HumanState = HumanState.RING;
                AnimationPosition(HumanState.RING);
                Ring.SetActive(true);
                //newParticle = Instantiate(GameManager.Instance.Data.Particles[4], transform.position, Quaternion.identity, transform);
                //newParticle.transform.localPosition = new Vector3(0, 12f, -2.5f);
                break;
            default:
                break;
        }
    }

    public void AnimationPosition(HumanState humanState)
    {
        switch (humanState)
        {
            case HumanState.IDLE:
                transform.DORotate(new Vector3(0, 0, 0), 0.5f);
                break;
            case HumanState.WALK:
                transform.DORotate(new Vector3(0, 0f, 0), 0.5f);
                break;
            case HumanState.KISS:
                transform.DORotate(new Vector3(0, -90f, 0), 0.5f);
                newParticle = Instantiate(GameManager.Instance.Data.Particles[2], transform.position, Quaternion.identity, transform);
                newParticle.transform.localPosition = new Vector3(0, 12.5f, -1.2f);
                break;
            case HumanState.CARRY:
                transform.DORotate(new Vector3(0, 0, 0), 0.5f);
                break;
            case HumanState.SIT:
                break;
            case HumanState.CARRYIDLE:
                break;
            case HumanState.ARGUING:
                transform.DORotate(new Vector3(0, -90f, 0), 0.5f);
                newParticle = Instantiate(GameManager.Instance.Data.Particles[3], transform.position, Quaternion.identity, transform);
                newParticle.transform.localPosition = new Vector3(0, 12.5f, 0);
                break;
            case HumanState.SADWALK:
                break;
            case HumanState.SADIDLE:
                transform.DORotate(new Vector3(0, 0, 0), 0.5f);
                newParticle = Instantiate(GameManager.Instance.Data.Particles[5], transform.position, Quaternion.identity, transform);
                newParticle.transform.localPosition = new Vector3(0, 13f, -1.2f);
                break;
            case HumanState.TALK:
                transform.DORotate(new Vector3(0, -90f, 0), 0.5f);
                newParticle = Instantiate(GameManager.Instance.Data.Particles[8], transform.position, Quaternion.identity, transform);
                newParticle.transform.localPosition = new Vector3(0, 13f, -1.2f);
                newParticle.GetComponent<ParticleSystemRenderer>().flip = new Vector3(1, 0, 0);
                break;
            case HumanState.SHOULDER:
                transform.DORotate(new Vector3(0, 0, 0), 0.5f);
                break;
            case HumanState.CLOSEARM:
                transform.DORotate(new Vector3(0, 0, 0), 0.5f);
                break;
            case HumanState.RING:
                transform.DORotate(new Vector3(0, -90f, 0), 0.5f);
                newParticle = Instantiate(GameManager.Instance.Data.Particles[10], transform.position, Quaternion.identity, transform);
                newParticle.transform.localPosition = new Vector3(-4, 11.5f, -1.5f);
                break;
            case HumanState.HANDWALK:
                transform.DORotate(new Vector3(0, 0, 0), 0.5f);
                break;
            case HumanState.CRY:
                transform.DORotate(new Vector3(0, -90f, 0), 0.5f);
                transform.DOLocalMove(new Vector3(2f, 0f, -0.8f), 0.5f);
                break;
            case HumanState.DANCE:
                transform.DORotate(new Vector3(0, -90f, 0), 0.5f);
                transform.DOLocalMove(new Vector3(2f, 0f, -0.8f), 0.5f);
                break;
            case HumanState.BLOWKISS:
                transform.DORotate(new Vector3(0, -90f, 0), 0.5f);
                transform.DOLocalMove(new Vector3(4f, 0f, -0.8f), 0.5f);
                break;
            case HumanState.INSULT:
                transform.DORotate(new Vector3(0, -90f, 0), 0.5f);
                transform.DOLocalMove(new Vector3(3f, 0f, -0.8f), 0.5f);
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
                transform.DOLocalMove(new Vector3(3, 0, 0), 0.5f);
                break;
            case RelationStatus.BAD:
                transform.DOLocalMove(new Vector3(4, 0, 0), 0.5f);
                break;
            case RelationStatus.NORMAL:
                transform.DOLocalMove(new Vector3(2, 0, 0), 0.5f);

                break;
            case RelationStatus.GOOD:
                transform.DOLocalMove(new Vector3(1.4f, 0f, 0), 0.5f);
                break;
            case RelationStatus.EXCELLENT:
                transform.DOLocalMove(new Vector3(0, 0, 0), 0.5f);
                break;
            default:
                break;
        }
    }

    public void TrigAnimationTime(float time)
    {
        TempHumanState = HumanState;
        StartCoroutine(TrigTime(time));
    }

    private IEnumerator TrigTime(float _time)
    {
        yield return new WaitForSeconds(_time);
        HumanState = TempHumanState;
        IsTurn = false;
    }



}
