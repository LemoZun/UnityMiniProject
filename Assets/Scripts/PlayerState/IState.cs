using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    // ������ Ŭ������ ������ �̹��� �������̽���
    void Enter();
    void Update();
    void Exit();

}
