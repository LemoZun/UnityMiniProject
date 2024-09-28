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
        Debug.Log("�� Idle ���� ����");
        golem.TimeTable(5f); //�ӽ� �ð� ����
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
