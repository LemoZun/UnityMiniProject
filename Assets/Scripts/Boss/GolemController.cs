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

        curState = states[(int)newState];
        curState.Enter();
    }

    public void TimeTable(float duration)
    {
        if(timeTable == null)
        {
            timeTable = StartCoroutine(IdleTimer(duration));
        }
    }

    IEnumerator IdleTimer(float _duration)
    {
        WaitForSeconds duration = new WaitForSeconds(_duration);
        yield return duration;
        SetState(GolemState.GroundSmash);

        // ������ �� �߰��ؾ���
        // idle ���¿����� �����ϰ� idle ���¿��� �ִ� �ð��� �����

    }

    private void OnDestroy()
    {
        StopCoroutine(timeTable);
    }


    private void Die()
    {

    }
}
