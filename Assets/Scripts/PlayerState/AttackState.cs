using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IPlayerState
{
    private PlayerController player;
    public AttackState(PlayerController player)
    {
        this.player = player;
    }

    public void Enter()
    {
        Debug.Log("Attack 상태 진입");
        player.animator.enabled = true;
        
    }
    public void Update()
    {

    }

    public void Exit()
    {
        
    }


}
