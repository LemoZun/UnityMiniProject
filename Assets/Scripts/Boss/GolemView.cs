using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GolemView : MonoBehaviour
{
    private GolemModel golemModel;
    private GolemController golem;
    private Animator animator;
    [SerializeField] Slider hpBar;
    private int[] patternHash = new int[]
    {
        Animator.StringToHash("BossStand"),
        Animator.StringToHash("BossSkill1"),
        Animator.StringToHash("BossSkill2"),
        Animator.StringToHash("BossSkill3"),
        Animator.StringToHash("GroundSmash"),
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
            Debug.LogError("�� �������� ����");
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
        Debug.Log("hp ������Ʈ �Ϸ��");
    }



    public void PlayaAnimation(int patternIndex)
    {
        if(patternIndex >= 0 && patternIndex < patternHash.Length)
        {
            animator.Play(patternHash[patternIndex]);
        }
        else
        {
            Debug.LogError("�ִϸ��̼� �ε��� ����");
        }
    }


}
