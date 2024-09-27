using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;
    

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //golemModel = new GolemModel();
    }

    public void ApplyDamageToEnemy(IAttackable target, int damage)
    {
        // 데미지 적용
        target.TakeDamage(damage);
    }
}
