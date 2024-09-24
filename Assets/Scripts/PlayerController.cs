using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] Sprite[] directionSprites = new Sprite[8];
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            LookAtMouse();
        }
        Move();
        
    }

    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector2 moveDirection = new Vector2(horizontalInput, verticalInput);

        rb.velocity = moveDirection * moveSpeed;

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
    private int GetSpriteIndex(float angle)
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
