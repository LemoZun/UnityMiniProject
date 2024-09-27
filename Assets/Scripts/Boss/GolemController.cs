using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemController : MonoBehaviour, IAttackable
{
    public GolemModel golemModel;

    private void Awake()
    {
        golemModel = new GolemModel();
        if( golemModel == null)
        {
            Debug.LogError("Golem Model 생성 에러");
        }
    }
    private void Start()
    {
        
        
        //golemModel = new GolemModel();
    }

    private void Update()
    {
        
    }
    public void TakeDamage(int damage)
    {
        golemModel.TakeDamage(damage);
        if(golemModel.HP <= 0)
        {
            Die(); // 이벤트로 여러 작업을 처리할수도 있음
        }
    }

    private void Die()
    {
    }
}
