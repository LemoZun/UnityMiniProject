using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerIdleState : IState
{
    private PlayerController player;
    private int lastDirectionIndex;

    public PlayerIdleState(PlayerController player)
    {
        this.player = player;
    }

    public PlayerIdleState(PlayerController player, int lastDirectionIndex)
    {
        this.player = player;
        this.lastDirectionIndex = lastDirectionIndex;
    }

    public void Enter()
    {
        Debug.Log("Idle 상태 진입");
        player.animator.enabled = false;
        player.rb.velocity = Vector2.zero;
        player.PlayIdleSprite(lastDirectionIndex);
    }
    public void Update()
    {
        // 이동시
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            Debug.Log("방향키 입력 감지됨");
            player.SetState(PlayerController.State.Walk);
        }

        //공격시
        if (Input.GetMouseButtonDown(0))
        {
            player.SetState(PlayerController.State.Attack);
        }
    }

    public void Exit()
    {

    }

    public void SetLastDirectionIndex(int directionIndex)
    {
        lastDirectionIndex = directionIndex;
    }
}
