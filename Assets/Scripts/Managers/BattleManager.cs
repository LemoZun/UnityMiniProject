using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;
    [SerializeField] public PlayerController player;
    [SerializeField] public GolemController golem;

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
        // 이벤트 구독들을 해줘야함
        // player.OnPlayerDied += // 함수들
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += InitializeField;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= InitializeField;
    }


    private void InitializeField(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
    {
        player = FindObjectOfType<PlayerController>();
        golem = FindObjectOfType<GolemController>();
    }


    public void ApplyDamageToEnemy(IAttackable target, int damage)
    {
        // 데미지 적용
        target.TakeDamage(damage);
    }

    public void ApllyDamageToPlayer(int damage)
    {

    }
    public bool IsPlayerInDangerZone(Tilemap dangerZone)
    {
        Vector3 PlayerPositoin = Instance.player.transform.position;

        //플레이어의 위치를 셀 좌표로 변환
        Vector3Int playerCellPosition = dangerZone.WorldToCell(PlayerPositoin);
        return dangerZone.HasTile(playerCellPosition);
    }
}
