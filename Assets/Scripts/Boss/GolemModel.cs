using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GolemModel //: IAttackable
{
    public event Action OnAttacked;
    public event Action OnDied;

    private int hp;
    public int HP { get; set; }
    private const int MaxHP = 500;

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
        Debug.Log("HP 업데이트 해야함 ");

        if(OnAttacked != null)
        {
            OnAttacked?.Invoke();
            if (HP <= 0)
            {
                //죽었을때 처리
                Debug.Log("골렘 죽음");
            }
        }
        else
        {
            Debug.Log("OnAttacked에 구독자가 없다.");
        }


        
        
        
    }

    

}
