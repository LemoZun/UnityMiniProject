using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : IPlayerState
{
    private PlayerController player;
    private int lastDirectionIndex = 0;

    public WalkState(PlayerController player)
    {
        this.player = player;
    }

    public void Enter()
    {
        Debug.Log("WALK ���� ����");
        //player.animator.SetBool("isWalking", true);
    }

    public void Update()
    {
        Walk();

        if(Input.GetMouseButtonDown(0))
        {
            player.SetState(PlayerController.State.Attack);
        }
        
    }

    public void Exit()
    {
        //player.animator.SetBool("isWalking", false);
    }

    public void Walk()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector2 moveDirection = new Vector2(horizontalInput, verticalInput).normalized;

        // ��ǥ�� �´� ��������Ʈ�� ����
        if (moveDirection != Vector2.zero)
        {
            player.rb.velocity = moveDirection * player.moveSpeed;
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            int directionIndex = GetDirectionIndex(angle);
            player.PlayAnimation(directionIndex);
        }
        else
        {
            player.SetState(PlayerController.State.Idle);
            
        }

        if (moveDirection == Vector2.zero)
        {
            player.SetState(PlayerController.State.Idle);
            // ���� ��������Ʈ�� ����
        }
    }
    public int GetLastDirectionIndex()
    {
        return lastDirectionIndex;
    }

    private int GetDirectionIndex(float angle)
    {
        if (angle >= -22.5f && angle < 22.5f)
            return 0; // ��
        else if (angle >= 22.5f && angle < 67.5f)
            return 1; // ���
        else if (angle >= 67.5f && angle < 112.5f)
            return 2; // ��
        else if (angle >= 112.5f && angle < 157.5f)
            return 3; // �»�
        else if (angle >= 157.5f || angle < -157.5f)
            return 4; // ��
        else if (angle >= -157.5f && angle < -112.5f)
            return 5; // ����
        else if (angle >= -112.5f && angle < -67.5f)
            return 6; // ��
        else // angle >= -67.5f && angle < -22.5f ��������
            return 7; // ����
    }
}
