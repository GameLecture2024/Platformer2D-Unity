using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Enemy : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider2D;
    new Rigidbody2D rigidbody2D;

    public PlayerUI playerUI;

    MovementTransform2D movementTransform2D;

    void LoadComponents()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        rigidbody2D = GetComponent<Rigidbody2D>();

        playerUI = FindObjectOfType<PlayerUI>();

    }

    private void Awake()
    {
        LoadComponents();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log($"{collision.gameObject.name}");

            //플레이어의 체력이 떨어지는 로직

            // 체력이 <=0 같을 때 게임을 메인 메뉴로 이동할지, 게임을 종료할지 선택할 수 있는 UI 띄운다.
            // 플레이어 체력을 접근해서, 플레이어 체력과 비교

            PlayerController player = collision.gameObject.GetComponent<PlayerController>();

            // 체력 감소 코드를 먼저 실행
            player.currentHp = player.currentHp - 1;
            playerUI.SliderValueChange(player.currentHp);
            // 게임 오버인지 체크 하는 로직
            if (player.currentHp <= 0)
            {
                MainController.instance.GameOver();
            }


            

        }
    }
}
