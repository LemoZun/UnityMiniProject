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
        // �ִϸ������� ���� ���¸� ������
        stateInfo = player.animator.GetCurrentAnimatorStateInfo(0);

        //�ִϸ��̼��� ����Ǵ� ���� ���¸� �ٲ��� ���ϵ���
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

        // ��ǥ�� �������� ��ȯ
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //int spriteIndex
        lastDirection = player.GetSpriteIndex(angle); //direction index�� ���� ���߿� ���ľ���
        player.PlayAttackAnimation(lastDirection);
    }
    //private void LookAtMouse()
    //{
    //    Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //    Vector2 direction = (mousePosition - transform.position).normalized;

    //    // ��ǥ�� �������� ��ȯ
    //    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    //    int spriteIndex = GetSpriteIndex(angle);

    //    // ��ǥ�� �´� ��������Ʈ�� ����
    //    spriteRenderer.sprite = directionSprites[spriteIndex];
    //}
}
