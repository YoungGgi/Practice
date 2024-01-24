using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    [SerializeField]
    private float speed;
    [SerializeField]
    private float health;
    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private RuntimeAnimatorController[] animCon;
    [SerializeField]
    private Rigidbody2D target;

    private bool isLive;
    
    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriter;

    
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();
    }

    // 물리 이동
    // 타켓(플레이어)의 위치와 자신의 현재 위치를 빼면서 거리를 구함(dirVec)
    // 가야하는 위치를 정규화(normalized), 일정한 속도로 이동(nextVec)
    // 해당 값들(rigid.position + nextVec)을 토대로 물리 연산
    private void FixedUpdate() 
    {
        if(!isLive)
          return;
        
        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero; // 물리 속도가 이동에 영향을 주지 않도록 속도 제거

    }

    private void LateUpdate() 
    {
        if(!isLive)
          return;
        
        spriter.flipX = target.position.x < rigid.position.x;    
    }

    private void OnEnable() 
    {
        target = GameManager.instance.GetPlayer.GetComponent<Rigidbody2D>();
        isLive = true;
        health = maxHealth;
    }

    public void Init(SpawnData data)
    {
        anim.runtimeAnimatorController = animCon[data.SpriteType];
        speed = data.Speed;
        maxHealth = data.Health;
        health = data.Health;
    }

}
