using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float damage;

    public float Damage
    {
        get {return damage; }
    }

    [SerializeField]
    private int per;

    Rigidbody2D rigid;

    private void Awake() 
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void Init(float damage, int per, Vector3 dir)
    {
        this.damage = damage;
        this.per = per;

        if(per > -1)
        {
            rigid.velocity = dir * 15f;
        }
        
    }

    // per == -1(근접 무기일 때) - 해당 메소드 건너뜀
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(!other.CompareTag("Enemy") || per == -1)
        {
            return;
        }

        per--;

        if(per == -1)
        {
            rigid.velocity = Vector2.zero;
            gameObject.SetActive(false);
        }

    }
}
