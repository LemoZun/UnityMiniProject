using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GolemGroundSmash : IGolemState
{
    private GolemController golem;
    //private GolemModel golemModel;
    //private GolemView golemView;
    private int startAnimationIndex = 4;
    private int endAnimationIndex = 5;
    //AnimatorStateInfo stateInfo;
    

    //[SerializeField] private Tilemap[] dangerZones;
    private int smashCount = 0;
    private PlayerController player;

    public GolemGroundSmash(GolemController golem)
    {
        this.golem = golem;
        //golemModel = golem.golemModel;
        //golemView = golem.golemView;
    }

    public void Enter()
    {
        Debug.Log("Ground Smash ���� ����");
        golem.golemView.OnAnimationEnd += EndGroundSmash;
        StratGroundSmash();
        Update();
        // ����Ƽ �̺�Ʈ�� �ִϸ��̼��� ���� �� endGroundSmash �ִϸ��̼� ��� // ���
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
        //golem.SetState(GolemController.GolemState.Idle);
        EndGroundSmash();

    }

    private void StratGroundSmash()
    {
        // groundSmashStart �ִϸ��̼� ���?
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
        golem.dangerZones[smashCount].gameObject.SetActive(true);

        if (IsPlayerInDangerZone(golem.dangerZones[smashCount]))
        {
            Debug.Log("�÷��̾� ����");
            //�÷��̾� ����
            //���ӿ���
        }
        else
        {
            Debug.Log("1���� �Ϸ�");
        }
        smashCount++;
        golem.golemView.OnAnimationEnd -= ActivateDangerZone;
        golem.SetState(GolemController.GolemState.Idle);

    }

    private bool IsPlayerInDangerZone(Tilemap dangerZone)
    {
        Vector3 PlayerPositoin = BattleManager.Instance.player.transform.position;

        //�÷��̾��� ��ġ�� �� ��ǥ�� ��ȯ
        Vector3Int playerCellPosition = dangerZone.WorldToCell(PlayerPositoin);
        return dangerZone.HasTile(playerCellPosition);
    }
}
