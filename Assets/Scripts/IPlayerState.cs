using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerState
{
    // ������ Ŭ������ ������ �̹��� �������̽���
    void Enter();
    void Update();
    void Exit();

}
