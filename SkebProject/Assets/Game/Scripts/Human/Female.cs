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
        
    }

    private void OnEnable()
    {
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

    public override void WalkState()
    {
        Destroy(newParticle);
        var Player = GameManager.Instance.Player;
        if (Player.RelationStatus == RelationStatus.EXCELLENT)
        {
            HumanState = HumanState.SIT;
            transform.DORotate(new Vector3(0, -90f - 0), 0.5f);
        }
        else if (Player.RelationStatus==RelationStatus.TERRIBLE)
        {
            HumanState = HumanState.SIT;
            transform.DORotate(new Vector3(0, -90f - 0), 0.5f);
        }
        else
        {
            transform.DORotate(new Vector3(0, 0f, 0), 0.5f);
            HumanState = HumanState.WALK;
        }
    }

    public override void IdleState()
    {
        var Player = GameManager.Instance.Player;
        switch (Player.RelationStatus)
        {
            case RelationStatus.TERRIBLE:
                break;
            case RelationStatus.BAD:
                HumanState = HumanState.ARGUING;
                transform.DORotate(new Vector3(0, 90f, 0), 0.5f);
                newParticle = Instantiate(GameManager.Instance.Data.Particles[3], transform.position, Quaternion.identity, transform);
                newParticle.transform.localPosition = new Vector3(0, 12.5f, 0);
                break;
            case RelationStatus.NORMAL:
                HumanState = HumanState.IDLE;
                break;
            case RelationStatus.GOOD:
                HumanState = HumanState.KISS;
                transform.DORotate(new Vector3(0, 90f, 0), 0.5f);
                newParticle = Instantiate(GameManager.Instance.Data.Particles[2], transform.position, Quaternion.identity, transform);
                newParticle.transform.localPosition = new Vector3(0, 12.5f, -1.2f);
                break;
            case RelationStatus.EXCELLENT:
                newParticle = Instantiate(GameManager.Instance.Data.Particles[4], transform.position, Quaternion.identity, transform);
                newParticle.transform.localPosition = new Vector3(0, 10f, -2.5f);
                break;
            default:
                break;
        }
    }


}
