using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern03 : MonoBehaviour
{
    public Transform startPos;      // 원을 방사할 위치
    public int bulletCount;         // 탄막의 갯수
    public float bulletSpeed;       // 탄막의 속도
    public GameObject bulletPrefab; // 탄막 프리팹

    private int MaxBulletCount;     // 패턴 종료를 파악하기 위한 변수

    private void Awake()
    {
        MaxBulletCount = bulletCount;
    }

    private void OnEnable()
    {
        StartCoroutine(SpawnEnemy());
    }

    private void OnDisable()
    {
        StopCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        int count = 0;

        yield return new WaitForSeconds(1f); // 패턴 대기 시간

        while(count < MaxBulletCount)
        {
            CircleEmit();
            count++;
            yield return new WaitForSeconds(1f);
        }
        gameObject.SetActive(false);
    }

    void CircleEmit()
    {
        float angle = 360 / (float)bulletCount; 

        for(int i=0; i<bulletCount; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab,
                startPos.position, Quaternion.identity);

            float bulletAngle = angle * i; // 12도, 24도. 36도 ./...
            float x = Mathf.Cos(bulletAngle * Mathf.PI / 180);
            float y = Mathf.Sin(bulletAngle * Mathf.PI / 180);

            // 각도로 부터 원점에서 시야 각도의 Vector2 값을 구하는 공식

            bullet.GetComponent<MovementTransform2D>().MoveSpeed(bulletSpeed);
            bullet.GetComponent<MovementTransform2D>().MoveTo(new Vector2(x, y));
        }
    }

}
