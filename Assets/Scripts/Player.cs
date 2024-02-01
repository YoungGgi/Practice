using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    
    [SerializeField]
    private Vector2 inputVec;

    [SerializeField]
    private Scanner scanner;

    [SerializeField]
    private float speed;

    [SerializeField]
    private Hand[] hands;

    public Vector2 GetInputVec
    {
        get {return inputVec;}
        set {inputVec = value;}
    }
    public Scanner Scan
    { get {return scanner;} }
    
    public float GetPlayerSpeed
    {
        get {return speed;}
        set {speed = value;}
    }

    public Hand[] GetHands
    { get {return hands;} }


    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator anim;
    
    private void Awake() 
    {
        scanner = GetComponent<Scanner>();
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        hands = GetComponentsInChildren<Hand>(true);  // 비활성화된 오브젝트 or 컴포넌트를 초기화, 그리고 활성화
    }

    // 인풋 시스템 메소드
    void OnMove(InputValue value)
    {
        if(!GameManager.instance.GetIsLive)
          return;
        
        inputVec = value.Get<Vector2>();
    }


    private void FixedUpdate() 
    {
        if(!GameManager.instance.GetIsLive)
          return;
        
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
        
        rigid.MovePosition(rigid.position + nextVec);
    }

    private void LateUpdate()
    {
        if(!GameManager.instance.GetIsLive)
          return;
        
        anim.SetFloat("Speed", inputVec.magnitude);
        
        if(inputVec.x != 0)
        {
            spriter.flipX = (inputVec.x < 0) ? true : false;
        }
    }
}
