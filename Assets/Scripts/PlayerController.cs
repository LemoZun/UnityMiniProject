using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public enum State { Idle, Walk, Attack, Death, Size }
    private IState[] states = new IState[(int)State.Size];
    private IState curState;

    //모델을 만든다면 넣어줄것들
    [SerializeField] public AudioClip attackSoundClip;
    [SerializeField] public AudioClip wooshSound;
    private int hp;
    public int HP { get => hp; set => hp = value; }
    private const int MaxHP = 100;
    [SerializeField] public Slider hpBar;
    public float moveSpeed;
    [SerializeField] public int attackPoint;
    public Rigidbody2D rb;
    public bool isPlayerAlive;

    public event Action OnPlayerDied;


    public Animator animator;

    [SerializeField] Sprite[] directionSprites = new Sprite[8];
    
    private
    int[] walkHash = new int[]
    {
        Animator.StringToHash("Walk_Right"),
        Animator.StringToHash("Walk_UpRight"),
        Animator.StringToHash("Walk_Up"),
        Animator.StringToHash("Walk_UpLeft"),
        Animator.StringToHash("Walk_Left"),
        Animator.StringToHash("Walk_DownLeft"),
        Animator.StringToHash("Walk_Down"),
        Animator.StringToHash("Walk_DownRight")
    };

    private int[] attackHash = new int[]
    {
        Animator.StringToHash("Normal_Attack_000"),
        Animator.StringToHash("Normal_Attack_045"),
        Animator.StringToHash("Normal_Attack_090"),
        Animator.StringToHash("Normal_Attack_135"),
        Animator.StringToHash("Normal_Attack_180"),
        Animator.StringToHash("Normal_Attack_225"),
        Animator.StringToHash("Normal_Attack_270"),
        Animator.StringToHash("Normal_Attack_315")
    };

    private int deathHash = Animator.StringToHash("Death");
       


    private SpriteRenderer spriteRenderer;

    private int lastDirectionIndex = 0;

    private void Awake()
    {
        HP = MaxHP;
        hpBar.value = HP;
        isPlayerAlive = true;
    }


    private void Start()
    {
        //OnPlayerDied += 


        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        states[(int)State.Idle] = new PlayerIdleState(this);
        states[(int)State.Walk] = new PlayerWalkState(this);
        states[(int)State.Attack] = new PlayerAttackState(this);
        states[(int)State.Death] = new PlayerDeathState(this);
        spriteRenderer = GetComponent<SpriteRenderer>();


        curState = states[(int)State.Idle];
        curState.Enter();
        Debug.Log("첫 IDLE 상태 진입");

        //Time.timeScale = 0.1f;
        
    }

    private void Update()
    {
        curState.Update();
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("마우스 입력");
            //LookAtMouse();
        }
        //Move();
        
    }

    private void OnDestroy()
    {
        //OnPlayerDied -=
    }

    public void SetState(State newState)
    {
        if(curState != null)
        {
            curState.Exit();
        }

        if(newState == State.Idle)
        {
            curState = states[(int)newState];
            ((PlayerIdleState)curState).SetLastDirectionIndex(lastDirectionIndex);
            
        }
        else
        {
            curState = states[(int)newState];
        }

        curState.Enter();
    }

    public void SetState(State newState, int direction)
    {
        if (curState != null)
        {
            curState.Exit();
        }

        if (newState == State.Idle)
        {
            curState = states[(int)newState];
            ((PlayerIdleState)curState).SetLastDirectionIndex(direction);
        }
        else
        {
            curState = states[(int)newState];
        }

        curState.Enter();
    }

    private void TakeDamage(int damage)
    {
        HP -= damage;
        hpBar.value = HP;

        if (HP <= 0)
        {
            DiedPlayer();
        }
    }

    public void DiedPlayer()
    {
        Debug.Log("플레이어 죽음");
        //hpBar.value = 0;
        SetState(State.Death);
        // 이벤트로 다른것도 해야함
        OnPlayerDied?.Invoke();
    }

    public Sprite GetSprite(int index)
    {
        if (index >= 0 && index < directionSprites.Length)
        {
            return directionSprites[index];
        }
        else
        {
            Debug.LogError("잘못된 인덱스");
            return null;
        }
    }

    public void SetLastDirectionIndex(int directionIndex)
    {
        lastDirectionIndex = directionIndex;
    }

    public void PlayWalkAnimation(int index)
    {
        if(index >= 0 && index < walkHash.Length)
        {
            animator.Play(walkHash[index]);
        }
    }

    public void PlayAttackAnimation(int index)
    {
        if (index >= 0 && index < attackHash.Length)
        {
            animator.Play(attackHash[index],0,0);
        }
    }

    public void PlayIdleSprite(int index)
    {
        if(index >= 0 && index < directionSprites.Length)
        {
            spriteRenderer.sprite = directionSprites[index];
        }
    }

    public void PlayDeathAnimation()
    {
        animator.Play(deathHash,0,0);
    }

    //공격 범위 확인용
    private void OnDrawGizmos()
    {
        Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        Vector2 attackPosition = (Vector2)transform.position + direction* 2f;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPosition, 1f);
    }



    private void LookAtMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - transform.position).normalized;

        // 좌표를 라디안으로 변환
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        int spriteIndex = GetSpriteIndex(angle);

        // 좌표에 맞는 스프라이트로 변경
        spriteRenderer.sprite = directionSprites[spriteIndex];
    }


    // 8 방향에 대한 스프라이트 index를 반환
    public int GetSpriteIndex(float angle)
    {
        if (angle >= -22.5f && angle < 22.5f)
            return 0; // 우
        else if (angle >= 22.5f && angle < 67.5f)
            return 1; // 우상
        else if (angle >= 67.5f && angle < 112.5f)
            return 2; // 상
        else if (angle >= 112.5f && angle < 157.5f)
            return 3; // 좌상
        else if (angle >= 157.5f || angle < -157.5f)
            return 4; // 좌
        else if (angle >= -157.5f && angle < -112.5f)
            return 5; // 좌하
        else if (angle >= -112.5f && angle < -67.5f)
            return 6; // 하
        else // angle >= -67.5f && angle < -22.5f 남은방향
            return 7; // 우하
    }
}