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
        Debug.Log("Ground Smash 패턴 진입");
        golem.isPlayingPattern = true;
        golem.golemView.OnAnimationEnd += EndGroundSmash;
        StratGroundSmash();
        Update();
       
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
        Debug.Log("GroundSmash Exit");
        golem.isPlayingPattern = false;
        //golem.SetState(GolemController.GolemState.Idle);
        //EndGroundSmash();

    }

    private void StratGroundSmash()
    {
        Debug.Log("Ground Smash 패턴 애니메이션 시작");
        golem.golemView.PlayaAnimation(startAnimationIndex);
        ActivateAlarm();
    }

    private void EndGroundSmash()
    {
        golem.golemView.OnAnimationEnd -= EndGroundSmash;

        //golem.golemView.OnAnimationEnd += ActivateDangerZone;
        //golem.golemView.OnAnimationEnd += DeactivateAlarm;
        Debug.Log("Ground smash 패턴 종료 시작");
        //곧 무너진다는걸 표현하는 스프라이트 넣어주기?
        golem.golemView.PlayaAnimation(endAnimationIndex);
        golem.golemView.StartCheckingEndRoutine();

        // 여기 이상
        DeactivateAlarm();
        ActivateDangerZone();

    }

    private void ActivateDangerZone()
    {
        if(smashCount < 4)
        {
            Debug.Log("맵 부수기 시작");
            golem.dangerZones[smashCount].gameObject.SetActive(true);

            if (BattleManager.Instance.IsPlayerInDangerZone(golem.dangerZones[smashCount]))
            {
                //플레이어 죽음
                BattleManager.Instance.player.DiedPlayer();
                //게임오버
            }
            else
            {
                Debug.Log($"{smashCount+1}번째 GroundSmash 패턴 완료");
            }


            smashCount++;
            //golem.golemView.OnAnimationEnd -= ActivateDangerZone;
            //golem.golemView.OnAnimationEnd -= DeactivateAlarm;

            if(golem.isAlive)
            {
                golem.SetState(GolemController.GolemState.Idle);
            }
            else
            {
                Debug.Log("골렘은 이미 죽어서 IDLE 상태에 들어갈 수 없습니다.");
            }
            
        }
        else
        {
            Debug.Log("가용 맵이 없어 게임 종료");
        }
    }

    private void ActivateAlarm()
    {
        if(smashCount < 4)
        {
            golem.alarms[smashCount].gameObject.SetActive(true);
        }
        else
        {
            Debug.LogError("temp");
        }
    }

    private void DeactivateAlarm()
    {
        if (smashCount < 4)
        {
            golem.alarms[smashCount].gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("temp");
        }
    }

}
