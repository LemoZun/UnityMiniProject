using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum State { Idle, Walk, Attack, Size }
    private IPlayerState[] states = new IPlayerState[(int)State.Size];
    private IPlayerState curState;
    public float moveSpeed;
    public Rigidbody2D rb;
    public Animator animator;

    [SerializeField] Sprite[] directionSprites = new Sprite[8];




    [SerializeField] AnimationClip[] animations = new AnimationClip[8];

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
       


    private SpriteRenderer spriteRenderer;

    private int lastDirectionIndex = 0;

    private void Awake()
    {
        
    }


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        states[(int)State.Idle] = new IdleState(this);
        states[(int)State.Walk] = new WalkState(this);
        states[(int)State.Attack] = new AttackState(this);
        spriteRenderer = GetComponent<SpriteRenderer>();


        curState = states[(int)State.Idle];
        curState.Enter();
        Debug.Log("ù IDLE ���� ����");


        
    }

    private void Update()
    {
        curState.Update();
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("���콺 �Է�");
            //LookAtMouse();
        }
        //Move();
        
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
            ((IdleState)curState).SetLastDirectionIndex(lastDirectionIndex);
            
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
            ((IdleState)curState).SetLastDirectionIndex(direction);

        }
        else
        {
            curState = states[(int)newState];
        }

        curState.Enter();
    }
    public Sprite GetSprite(int index)
    {
        if (index >= 0 && index < directionSprites.Length)
        {
            return directionSprites[index];
        }
        else
        {
            Debug.LogError("�߸��� �ε���");
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
            animator.Play(attackHash[index]);
        }
    }

    public void PlayIdleSprite(int index)
    {
        if(index >= 0 && index < directionSprites.Length)
        {
            spriteRenderer.sprite = directionSprites[index];
        }
    }

    //���� ���� Ȯ�ο�
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

        // ��ǥ�� �������� ��ȯ
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        int spriteIndex = GetSpriteIndex(angle);

        // ��ǥ�� �´� ��������Ʈ�� ����
        spriteRenderer.sprite = directionSprites[spriteIndex];
    }


    // 8 ���⿡ ���� ��������Ʈ index�� ��ȯ
    public int GetSpriteIndex(float angle)
    {
        if (angle >= -22.5f && angle < 22.5f)
            return 0; // ��
        else if (angle >= 22.5f && angle < 67.5f)
            return 1; // ���
        else if (angle >= 67.5f && angle < 112.5f)
            return 2; // ��
        else if (angle >= 112.5f && angle < 157.5f)
            return 3; // �»�
        else if (angle >= 157.5f || angle < -157.5f)
            return 4; // ��
        else if (angle >= -157.5f && angle < -112.5f)
            return 5; // ����
        else if (angle >= -112.5f && angle < -67.5f)
            return 6; // ��
        else // angle >= -67.5f && angle < -22.5f ��������
            return 7; // ����
    }
}

/* ������ ����
 * 
    // �ʹ�.. ����..
    //private static int walkRightHash = Animator.StringToHash("Walk_Right");
    //private static int walkUpRightHash = Animator.StringToHash("Walk_UpRight");
    //private static int walkUpHash = Animator.StringToHash("Walk_Up");
    //private static int walkUpLeftHash = Animator.StringToHash("Walk_UpLeft");
    //private static int walkLeftHash = Animator.StringToHash("Walk_Left");
    //private static int walkDownLeftHash = Animator.StringToHash("Walk_DownLeft");
    //private static int walkDownHash = Animator.StringToHash("Walk_Down");
    //private static int walkDownRightHash = Animator.StringToHash("Walk_DownRight");

            //switch (index)
            //{
            //    case 0:
            //        animator.Play(walkRightHash);
            //        break;
            //    case 1:
            //        animator.Play(walkUpRightHash);
            //        break;
            //    case 2:
            //        animator.Play(walkUpHash);
            //        break;
            //    case 3:
            //        animator.Play(walkUpLeftHash);
            //        break;
            //    case 4:
            //        animator.Play(walkLeftHash);
            //        break;
            //    case 5:
            //        animator.Play(walkDownLeftHash);
            //        break;
            //    case 6:
            //        animator.Play(walkDownHash);
            //        break;
            //    case 7:
            //        animator.Play(walkDownRightHash);
            //        break;
            //}

    //public void Move()
    //{
    //    float horizontalInput = Input.GetAxis("Horizontal");
    //    float verticalInput = Input.GetAxis("Vertical");

    //    Vector2 moveDirection = new Vector2(horizontalInput, verticalInput).normalized;

        

    //    // ��ǥ�� �´� ��������Ʈ�� ����
    //    if(moveDirection != Vector2.zero)
    //    {
    //        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
    //        int spriteIndex = GetSpriteIndex(angle);
    //        spriteRenderer.sprite = directionSprites[spriteIndex];
    //        animator.Play(animations[spriteIndex].name);
    //    }
    //    rb.velocity = moveDirection * moveSpeed;

    //    if (moveDirection == Vector2.zero)
    //    {
    //        // ���� ��������Ʈ�� ����
    //    }
    //}



 */