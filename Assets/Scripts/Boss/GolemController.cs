using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.U2D.Path.GUIFramework;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GolemController : MonoBehaviour, IAttackable
{

    public enum GolemState { Idle, GroundSmash, Skill2, Skill3, Skill4, Dead, Size}
    private IGolemState[] states = new IGolemState[(int)GolemState.Size];

    IGolemState curState;

    //public event Action OnGolemDied;

    // �� ��ũ��Ʈ�� ���� �������ϳ�?
    [SerializeField] public Tilemap[] dangerZones;
    [SerializeField] public GameObject[] alarms;
    public GolemModel golemModel;
    public GolemView golemView;
    public Animator animator;
    Coroutine timeTable;
    public bool isPlayingPattern = false;
    public bool isAlive;
    int tempCount = 0;

    private void Awake()
    {
        golemModel = new GolemModel();
        if( golemModel == null)
        {
            Debug.LogError("Golem Model ���� ����");
        }

        isAlive = true;
       

        //�ϴ� ���� ����
        golemView = GetComponent<GolemView>();

    }
    private void Start()
    {
        foreach(var tilemap in dangerZones)
        {
            tilemap.gameObject.SetActive(false);
        }

        foreach (var alarm in alarms)
        {
            alarm.gameObject.SetActive(false);
        }

        golemModel.OnGolemDied += Die;



        states[(int)GolemState.Idle] = new GolemIdleState(this);
        states[(int)GolemState.GroundSmash] = new GolemGroundSmash(this);
        states[(int)GolemState.Dead] = new GolemDieState(this);
        animator = GetComponent<Animator>();
        curState = states[(int)GolemState.Idle];
        Debug.Log("ù Idle ���� ����");
        curState.Enter();
    }

    private void Update()
    {
        
    }

    private void OnDestroy()
    {
        golemModel.OnGolemDied -= Die;
    }

    public void TakeDamage(int damage)
    {
        if(curState != states[(int)GolemState.Dead])
        {
            golemModel.TakeDamage(damage);
        }
        else
        {
            Debug.Log("������ �̹� �׾����ϴ�");
        }
        
        //if(golemModel.HP <= 0)
        //{
        //    Die(); // �̺�Ʈ�� ���� �۾��� ó���Ҽ��� ����
        //}
    }
    public void SetState(GolemState newState)
    {
        if (curState != null)
        {
            curState.Exit();
        }

        Debug.Log($"���ο� ���� ���� : {newState}");

        curState = states[(int)newState];
        curState.Enter();
    }

    public void TimeTable(float duration)
    {
        if(timeTable == null)
        {
            timeTable = StartCoroutine(IdleTimer(duration));
        }
        else
        {
            Debug.LogError("�̹� Ÿ�����̺� ����");
        }
    }

    IEnumerator IdleTimer(float _duration)
    {
        tempCount++;
        //Debug.Log(tempCount);
        WaitForSeconds duration = new WaitForSeconds(_duration);
        yield return duration;
        Debug.Log(isPlayingPattern);
        while (!isPlayingPattern && isAlive)
        {
            Debug.Log($"{tempCount} ��° groundSmash �ڷ�ƾ���� ����");
            SetState(GolemState.GroundSmash);
            if(timeTable != null)
            {
                Debug.Log("�ڷ�ƾ �ߴ�");
                StopCoroutine(timeTable);
                timeTable = null;
            }
        }
        

        // ������ �� �߰��ؾ���
        // idle ���¿����� �����ϰ� idle ���¿��� �ִ� �ð��� �����

    }

    //private void OnDestroy()
    //{
    //    StopCoroutine(timeTable);
    //}


    private void Die()
    {
        Debug.Log("�� ����");
        SetState(GolemState.Dead);
        // �����ϰ��־���
    }

    public void StopAllActions()
    {
        isPlayingPattern = false;
        golemView.StopAnimation();
        foreach (GameObject alarm in alarms)
        {
            if (alarm.activeSelf)
            {
                alarm.SetActive(false);
            }
        }

        //StopAllActions();
    }

}
