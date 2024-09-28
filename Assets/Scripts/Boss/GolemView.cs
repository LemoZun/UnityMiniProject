using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class GolemView : MonoBehaviour
{
    private GolemModel golemModel;
    private GolemController golem;
    private Animator animator;
    [SerializeField] Slider hpBar;
    public Coroutine waitAnimationEnd;

    public event Action OnAnimationEnd;


    private int[] patternHash = new int[]
    {
        
        Animator.StringToHash("BossIdle"),
        Animator.StringToHash("BossSkill1"),
        Animator.StringToHash("BossSkill2"),
        Animator.StringToHash("BossSkill3"),
        Animator.StringToHash("GroundSmashStart"),
        Animator.StringToHash("GroundSmashEnd"),
        Animator.StringToHash("BossDie")
    };

    private void Awake()
    {
    }

    private void Start()
    {
        golem = GetComponent<GolemController>();
        golemModel = golem.golemModel;
        if(golemModel == null )
        {
            Debug.LogError("모델 가져오기 실패");
        }
        else
        {
            Debug.Log("dummy");
        }

        golemModel.OnAttacked += UpdateHPUI;
        animator = GetComponent<Animator>();
        UpdateHPUI();

        

    }

    private void OnDestroy()
    {
        golemModel.OnAttacked -= UpdateHPUI;
    }


    private void UpdateHPUI()
    {
        hpBar.value = golemModel.HP;
        Debug.Log("hp 업데이트 완료됨");
    }



    public void PlayaAnimation(int patternIndex)
    {
        if(patternIndex >= 0 && patternIndex < patternHash.Length)
        {
            animator.Play(patternHash[patternIndex],0,0);
        }
        else
        {
            Debug.LogError("애니메이션 인덱스 에러");
        }
    }

    public void StartCheckingEndRoutine()
    {
        if (waitAnimationEnd == null)
        {
            waitAnimationEnd = StartCoroutine(WaitAnimationEnd());
        }
    }


    public IEnumerator WaitAnimationEnd()
    {
        AnimatorStateInfo _stateInfo = golem.animator.GetCurrentAnimatorStateInfo(0);
        while (_stateInfo.normalizedTime < 1.0f)
        {
            _stateInfo = golem.animator.GetCurrentAnimatorStateInfo(0);
            yield return null;
        }
        Debug.Log("애니메이션 종료");
        OnAnimationEnd?.Invoke();

        if(waitAnimationEnd != null)
        {
            StopCoroutine(waitAnimationEnd);
        }
    }

    public void CheckAnimationEnd()
    {
        AnimatorStateInfo _stateInfo = golem.animator.GetCurrentAnimatorStateInfo(0);
        while (_stateInfo.normalizedTime < 1.0f)
        {
            _stateInfo = golem.animator.GetCurrentAnimatorStateInfo(0);
        }
        Debug.Log("애니메이션 종료");
    }

}
