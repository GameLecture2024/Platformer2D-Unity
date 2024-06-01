using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabStudy : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rigidbody2D;
    public CircleCollider2D circleCollider2D;
    public Animator animator;          // 자식이 소유하고 있는 Component
    public AudioSource audioSource;    // 부모가 소유하고 있는 Component

    // Start is called before the first frame update
    void Start()
    {
        // GetComponent 코드 작성
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        animator = GetComponentInChildren<Animator>();
        audioSource = GetComponentInParent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
