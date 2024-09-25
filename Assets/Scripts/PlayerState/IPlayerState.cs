using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerState
{
    // 저번엔 클래스로 했으니 이번엔 인터페이스로
    void Enter();
    void Update();
    void Exit();

}
