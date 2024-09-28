using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemIdleState : IGolemState
{
    
    private GolemController golem;
    private int animationIndex = 0;
    public GolemIdleState(GolemController golem)
    {
        this.golem = golem;
    }

    public void Enter()
    {
        Debug.Log("골렘 Idle 상태 진입");
        golem.TimeTable(5f); //임시 시간 설정
        PlayGolemIdleAnimation();

    }

    public void Update()
    {
        
    }

    public void Exit()
    {
        
    }

    private void PlayGolemIdleAnimation()
    {
        golem.golemView.PlayaAnimation(animationIndex);
    }

}
