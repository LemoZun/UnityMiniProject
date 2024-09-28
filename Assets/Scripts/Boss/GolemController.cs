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

    // 맵 스크립트를 따로 만들어야하나?
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
            Debug.LogError("Golem Model 생성 에러");
        }

        //일단 여기 보류
        golemView = GetComponent<GolemView>();

    }
    private void Start()
    {
        states[(int)GolemState.Idle] = new GolemIdleState(this);
        states[(int)GolemState.GroundSmash] = new GolemGroundSmash(this);
        animator = GetComponent<Animator>();
        curState = states[(int)GolemState.Idle];
        Debug.Log("첫 Idle 상태 진입");
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
            Die(); // 이벤트로 여러 작업을 처리할수도 있음
        }
    }
    public void SetState(GolemState newState)
    {
        if (curState != null)
        {
            curState.Exit();
        }

        Debug.Log($"새로운 상태 지정 : {newState}");

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
            Debug.LogError("이미 타임테이블 존재");
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
            Debug.Log($"{tempCount} 번째 groundSmash 코루틴에서 진입");
            SetState(GolemState.GroundSmash);
            if(timeTable != null)
            {
                Debug.Log("코루틴 중단");
                StopCoroutine(timeTable);
                timeTable = null;
            }
        }
        

        // 로직을 더 추가해야함
        // idle 상태에서만 들어가야하고 idle 상태에서 있던 시간을 재야함

    }

    //private void OnDestroy()
    //{
    //    StopCoroutine(timeTable);
    //}


    private void Die()
    {

    }
}
