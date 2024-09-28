using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GolemGroundSmash : IGolemState
{
    private GolemController golem;
    private int startAnimationIndex = 4;
    private int endAnimationIndex = 5;
    private int smashCount = 0;
    private PlayerController player;

    public GolemGroundSmash(GolemController golem)
    {
        this.golem = golem;
    }

    public void Enter()
    {
        Debug.Log("Ground Smash ���� ����");
        golem.isPlayingPattern = true;
        golem.golemView.OnAnimationEnd += EndGroundSmash;
        StratGroundSmash();
        Update();
       
        //golem.SetState(GolemController.GolemState.Idle);

    }

    public void Update()
    {
        golem.golemView.StartCheckingEndRoutine();
        //Debug.Log("Ground Smash ���� �ִϸ��̼� ����");
        //golem.SetState(GolemController.GolemState.Idle);


    }

    public void Exit()
    {
        Debug.Log("GroundSmash Exit");
        golem.isPlayingPattern = false;
        //golem.SetState(GolemController.GolemState.Idle);
        //EndGroundSmash();

    }

    private void StratGroundSmash()
    {
        Debug.Log("Ground Smash ���� �ִϸ��̼� ����");
        golem.golemView.PlayaAnimation(startAnimationIndex);

        
        // �ִϸ��̼��� ������ �ٷ� �ڿ� returnGroundSmash �� ���;���
    }

    private void EndGroundSmash()
    {
        golem.golemView.OnAnimationEnd -= EndGroundSmash;
        golem.golemView.OnAnimationEnd += ActivateDangerZone;
        Debug.Log("Ground smash ���� ���� ����");
        //�� �������ٴ°� ǥ���ϴ� ��������Ʈ �־��ֱ�?
        golem.golemView.PlayaAnimation(endAnimationIndex);
        golem.golemView.StartCheckingEndRoutine();

        ActivateDangerZone();
        
    }

    private void ActivateDangerZone()
    {
        if(smashCount < 4)
        {
            Debug.Log("�� �μ��� ����");
            golem.dangerZones[smashCount].gameObject.SetActive(true);

            if (IsPlayerInDangerZone(golem.dangerZones[smashCount]))
            {
                Debug.Log("�÷��̾� ����");
                //�÷��̾� ����
                //���ӿ���
            }
            else
            {
                Debug.Log($"{smashCount+1}��° GroundSmash ���� �Ϸ�");
            }


            smashCount++;
            golem.golemView.OnAnimationEnd -= ActivateDangerZone;
            golem.SetState(GolemController.GolemState.Idle);
        }
        else
        {
            Debug.Log("���� ���� ���� ���� ����");
        }


    }

    private bool IsPlayerInDangerZone(Tilemap dangerZone)
    {
        Vector3 PlayerPositoin = BattleManager.Instance.player.transform.position;

        //�÷��̾��� ��ġ�� �� ��ǥ�� ��ȯ
        Vector3Int playerCellPosition = dangerZone.WorldToCell(PlayerPositoin);
        return dangerZone.HasTile(playerCellPosition);
    }
}
