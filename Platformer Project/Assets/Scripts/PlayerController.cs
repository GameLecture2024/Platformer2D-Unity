using System;
using UnityEditor;
using UnityEngine;

// ���� ������ public private protected
public class PlayerController : MonoBehaviour
{
    // Start, Update ����Ƽ �̺�Ʈ �Լ��� ���� �̸��� �ִ��� ����
    // ���� �̸��� ������? ����Ƽ���� ���س��� ���� �ð��� �� �Լ��� ����

    // Start is called before the first frame update
    // ù �������� �ҷ����� ���� (�ѹ�) �����Ѵ�. �ѹ���!

    // �ӵ�, ����
    [Header("�̵�")]
    public float moveSpeed = 5f;   // ĳ������ �̵� �ӵ�
    public float JumpForce = 10f;
    private float moveInput;  // �÷��̾��� ���� �� ��ǲ ������ ����

    //public Transform startTransform; // ĳ���Ͱ� ������ ��ġ
    public Rigidbody2D rigidbody2D;  // ����(��ü) ����� �����ϴ� ������Ʈ

    [Header("����")]
    public bool isGrounded;          // true : ĳ���Ͱ� ���� �� �� �ִ� ����, false : ���� ����
    public float groundDistance = 2f;
    public LayerMask groundLayer;

    [Header("Flip")]
    public SpriteRenderer spriteRenderer;
    private bool facingRight = true;
    private int facingDirection = 1;


    [SerializeField] ParticleController particleController;
    public Animator animator;
    private bool isMove;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
            
        Debug.Log("Hello Unity");
        // ���� �� ��ġ <= ���ο� x,y �����ϴ� ������ Ÿ��( ���� x��ǥ, 10 y��ǥ)
        //transform.position = new Vector2(transform.position.x, 10);

        // ���� �� ��ġ�� startTransform���� ����
        InitializePlayerStatus();
    }

    void InitializePlayerStatus()
    {
        //transform.position = startTransform.position;
        rigidbody2D.velocity = Vector2.zero;
        facingRight = true;
        spriteRenderer.flipX = false;
    }

    // Update is called once per frame
    // 1 ������ ���� ȣ��ȴ�. - �ݺ������� ���� 
    void Update()
    {
        // �Լ� �̸� �տ� ���콺Ŀ���� �ΰ� Ctlr + R + R 
        HandleAnimation();
        CollisionCheck();
        HandleInput();
        HandleFlip();
        Move();
        FallDownCheck();
    }

    private void FallDownCheck()
    {
        // y�� ���̰� Ư�� �������� ���� �� ������ ������ �����Ѵ�. => �浹 üũ ��ü
        if(transform.position.y < -11)
        {
            InitializePlayerStatus();
        }
    }

    private void HandleAnimation()
    {
        // rigidbody.velocity : ���� rigidbody �ӵ� = 0 �������� �ʴ� ����, !=0 �����̰� �ִ� ����
        isMove = rigidbody2D.velocity.x != 0;  
        animator.SetBool("isMove", isMove);
        animator.SetBool("isGrounded", isGrounded);
        // SetFloat �Լ��� ���ؼ� y�ִ��� �� 1�� ��ȯ.. y �ּ��� �� -1�� ��ȯ
        // ���� Ű�� ������. ���������� y ���� ����, �߷¿� ���ؼ� ���� y �ӵ� -���� �������̴ϴ�.
        animator.SetFloat("yVelocity", rigidbody2D.velocity.y);  
    }

    /// <summary>
    /// ������ �� �� ������ �ƴ��� üũ �ϴ��� ��� -> Collider Check
    /// </summary>
    private void CollisionCheck()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundDistance, groundLayer);

        particleController.isGround = isGrounded;
    }
    /// <summary>
    /// �÷��̾��� �Է� ���� �޾ƿ;� �մϴ�.  a,d Ű���� �� �� Ű�� ������ �� -1 ~ 1 ��ȯ�ϴ� Ŭ����
    /// </summary>
    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            particleController.PlayParticle();
        }

        moveInput = Input.GetAxis("Horizontal");

        JumpButton();
    }

    private void HandleFlip()
    {
        // ������ �������� �ٶ󺸰� ���� �� 
        if(facingRight && moveInput < 0)
        {
            Flip();
        }
        // ���� �������� �ٶ󺸰� ���� ��
        else if(!facingRight && moveInput > 0)
        {
            Flip();
        }
    }

    private void Flip()
    {
        facingDirection = facingDirection * -1;
        facingRight = !facingRight;
        spriteRenderer.flipX = !facingRight;
    }

    private void JumpButton()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            Jump();
        }
    }

    private void Jump()
    {
        // 점프 사운드 출력.
        // SFX 배열에 등록된 효과음 출력 숫자 2는 Jump1에 해당함.
        AudioManager.instance.PlaySFX(2); 
        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, JumpForce);
    }


    private void Move()
    {
        rigidbody2D.velocity = new Vector2(moveSpeed * moveInput, rigidbody2D.velocity.y);
    }


    private void PlatformMove(Vector2 power)
    {
        rigidbody2D.velocity = power;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - groundDistance));
    }

}
