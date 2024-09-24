using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IdleState : IPlayerState
{
    private PlayerController player;
    private int lastDirectionIndex;

    public IdleState(PlayerController player)
    {
        this.player = player;
    }

    public IdleState(PlayerController player, int lastDirectionIndex)
    {
        this.player = player;
        this.lastDirectionIndex = lastDirectionIndex;
    }

    public void Enter()
    {
        Debug.Log("Idle ���� ����");
    }
    public void Update()
    {
        // �̵���
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            Debug.Log("����Ű �Է� ������");
            player.SetState(PlayerController.State.Walk);
        }

        //���ݽ�
        //if (Input.GetMouseButtonDown(0))
        //{
        //    player.SetState(PlayerController.State.Attack);
        //}
    }

    public void Exit()
    {

    }

    public void SetLastDirectionIndex(int directionIndex)
    {
        lastDirectionIndex = directionIndex;
    }
}
