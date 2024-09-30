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
        // �̺�Ʈ �������� �������
        // player.OnPlayerDied += // �Լ���
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
        // ������ ����
        target.TakeDamage(damage);
    }

    public void ApllyDamageToPlayer(int damage)
    {

    }
    public bool IsPlayerInDangerZone(Tilemap dangerZone)
    {
        Vector3 PlayerPositoin = Instance.player.transform.position;

        //�÷��̾��� ��ġ�� �� ��ǥ�� ��ȯ
        Vector3Int playerCellPosition = dangerZone.WorldToCell(PlayerPositoin);
        return dangerZone.HasTile(playerCellPosition);
    }
}
