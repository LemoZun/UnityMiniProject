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
        Debug.Log("Ground Smash 패턴 진입");
        golem.golemView.OnAnimationEnd += EndGroundSmash;
        StratGroundSmash();
        Update();
        // 유니티 이벤트로 애니메이션이 끝날 시 endGroundSmash 애니메이션 재생 // 취소
        //golem.SetState(GolemController.GolemState.Idle);

    }

    public void Update()
    {
        golem.golemView.StartCheckingEndRoutine();
        //Debug.Log("Ground Smash 시작 애니메이션 종료");
        //golem.SetState(GolemController.GolemState.Idle);

    }

    public void Exit()
    {
        //golem.SetState(GolemController.GolemState.Idle);
        EndGroundSmash();

    }

    private void StratGroundSmash()
    {
        // groundSmashStart 애니메이션 재생?
        Debug.Log("Ground Smash 패턴 애니메이션 시작");
        golem.golemView.PlayaAnimation(startAnimationIndex);

        
        // 애니메이션이 끝나면 바로 뒤에 returnGroundSmash 가 나와야함
    }

    private void EndGroundSmash()
    {
        golem.golemView.OnAnimationEnd -= EndGroundSmash;
        golem.golemView.OnAnimationEnd += ActivateDangerZone;
        Debug.Log("Ground smash 패턴 종료 시작");
        //곧 무너진다는걸 표현하는 스프라이트 넣어주기?
        golem.golemView.PlayaAnimation(endAnimationIndex);
        golem.golemView.StartCheckingEndRoutine();

        ActivateDangerZone();
        
    }

    private void ActivateDangerZone()
    {
        golem.dangerZones[smashCount].gameObject.SetActive(true);

        if (IsPlayerInDangerZone(golem.dangerZones[smashCount]))
        {
            Debug.Log("플레이어 죽음");
            //플레이어 죽음
            //게임오버
        }
        else
        {
            Debug.Log("1패턴 완료");
        }
        smashCount++;
        golem.golemView.OnAnimationEnd -= ActivateDangerZone;
        golem.SetState(GolemController.GolemState.Idle);

    }

    private bool IsPlayerInDangerZone(Tilemap dangerZone)
    {
        Vector3 PlayerPositoin = BattleManager.Instance.player.transform.position;

        //플레이어의 위치를 셀 좌표로 변환
        Vector3Int playerCellPosition = dangerZone.WorldToCell(PlayerPositoin);
        return dangerZone.HasTile(playerCellPosition);
    }
}
