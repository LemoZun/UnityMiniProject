using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GolemModel //: IAttackable
{
    public event Action OnGolemAttacked;
    public event Action OnGolemDied;

    private int hp;
    public int HP { get; set; }
    private const int MaxHP = 100;

    private int mp;
    public int MP { get; private set; }
    private const int MaxMP = 100;

    public GolemModel()
    {
        HP = MaxHP;
        MP = 0;
    }

    public void TakeDamage(int damage)
    {
        HP -= damage;
        Debug.Log("HP ������Ʈ �ؾ��� ");

        if(OnGolemAttacked != null)
        {
            OnGolemAttacked?.Invoke();
            if (HP <= 0)
            {
                //�׾����� ó��
                Debug.Log("�� ����");
                OnGolemDied?.Invoke();
            }
        }
        else
        {
            Debug.Log("OnAttacked�� �����ڰ� ����.");
        }


        
        
        
    }

    

}
