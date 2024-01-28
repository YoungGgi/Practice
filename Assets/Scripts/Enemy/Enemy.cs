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

    WaitForFixedUpdate wait;
    
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();
        wait = new WaitForFixedUpdate();
    }

    // 물리 이동
    // 타켓(플레이어)의 위치와 자신의 현재 위치를 빼면서 거리를 구함(dirVec)
    // 가야하는 위치를 정규화(normalized), 일정한 속도로 이동(nextVec)
    // 해당 값들(rigid.position + nextVec)을 토대로 물리 연산
    private void FixedUpdate() 
    {
        // GetCurrentAnimatorStateInfo() => 현재 작동 중인 애니메이터의 상태 확인, 매개변수로 애니메이터의 레이어(base layer = 0)
        // .IsName() => 현재 작동중인 애니메이션의 이름 입력
        if(!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
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

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(!other.CompareTag("Bullet"))
        {
            return;
        }

        health -= other.GetComponent<Bullet>().Damage;

        // 피격시 넉백 효과
        StartCoroutine(KnockBack());

        if(health > 0)
        {
            anim.SetTrigger("Hit");
        }
        else
        {
            Dead();
        }

    }

    void Dead()
    {
        gameObject.SetActive(false);
    }

    IEnumerator KnockBack()
    {
        yield return wait;
        Vector3 playerPos = GameManager.instance.GetPlayer.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);  // 순간적인 움직임을 구현 = ForceMode2D.Impulse
    }

}
