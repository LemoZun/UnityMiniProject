using System.Collections;
using UnityEngine;

public class AttackState : IPlayerState
{
    private PlayerController player;
    private int lastDirection;
    private float aniPlayTime = 0.8f;
    AnimatorStateInfo stateInfo;
    private bool isAttacking = false;

    Coroutine attackRoutine;


    public AttackState(PlayerController player)
    {
        this.player = player;
    }

    public void Enter()
    {
        player.rb.velocity = Vector2.zero;
        Debug.Log("Attack");
        
        if(isAttacking)
        {
            Debug.Log("이미 공격중입니다");
            return;
        }

        player.animator.enabled = true;
        Attack();

        if (attackRoutine == null)
        {
            attackRoutine = player.StartCoroutine(AttackRoutine());
        }
    }
    public void Update()
    {
    }

    public void Exit()
    {
        if (attackRoutine != null)
        {
            player.StopCoroutine(attackRoutine);
            attackRoutine = null;
        }

    }

    private void Attack()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - player.transform.position).normalized;

        // 좌표를 라디안으로 변환
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        lastDirection = player.GetSpriteIndex(angle); //direction index와 같음 나중에 합쳐야함
        player.PlayIdleSprite(lastDirection);
        player.PlayAttackAnimation(lastDirection);
        
        isAttacking = true;
    }

    IEnumerator AttackRoutine()
    {
        WaitForSeconds delay = new WaitForSeconds(aniPlayTime);

        yield return delay;
        

        while (Input.GetMouseButton(0))
        {
            Attack();
            
            yield return delay;
            
        }

        //애니메이터의 현재 상태를 가져옴
        stateInfo = player.animator.GetCurrentAnimatorStateInfo(0);
        //애니메이션이 진행되는 동안 상태를 바꾸지 못하도록
        while(stateInfo.normalizedTime < aniPlayTime)
        {
            stateInfo = player.animator.GetCurrentAnimatorStateInfo(0);
            yield return null;
            
        }
        isAttacking = false;
        player.SetState(PlayerController.State.Idle, lastDirection);
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
