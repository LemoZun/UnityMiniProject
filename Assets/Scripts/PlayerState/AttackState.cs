using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class AttackState : IPlayerState
{
    private PlayerController player;
    private bool isAttacking = false;
    int lastDirection;
    AnimatorStateInfo stateInfo;
    public AttackState(PlayerController player)
    {
        this.player = player;
    }

    public void Enter()
    {
        Debug.Log("Attack");
        isAttacking = true;
        player.animator.enabled = true;
        

    }
    public void Update()
    {

        Attack();
        // 애니메이터의 현재 상태를 가져옴
        stateInfo = player.animator.GetCurrentAnimatorStateInfo(0);

        //애니메이션이 진행되는 동안 상태를 바꾸지 못하도록
        if(isAttacking && stateInfo.normalizedTime >= 0.8f)
        {
            
            player.SetState(PlayerController.State.Idle, lastDirection);
        }
    }

    public void Exit()
    {
        isAttacking = false;
    }

    private void Attack()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - player.transform.position).normalized;

        // 좌표를 라디안으로 변환
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //int spriteIndex
        lastDirection = player.GetSpriteIndex(angle); //direction index와 같음 나중에 합쳐야함
        player.PlayAttackAnimation(lastDirection);
    }
    //private void LookAtMouse()
    //{
    //    Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //    Vector2 direction = (mousePosition - transform.position).normalized;

    //    // 좌표를 라디안으로 변환
    //    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    //    int spriteIndex = GetSpriteIndex(angle);

    //    // 좌표에 맞는 스프라이트로 변경
    //    spriteRenderer.sprite = directionSprites[spriteIndex];
    //}
}
