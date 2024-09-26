using System.Collections;
using UnityEngine;

public class PlayerAttackState : IState
{
    private PlayerController player;
    private int lastDirection;
    private float aniPlayTime = 0.8f;
    AnimatorStateInfo stateInfo;
    private bool isAttacking = false;

    private Coroutine attackRoutine;
    private Collider2D attackHitbox = null;
    private float hitboxRadius = 1f;
    private LayerMask enemyLayer;
    public PlayerAttackState(PlayerController player)
    {
        this.player = player;
        enemyLayer = LayerMask.GetMask("Enemy");
    }

    public void Enter()
    {
        player.rb.velocity = Vector2.zero;
        Debug.Log("Attack");
        
        if(isAttacking)
        {
            Debug.Log("�̹� �������Դϴ�");
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

        // ��ǥ�� �������� ��ȯ
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        lastDirection = player.GetSpriteIndex(angle); //direction index�� ���� ���߿� ���ľ���
        player.PlayIdleSprite(lastDirection);
        player.PlayAttackAnimation(lastDirection);
        CheckAttackHitBox(direction);
        isAttacking = true;
    }

    private void CheckAttackHitBox(Vector2 _direction)
    {
        Vector2 attackPosition = (Vector2)player.transform.position + _direction *2f;
        attackHitbox = Physics2D.OverlapCircle(attackPosition, hitboxRadius, enemyLayer);

        //OnDrawGizmos();

        if (attackHitbox != null)
        {
            Debug.Log("������ ���� ����");

        }
        else
        {
            Debug.Log("Miss");
        }
    }

    private void OnDrawGizmos() //���⼭ ����
    {
        Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.transform.position).normalized;
        Vector2 attackPosition = (Vector2)player.transform.position + direction * 2f;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPosition, hitboxRadius);
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

        //�ִϸ������� ���� ���¸� ������
        stateInfo = player.animator.GetCurrentAnimatorStateInfo(0);
        //�ִϸ��̼��� ����Ǵ� ���� ���¸� �ٲ��� ���ϵ���
        while(stateInfo.normalizedTime < aniPlayTime)
        {
            stateInfo = player.animator.GetCurrentAnimatorStateInfo(0);
            yield return null;
            
        }
        isAttacking = false;
        player.SetState(PlayerController.State.Idle, lastDirection);
    }




    private void DealDamage()
    {

    }


    private void TakeDamage()
    {

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
