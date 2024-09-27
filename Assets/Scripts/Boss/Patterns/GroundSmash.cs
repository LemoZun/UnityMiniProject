using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GroundSmash : IGolemState
{
    private GolemController golem;
    private GolemModel golemModel;
    [SerializeField] private Tilemap dangerZones;


    public GroundSmash(GolemController golem)
    {
        this.golem = golem;
        golemModel = golem.golemModel;
    }

    public void Enter()
    {
        Debug.Log("Ground Smash 패턴 진입");
        
    }

    public void Update()
    {
        throw new System.NotImplementedException();
    }

    public void Exit()
    {
        throw new System.NotImplementedException();
    }
}
