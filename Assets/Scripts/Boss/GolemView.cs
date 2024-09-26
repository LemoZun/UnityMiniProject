using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemView : MonoBehaviour
{
    private Animator animator;

    private int[] patternHash = new int[]
    {
        Animator.StringToHash("BossStand"),
        Animator.StringToHash("BossSkill1"),
        Animator.StringToHash("BossSkill2"),
        Animator.StringToHash("BossSkill3"),
        Animator.StringToHash("GroundSmash"),
        Animator.StringToHash("BossDie")
    };

    private void Start()
    {
        animator = GetComponent<Animator>();
    }


}
