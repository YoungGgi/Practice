using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    [SerializeField]
    private Vector2 inputVec;
    
    [SerializeField]
    private float speed;
    Rigidbody2D rigid;
    
    private void Awake() 
    {
        rigid = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate() 
    {
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
        
        rigid.MovePosition(rigid.position + nextVec);
    }
}
