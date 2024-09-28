using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Path.GUIFramework;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GolemController : MonoBehaviour, IAttackable
{

    public enum GolemState { Idle, GroundSmash, Skill2, Skill3, Skill4, Die, Size}
    private IGolemState[] states = new IGolemState[(int)GolemState.Size];

    IGolemState curState;

    // �� ��ũ��Ʈ�� ���� �������ϳ�?
    [SerializeField] public Tilemap[] dangerZones;

    public GolemModel golemModel;
    public GolemView golemView;
    public Animator animator;
    Coroutine timeTable;
    public bool isPlayingPattern = false;
    int tempCount = 0;

    private void Awake()
    {
        golemModel = new GolemModel();
        if( golemModel == null)
        {
            Debug.LogError("Golem Model ���� ����");
        }

        //�ϴ� ���� ����
        golemView = GetComponent<GolemView>();

    }
    private void Start()
    {
        states[(int)GolemState.Idle] = new GolemIdleState(this);
        states[(int)GolemState.GroundSmash] = new GolemGroundSmash(this);
        animator = GetComponent<Animator>();
        curState = states[(int)GolemState.Idle];
        Debug.Log("ù Idle ���� ����");
        curState.Enter();
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
        while (!isPlayingPattern)
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

    }
}
