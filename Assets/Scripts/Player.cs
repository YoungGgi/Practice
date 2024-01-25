using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    
    [SerializeField]
    private Vector2 inputVec;

    public Vector2 GetInputVec
    {
        get {return inputVec;}
        set {inputVec = value;}
    }

    [SerializeField]
    private Scanner scanner;

    public Scanner Scan
    {
        get {return scanner;}
    }
    
    [SerializeField]
    private float speed;
    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator anim;
    
    private void Awake() 
    {
        scanner = GetComponent<Scanner>();
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // 인풋 시스템 메소드
    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }


    private void FixedUpdate() 
    {
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
        
        rigid.MovePosition(rigid.position + nextVec);
    }

    private void LateUpdate()
    {
        anim.SetFloat("Speed", inputVec.magnitude);
        
        
        if(inputVec.x != 0)
        {
            spriter.flipX = (inputVec.x < 0) ? true : false;
        }
    }
}
