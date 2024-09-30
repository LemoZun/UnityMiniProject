using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemDieState : IGolemState
{
    private GolemController golem;
    private int deathAnimationIndex = 6;
    public GolemDieState(GolemController golem)
    {
        this.golem = golem;
    }

    public void Enter()
    {
        Debug.Log("�� ����");
        golem.StopAllActions();
       
        golem.golemView.PlayaAnimation(deathAnimationIndex);
    }
    public void Update()
    {
        throw new System.NotImplementedException();
    }

    public void Exit()
    {
        Debug.Log("Death ���¿��� �������� �õ�");
        
    }


}
