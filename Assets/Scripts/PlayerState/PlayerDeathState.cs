using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathState : IState
{
    private PlayerController player;
    private int lastDirectionIndex;

    public PlayerDeathState(PlayerController player)
    {
        this.player = player;
    }

    public void Enter()
    {
        Debug.Log("Death 상태 진입");
        player.hpBar.value = 0;
        player.rb.velocity = Vector2.zero;
        player.animator.enabled = true;
        player.PlayDeathAnimation();
        
    }
    public void Update()
    {
        
    }

    public void Exit()
    {
        
    }

    private void Death()
    {

    }

}
