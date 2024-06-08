using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Saw : Trap
{
    public Animator anim;
    public Transform[] movePositions;       // 톱니바퀴가 이동할 위치를 저장할 변수
    public float speed = 5f;
    public int moveIndex = 0;
    public bool OnGoingForward = true;
    public bool IsTrapOn = true;           // MoveTrap함수를 이동 시킬지 말지 제어하는 변수
    public float stopTime = 1f;
    private void Start()
    {
        anim = GetComponent<Animator>();

        isWorking = true;
    }

    private void Update() // 컴퓨터 성능 영향을 받는다.
    {
        anim.SetBool("isWorking", isWorking);

        // if(조건 == true)
        //StartCoroutine(CoMoveTrap());  코루틴을 Update에서 함부로 사용하면 안된다.
        // IsTrapon 에 따라 MoveTrap을 실행 시키는 조건
        if(IsTrapOn == true)
        {
            MoveTrap();
        }
    }

    IEnumerator CoMoveTrap()
    {
        IsTrapOn = false;
        yield return new WaitForSeconds(stopTime);
        IsTrapOn = true;
    }

    private void MoveTrap()
    {
        // 평균으로 0.0016 값
        transform.position = Vector3.MoveTowards(transform.position, movePositions[moveIndex].position, speed * Time.deltaTime);

        // 조건문 - 함정이 목표한 지점까지 도착했는지?
        if(Vector3.Distance(transform.position, movePositions[moveIndex].position) <= 0.1f)
        {
            moveIndex = moveIndex + 1;
            StartCoroutine(CoMoveTrap());
        }
        // 다음 목표 지점이 없으면.. move Index = 0.
        if(movePositions.Length <= moveIndex)
        {
            moveIndex = 0;
        }
    }


    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if (collision.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
        }
    }
}
