using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class NewMale : Human
{
    private GameObject newParticle;
    public GameObject Ring;

    public override void IdleState()
    {
        if (GameManager.Instance.Player.IdleRandomIndex == 0)
        {
            Ring.SetActive(false);
            HumanState = HumanState.KISS;
            transform.DOLocalMove(new Vector3(-3, 0, 0), 0.5f);
            transform.DORotate(new Vector3(0, -90f, 0), 0.5f);
            newParticle = Instantiate(GameManager.Instance.Data.Particles[2], transform.position, Quaternion.identity, transform);
            newParticle.transform.localPosition = new Vector3(-3, 13.5f, -0.5f);
        }

        else if (GameManager.Instance.Player.IdleRandomIndex == 1)
        {
            Ring.SetActive(true);
            HumanState = HumanState.RING;
            transform.DORotate(new Vector3(0, -90f, 0), 0.5f);
            newParticle = Instantiate(GameManager.Instance.Data.Particles[10], transform.position, Quaternion.identity, transform);
            newParticle.transform.localPosition = new Vector3(-4, 11.5f, -1.5f);
        }

        //HumanState = HumanState.CARRYIDLE;
        //transform.DOLocalMove(new Vector3(-3, 0, 0), 0.5f);
        //newParticle = Instantiate(GameManager.Instance.Data.Particles[6], transform.position, Quaternion.identity, transform);
        //newParticle.transform.localPosition = new Vector3(0, 12.5f, -1.2f);
    }

    private void FinishState()
    {
        Ring.SetActive(false);
        HumanState = HumanState.KISS;
        transform.DOLocalMove(new Vector3(-3, 0, 0), 0.5f);
        transform.DORotate(new Vector3(0, -90f, 0), 0.5f);
        newParticle = Instantiate(GameManager.Instance.Data.Particles[2], transform.position, Quaternion.identity, transform);
        newParticle.transform.localPosition = new Vector3(-3, 13.5f, -0.5f);
    }

    public override void WalkState()
    {
        Ring.SetActive(false);
        if (GameManager.Instance.Player.WalkRandomIndex == 0)
        {
            Destroy(newParticle);
            HumanState = HumanState.CARRY;
            transform.DOLocalMove(new Vector3(-3, 0, 0), 0.5f);
            transform.DORotate(new Vector3(0, 0, 0), 0.5f);
        }
        else if (GameManager.Instance.Player.WalkRandomIndex == 1)
        {
            Destroy(newParticle);
            HumanState = HumanState.SHOULDER;
            transform.DOLocalMove(new Vector3(-3, 0, 0), 0.5f);
            transform.DORotate(new Vector3(0, 0, 0), 0.5f);
        }

    }

    private void OnEnable()
    {
        PlayerController.WalkAction += WalkState;
        PlayerController.IdleAction += IdleState;
        GameManager.FinishEvent += FinishState;
    }

    private void OnDisable()
    {
        PlayerController.WalkAction -= WalkState;
        PlayerController.IdleAction -= IdleState;
        GameManager.FinishEvent -= FinishState;
    }

    // Start is called before the first frame update
    void Start()
    {
        base._anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
