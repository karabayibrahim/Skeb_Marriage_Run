using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class NewMale : Human
{
    private GameObject newParticle;

    public override void IdleState()
    {
        HumanState = HumanState.KISS;
        transform.DOLocalMove(new Vector3(-3, 0, 0), 0.5f);
        transform.DORotate(new Vector3(0, -90f, 0), 0.5f);
        newParticle = Instantiate(GameManager.Instance.Data.Particles[2], transform.position, Quaternion.identity, transform);
        newParticle.transform.localPosition = new Vector3(-3, 13.5f, -0.5f);
        //HumanState = HumanState.CARRYIDLE;
        //transform.DOLocalMove(new Vector3(-3, 0, 0), 0.5f);
        //newParticle = Instantiate(GameManager.Instance.Data.Particles[6], transform.position, Quaternion.identity, transform);
        //newParticle.transform.localPosition = new Vector3(0, 12.5f, -1.2f);
    }

    public override void WalkState()
    {
        Destroy(newParticle);
        HumanState = HumanState.CARRY;
        transform.DOLocalMove(new Vector3(-3, 0, 0),0.5f);
        transform.DORotate(new Vector3(0,0, 0), 0.5f);
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
