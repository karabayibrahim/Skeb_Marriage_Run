using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        HumanState = HumanState.WALK;
    }

    private void IdleState()
    {
        HumanState = HumanState.IDLE;
    }

}
