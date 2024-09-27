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
            Debug.LogError("Golem Model ���� ����");
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
            Die(); // �̺�Ʈ�� ���� �۾��� ó���Ҽ��� ����
        }
    }

    private void Die()
    {
    }
}
