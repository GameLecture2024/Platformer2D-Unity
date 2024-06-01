using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public GameObject goalObject;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("플레이어가 목표 지점이 도달햇습니다.");

            // 화면에 게임을 클리어 했습니다. 표시. User Interface
            // goalObject는 반드시 TMP_Text 컴포넌트를 갖고 있다.
            goalObject.SetActive(true);  

            if(goalObject.GetComponent<TMP_Text>() != null)
            {
                TMP_Text goalText = goalObject.GetComponent<TMP_Text>();
                goalText.text = "게임 클리어!";
            }

            // 이펙트가 나와라.

            // 효과음 사운드. 배경 음악이 바뀐다.

        }
    }
}
