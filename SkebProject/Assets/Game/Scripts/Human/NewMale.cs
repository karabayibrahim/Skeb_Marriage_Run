using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMale : Human
{
    private GameObject newParticle;

    public override void IdleState()
    {
        HumanState = HumanState.CARRYIDLE;
        newParticle = Instantiate(GameManager.Instance.Data.Particles[6], transform.position, Quaternion.identity, transform);
        newParticle.transform.localPosition = new Vector3(0, 12.5f, -1.2f);
    }

    public override void WalkState()
    {
        Destroy(newParticle);
        HumanState = HumanState.CARRY;
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
